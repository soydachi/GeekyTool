using System;
using System.Reflection;

namespace GeekyTool.Messaging
{
    /// <summary>
    /// A class which delegates managed without holding a strong reference to the target object.
    /// </summary>
    public class WeakDelegate
    {
        private MethodInfo method;
        private WeakReference targetReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakDelegate" />-Klasse.
        /// </summary>
        /// <param name="action">Delegate to a method.</param>
        public WeakDelegate(Delegate action) : this(new WeakReference(action.Target), action.GetMethodInfo()) { }

        /// <summary>
        /// Initializes a new instance of the<see cref="WeakDelegate" />-Class.
        /// </summary>
        /// <param name="targetReference">Weak reference to the target object.</param>
        /// <param name="method">target method.</param>
        /// <exception cref="ArgumentNullException">
        /// Dispatched when<paramref name="method" /> is NULL, OR if <paramref
        /// name="method" /> is a not static method, and <paramref name="targetReference" />
        /// In this case does not point to an object.
        /// </exception>
        public WeakDelegate(WeakReference targetReference, MethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException(MemberResolver.Resolve(() => method).Name);
            if (targetReference.Target == null && !method.IsStatic)
            {
                throw new ArgumentNullException(
                    MemberResolver.Resolve(() => targetReference).Name,
                    "If the target method is not static, the target object can not be null.");
            }

            this.targetReference = targetReference;
            this.method = method;
        }

        /// <summary>
        /// Indicates whether the method can be called.
        /// </summary>
        /// <remarks>
        /// <list type="table"><listheader><term>return Value</term><description>description</description></listheader><item><term><c>true</c></term><description>Method
        /// is static OR call target exists
        /// yet</description></item><item><term><c>false</c></term><description>Method is not
        /// static and call destination does not exist</description></item></list>
        /// </remarks>
        public bool IsExecutable
        {
            get { return IsMethodStatic || IsTargetAlive; }
        }

        /// <summary>
        /// Specifies whether the target method is static.
        /// </summary>
        public bool IsMethodStatic
        {
            get { return method.IsStatic; }
        }

        /// <summary>
        /// Indicates whether the target object is still exists. If the method is static, provides this
        /// Property always <c>false</c>.
        /// </summary>
        public bool IsTargetAlive
        {
            get { return targetReference.Target != null; }
        }

        /// <summary>
        /// Provides information to the target method.
        /// </summary>
        public MethodInfo Method
        {
            get { return method; }
        }

        /// <summary>
        /// Returns a weak reference to the target object.
        /// </summary>
        public WeakReference TargetReference
        {
            get { return targetReference; }
        }

        /// <summary>
        /// Created from the <see cref="WeakDelegate" />-Instance a delegate.
        /// </summary>
        /// <typeparam name="T">Type of Delegate.</typeparam>
        /// <returns>Delegate.</returns>
        public Delegate CreateDelegate<T>()
        {
            var methodDelegate = this.Method.CreateDelegate(typeof (T), this.TargetReference.Target);

            return methodDelegate;
        }

        public delegate void OpenEventHandler<in T>(T target, object sender, EventArgs arguments);
    }
}