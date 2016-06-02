using System;
using System.Threading.Tasks;

namespace GeekyTool.Messaging
{
    /// <summary>
    /// The Messenger is a class allowing objects to exchange messages.
    /// </summary>
    public interface IMessenger
    {
        // <summary>
        /// Notify all subscribers who have registered with a specific token.
        /// </summary>
        /// <param name="token">Token.</param>
        /// <exception cref="ArgumentNullException">
        /// Dispatched when <paramref name="token" /> equal <c>null</c>.
        /// </exception>
        void Publish(string token);

        /// <summary> 
        /// Notify all subscribers who have (implicitly) registered with the default token. 
        /// </summary> 
        /// <typeparam name="T">Type of message.</typeparam> 
        /// <param name="message">Message.</param> 
        void Publish<T>(T message);

        /// <summary>
        /// Notify all object who suscribed with this token and message type.
        /// </summary>
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="s">Message.</param>
        /// <param name="token">Token.</param>
        void Publish<T>(T message, object token);

        /// <summary>
        /// Register a Callback-Method for certain message and a certain Token.
        /// </summary>
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="callback">Callback-Method, who work with he message.</param>
        /// <param name="token">Token.</param>
        void Register<T>(Func<T, Task> callback, object token);

        /// <summary>
        /// Registers a callback method for a particular message type 
        /// with a specific token.
        /// </summary>
        /// <param name="callback">Callback-Method, who work with he message.</param>
        /// <param name="token">Token.</param>
        /// <exception cref="ArgumentNullException">
        /// Occurs when one of the parameters is <c>null</c>.
        /// </exception>
        void Register(Func<Task> callback, object token);

        /// <summary>
        /// Register a Callback-Method for certain message and a certain Token.
        /// </summary>
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="callback">Callback-Method, who work with he message.</param>
        /// <param name="token">Token.</param>
        void Register<T>(Action<T> callback, object token);

        /// <summary>
        /// Register a Callback-Method for the default Token with a certain message.
        /// </summary>
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="callback">Callback-Method, who work with he message.</param>
        void Register<T>(Func<T, Task> callback);

        /// <summary>
        /// Register a Callback-Method for the default Token with a certain message.
        /// </summary>
        /// <param name="callback">Callback-Method, who work with he message.</param>
        void Register(Func<Task> callback);

        /// <summary>
        /// Register a Callback-Method for the default Token with a certain message.
        /// </summary>
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="callback">Callback-Method, who work with he message.</param>
        void Register<T>(Action<T> callback);

        /// <summary>
        /// Registers a callback method for a particular message type 
        /// with a specific token.
        /// </summary>
        /// <param name="callback">Callback-Method, who work with he message.</param>
        /// <param name="token">Token.</param>
        /// <exception cref="ArgumentNullException">
        /// Occurs when one of the parameters is <c>null</c>.
        /// </exception>
        void Register(Action callback, object token);

        /// <summary>
        /// Removes a callback method for a particular message type, 
        /// the (implicit) was registered with the default token.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        void Unregister<T>(Action<T> callback);

        /// <summary>
        /// Erase a Callback-Method for a certain message, with a certain registred Token.
        /// </summary>
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        /// <param name="token">Token.</param>
        void Unregister<T>(Func<T, Task> callback, object token);

        /// <summary>
        /// Removes a callback method for a particular message type 
        /// that was registered with a specific token
        /// </summary>
        /// <param name="callback">Callback-Method.</param>
        /// <param name="token">Token.</param>
        /// <exception cref="ArgumentNullException"><paramref name=""/> is <see langword="null" />.</exception>
        void Unregister(Func<Task> callback, object token);

        /// <summary>
        /// Removes a callback method for a particular message type 
        /// that was registered with a specific token
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        /// <param name="token">Token.</param>
        /// <exception cref="ArgumentNullException"><paramref name=""/> is <see langword="null" />.</exception>
        void Unregister<T>(Action<T> callback, object token);

        /// <summary>
        /// Removes a callback method for a particular message type 
        /// that was registered with a specific token
        /// </summary>
        /// <param name="callback">Callback-Method.</param>
        /// <param name="token">Token.</param>
        /// <exception cref="ArgumentNullException"><paramref name=""/> is <see langword="null" />.</exception>
        void Unregister(Action callback, object token);

        /// <summary>
        /// Erase a Callback-Method for a certain message, with the default Token.
        /// </summary>
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        void Unregister<T>(Func<T, Task> callback);

        /// <summary>
        /// Removes a callback method for a particular message type, 
        /// the (implicit) was registered with the default token.
        /// </summary>
        /// <param name="callback">Callback-Method.</param>
        void Unregister(Func<Task> callback);
    }
}