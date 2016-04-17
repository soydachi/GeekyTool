namespace GeekyTool.Messaging
{
    /// <summary>
    /// Defines an interface for classes that create objects whose types <see
    /// cref="IMessageBus" />-Implement Interface.
    /// </summary>
    public interface IMessageBusFactory
    {
        /// <summary>
        /// provides a <see cref="IMessageBus" />.
        /// </summary>
        /// <returns>
        /// An object whose type is the <see cref="IMessageBus" />-Implement Interface.
        /// </returns>
        IMessageBus CreateBus();
    }

    /// <summary>
    /// Returns objects of type <see cref="WeakMessageBus" />.
    /// </summary>
    public class WeakMessageBusFactory : IMessageBusFactory
    {
        /// <summary>
        /// provides a <see cref="IMessageBus" />.
        /// </summary>
        /// <returns>
        /// An object whose type is the <see cref="IMessageBus" />-Implement Interface.
        /// </returns>
        public IMessageBus CreateBus()
        {
            return new WeakMessageBus();
        }
    }
}