using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace GeekyTool.Messaging
{
    /// <summary>
    /// A class that manages the notifications between objects. 
    /// The references to the receiver to be kept as weak reference, 
    /// allowing them to be cleaned up by garbage collection.
    /// </summary>
    public class WeakMessageBus : IMessageBus
    {
        /// <summary>
        /// Contains a <see cref="WeakReference" /> to the target object of the method and <see
        /// cref="MethodInfo" /> the Callback-Method.
        /// </summary>
        private HashSet<WeakDelegate> callbacks;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReferenceBus" />-Class.
        /// </summary>
        public WeakMessageBus()
        {
            callbacks = new HashSet<WeakDelegate>();
        }

        /// <summary>
        /// Adds a callback method to add to the collection.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        /// <exception cref="ArgumentNullException">
        /// Dispatched when <paramref name="callback" /> is equal <c>null</c>.
        /// </exception>
        public void AddCallback<T>(Func<T, Task> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(MemberResolver.Resolve(() => callback).Name);

            var weakAction = new WeakDelegate(callback);
            callbacks.Add(weakAction);
        }

        /// <summary>
        /// Adds a callback method to add to the collection.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        /// <exception cref="ArgumentNullException">
        /// Dispatched when <paramref name="callback" /> is equal <c>null</c>.
        /// </exception>
        public void AddCallback<T>(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(MemberResolver.Resolve(() => callback).Name);

            var weakAction = new WeakDelegate(callback);
            callbacks.Add(weakAction);
        }

        /// <summary>
        /// Removes all callback methods of an object from the collection.
        /// </summary>
        /// <param name="target">The object that has subscribed to notifications.</param>
        public void RemoveAllCallbacks(object target)
        {
            int count = callbacks.RemoveWhere(cb => Object.ReferenceEquals(cb.TargetReference.Target, target));
            DebuggingOutput(count);
        }

        /// <summary>
        /// Removes a callback method from the collection.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        /// <exception cref="ArgumentNullException">
        /// Dispatched when <paramref name="callback" /> is equal <c>null</c>.
        /// </exception>
        public void RemoveCallback<T>(Func<T, Task> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(MemberResolver.Resolve(() => callback).Name);

            int count = callbacks.RemoveWhere(cb =>
            {
                bool sameTarget = Object.ReferenceEquals(cb.TargetReference.Target, callback.Target);
                bool sameMethod = cb.Method == callback.GetMethodInfo();
                return sameTarget && sameMethod;
            });
            DebuggingOutput(count);
        }

        /// <summary>
        /// Removes a callback method from the collection.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="callback">Callback-Method.</param>
        /// <exception cref="ArgumentNullException">
        /// Dispatched when <paramref name="callback" /> is equal <c>null</c>.
        /// </exception>
        public void RemoveCallback<T>(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(MemberResolver.Resolve(() => callback).Name);

            int count = callbacks.RemoveWhere(cb =>
            {
                bool sameTarget = Object.ReferenceEquals(cb.TargetReference.Target, callback.Target);
                bool sameMethod = cb.Method == callback.GetMethodInfo();
                return sameTarget && sameMethod;
            });
            DebuggingOutput(count);
        }

        /// <summary>
        /// Performs all managed callback methods. Here are references to objects that have 
        /// already been cleaned up by the garbage collection.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="s">Message.</param>
        public void Route<T>(T message)
        {
            if (callbacks.Count < 1)
                return;

            CleanCallbackSet();
            foreach (var item in callbacks)
            {
                var reference = item.TargetReference;
                var method = item.Method;
                if (item.IsExecutable)
                {
                    var callback = (Action<T>)item.CreateDelegate<Action<T>>();
                    callback(message);
                }
            }
        }

        /// <summary>
        /// Removes all <see cref="WeakReference" />-Objects from the collection, 
        /// whose goal has been cleaned up by garbage collection.
        /// </summary>
        private void CleanCallbackSet()
        {
            int count = callbacks.RemoveWhere(weakaction => !weakaction.IsExecutable);
            DebuggingOutput(count);
        }

        //In release build, this method is skipped
        [Conditional("DEBUG")]
        private void DebuggingOutput(int removedCount)
        {
            string s;
            if (removedCount == 0)
                Debug.WriteLine("No listeners removed.");
            else
            {
                s = removedCount > 1 ? "s" : "";
                Debug.WriteLine(String.Format("{0} dead listener reference" + s + " removed.", removedCount));
            }
            int remainingCount = callbacks.Count;
            s = remainingCount > 1 ? "s" : "";
            Debug.WriteLine(String.Format("{0} listener reference" + s + " remaining.", remainingCount));
        }
    }
}