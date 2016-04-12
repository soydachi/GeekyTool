using System;
using System.Threading.Tasks;

namespace GeekyTool.Messaging
{
    /// <summary>
    /// The Messenger is a class allowing objects to exchange messages.
    /// </summary>
    public interface IMessenger
    {
        /// <summary>
        /// Notify all object who suscribed with this token.
        /// </summary>
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="s">Message.</param>
        /// <param name="token">Token.</param>
        void Publish<T>(T message, object token);

        /// <summary>
        /// Notify all object who registred implicit with the token
        /// </summary>
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="s">Message.</param>
        void Publish<T>(T message);

        /// <summary>
        /// Register a Callback-Method for certain message and a certain Token.
        /// </summary>
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="callback">Callback-Method, who work with he message.</param>
        /// <param name="token">Token.</param>
        void Register<T>(Func<T, Task> callback, object token);

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
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="callback">Callback-Method, who work with he message.</param>
        void Register<T>(Action<T> callback);

        /// <summary>
        /// Erase a Callback-Method for a certain message, with a certain registred Token.
        /// </summary>
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        /// <param name="token">Token.</param>
        void Unregister<T>(Func<T, Task> callback, object token);

        /// <summary>
        /// Erase a Callback-Method for a certain message, with the default Token.
        /// </summary>
        /// <typeparam name="T">Type of Message.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        void Unregister<T>(Func<T, Task> callback);
    }
}