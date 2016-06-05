using System;
using System.Threading.Tasks;

namespace GeekyTool.Messaging
{
    /// <summary>
    /// Provides an interface for classes that manage the callback methods 
    /// for notifications between objects.
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Adds a callback method to add to the collection.
        /// </summary>
        /// <typeparam name="T">Type of messages that forwards this message loop.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        void AddCallback<T>(Func<T, Task> callback);

        /// <summary>
        /// Adds a callback method to add to the collection.
        /// </summary>
        /// <param name="callback">Callback-Method.</param>
        void AddCallback(Func<Task> callback);

        /// <summary>
        /// Adds a callback method to add to the collection.
        /// </summary>
        /// <typeparam name="T">Type of messages that forwards this message loop.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        void AddCallback<T>(Action<T> callback);

        /// <summary>
        /// Adds a callback method to add to the collection.
        /// </summary>
        /// <param name="callback">Callback-Method.</param>
        void AddCallback(Action callback);

        /// <summary>
        /// Removes a callback method from the collection.
        /// </summary>
        /// <typeparam name="T">Type of messages that forwards this message loop.</typeparam>
        /// <param name="callback">Callback-Methode.</param>
        void RemoveCallback<T>(Func<T, Task> callback);

        /// <summary>
        /// Removes a callback method from the collection.
        /// </summary>
        /// <param name="callback">Callback-Method.</param>
        /// <exception cref="ArgumentNullException">
        /// Dispatched when <paramref name="callback" /> is equal <c>null</c>.
        /// </exception>
        void RemoveCallback(Func<Task> callback);

        /// <summary>
        /// Removes a callback method from the collection.
        /// </summary>
        /// <typeparam name="T">Type of messages that forwards this message loop.</typeparam>
        /// <param name="callback">Callback-Methode.</param>
        void RemoveCallback<T>(Action<T> callback);

        /// <summary>
        /// Get the callback count from the collection
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Removes all callback methods of an object from the collection.
        /// </summary>
        /// <param name="target">The object that has subscribed to notifications.</param>
        void RemoveAllCallbacks(object target);

        /// <summary>
        /// Removes a callback method from the collection.
        /// </summary>
        /// <param name="callback">Callback-Method.</param>
        /// <exception cref="ArgumentNullException">
        /// Dispatched when <paramref name="callback" /> is equal <c>null</c>.
        /// </exception>
        void RemoveCallback(Action callback);

        /// <summary>
        /// Performs all managed callback methods.
        /// </summary>
        /// <typeparam name="T">Type of messages that forwards this message loop.</typeparam>
        /// <param name="s">Message.</param>
        void Route<T>(T message);

        /// <summary>
        /// Performs all managed callback methods.
        /// </summary>
        void Route();
    }
}