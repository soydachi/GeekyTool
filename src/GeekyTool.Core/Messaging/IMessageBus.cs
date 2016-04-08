using System;

namespace GeekyTool.Core.Messaging
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
        void AddCallback<T>(Action<T> callback);

        /// <summary>
        /// Removes a callback method from the collection.
        /// </summary>
        /// <typeparam name="T">Type of messages that forwards this message loop.</typeparam>
        /// <param name="callback">Callback-Methode.</param>
        void RemoveCallback<T>(Action<T> callback);

        /// <summary>
        /// Removes all callback methods of an object from the collection.
        /// </summary>
        /// <param name="target">The object that has subscribed to notifications.</param>
        void RemoveAllCallbacks(object target);

        /// <summary>
        /// Performs all managed callback methods.
        /// </summary>
        /// <typeparam name="T">Type of messages that forwards this message loop.</typeparam>
        /// <param name="s">Message.</param>
        void Route<T>(T message);
    }
}