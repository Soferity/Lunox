#region Imports

using System.Threading.Tasks;

#endregion

namespace Lunox.Core.Activation
{
    #region ActivationHandler

    /// <summary>
    /// For more information on understanding and extending activation flow see
    /// https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/activation.md
    /// </summary>
    internal abstract class ActivationHandler
    {
        public abstract bool CanHandle(object args);

        public abstract Task HandleAsync(object args);
    }

    /// <summary>
    /// Extend this class to implement new ActivationHandlers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class ActivationHandler<T> : ActivationHandler where T : class
    {
        /// <summary>
        /// Override this method to add the activation logic in your activation handler
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected abstract Task HandleInternalAsync(T args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override async Task HandleAsync(object args)
        {
            await HandleInternalAsync(args as T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool CanHandle(object args)
        {
            // CanHandle checks the args is of type you have configured
            return args is T && CanHandleInternal(args as T);
        }

        /// <summary>
        /// You can override this method to add extra validation on activation args
        /// to determine if your ActivationHandler should handle this activation args
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual bool CanHandleInternal(T args)
        {
            return true;
        }
    }

    #endregion
}