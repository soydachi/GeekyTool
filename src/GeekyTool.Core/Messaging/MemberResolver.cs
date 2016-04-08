using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GeekyTool.Core.Messaging
{
    /// <summary>
    /// Provides helper methods with which information about the members of a class can be determined.
    /// </summary>
    public static class MemberResolver
    {
        /// <summary>
        /// Throws an <see cref="Expression&lt;Func&lt;T&gt;&gt;"/> in an <see cref="MemberInfo"/>-object on.
        /// </summary>
        /// <typeparam name="T">Type of the member. Need not be specified explicitly.</typeparam>
        /// <param name="expression"><see cref="Expression&lt;Func&lt;T&gt;&gt;"/>-Object that points to 
        /// the members of a class.</param>
        /// <returns>A <see cref="MemberInfo"/>-Object which contains the member information.</returns>
        public static MemberInfo Resolve<T>(Expression<Func<T>> expression)
        {
            var me = (MemberExpression)expression.Body;
            return me.Member;
        }
    }
}