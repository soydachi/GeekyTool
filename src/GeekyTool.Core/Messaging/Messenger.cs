using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GeekyTool.Core.Messaging
{
    /// <summary>
    /// Allows for easy communication between loosely coupled classes.
    /// </summary>
    public class Messenger : IMessenger
    {
        /// <summary>
        /// Standard token.
        /// </summary>
        private const string DefaultToken = "event://Messenger/Default";

        private static readonly object defaultInstanceLock = new object();
        private static Messenger defaultInstance;

        //key:   Type of notification.
        //value: key:   Token
        //       value: MessageBus
        private readonly Dictionary<Type, Dictionary<object, IMessageBus>> messaging;

        private readonly object messagingDictionaryLock = new object();



        /// <summary>
        /// Returns the default instance of the <see cref="Messenger" />-Class.
        /// </summary>
        public static Messenger Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    lock (defaultInstanceLock)
                    {
                        if (defaultInstance == null)
                        {
                            defaultInstance = new Messenger();
                        }
                    }
                }
                return defaultInstance;
            }
        }

        /// <summary>
        /// Notify all subscribers who have (implicitly) registered with the default token.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="s">Message.</param>
        public void Publish<T>(T message)
        {
            Publish(message, DefaultToken);
        }

        /// <summary>
        /// Notify all subscribers who have registered with a specific token.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="s">Message.</param>
        /// <param name="token">Token.</param>
        /// <exception cref="ArgumentNullException">
        /// Dispatched when <paramref name="token" /> equal <c>null</c>.
        /// </exception>
        public void Publish<T>(T message, object token)
        {
            if (token == null)
                throw new ArgumentNullException(MemberResolver.Resolve(() => token).Name);

#if DEBUG
            var sw = Stopwatch.StartNew();
#endif

            Type messageType = typeof(T);
            lock (messagingDictionaryLock)
            {
                if (messaging.ContainsKey(messageType))
                {
                    var busDictionary = messaging[messageType];
                    if (busDictionary.ContainsKey(token))
                    {
#if DEBUG
                        Debug.WriteLine($"Routing event with token {token}.");
#endif
                        var messageBus = busDictionary[token];
                        messageBus.Route(message);
                    }
#if DEBUG
                    else
                    {
                        Debug.WriteLine($"No subscribers for token {token}.");
                    }
#endif
                }
#if DEBUG
                else
                {
                    Debug.WriteLine($"No subscribers for type {messageType}.");
                }
#endif
            }
#if DEBUG
            sw.Stop();
            Debug.WriteLine($"Routing took {sw.ElapsedMilliseconds} ms.");
#endif
        }

        /// <summary>
        /// Registers a callback method for a particular message type with the default token.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="callback">Callback-Method, who work with he message.</param>
        public void Register<T>(Action<T> callback)
        {
            Register(callback, DefaultToken);
        }

        /// <summary>
        /// Registers a callback method for a particular message type 
        /// with a specific token.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="callback">Callback-Method, who work with he message.</param>
        /// <param name="token">Token.</param>
        /// <exception cref="ArgumentNullException">
        /// Occurs when one of the parameters is <c>null</c>.
        /// </exception>
        public void Register<T>(Action<T> callback, object token)
        {
            if (callback == null)
                throw new ArgumentNullException(MemberResolver.Resolve(() => callback).Name);

            if (token == null)
                throw new ArgumentNullException(MemberResolver.Resolve(() => token).Name);

            Type messageType = typeof(T);
            lock (messagingDictionaryLock)
            {
                if (!messaging.ContainsKey(messageType))
                    messaging.Add(messageType, new Dictionary<object, IMessageBus>());
                var tokenizedBusDictionary = messaging[messageType];

                var messageBus = tokenizedBusDictionary[token];

                messageBus.AddCallback(callback);
            }
        }

        /// <summary>
        /// Removes a callback method for a particular message type, 
        /// the (implicit) was registered with the default token.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        public void Unregister<T>(Action<T> callback)
        {
            Unregister(callback, DefaultToken);
        }

        /// <summary>
        /// Removes a callback method for a particular message type 
        /// that was registered with a specific token
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        /// <param name="token">Token.</param>
        /// <exception cref="ArgumentNullException"><paramref name=""/> is <see langword="null" />.</exception>
        public void Unregister<T>(Action<T> callback, object token)
        {
            if (callback == null)
                throw new ArgumentNullException(MemberResolver.Resolve(() => callback).Name);
            if (token == null)
                throw new ArgumentNullException(MemberResolver.Resolve(() => token).Name);

            Type messageType = typeof(T);
            lock (messagingDictionaryLock)
            {
                if (messaging.ContainsKey(messageType))
                {
                    var busDictionary = messaging[messageType];
                    if (busDictionary.ContainsKey(token))
                    {
                        var messageBus = busDictionary[token];
                        messageBus.RemoveCallback(callback);
                    }
                }
            }
        }
    }
}