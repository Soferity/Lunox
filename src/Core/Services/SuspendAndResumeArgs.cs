#region Imports

using System;

#endregion

namespace Lunox.Services
{
    #region SuspendAndResumeArgs
    /// <summary>
    /// 
    /// </summary>
    public class SuspendAndResumeArgs : EventArgs
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public SuspensionState SuspensionState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Type Target { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="suspensionState"></param>
        /// <param name="target"></param>
        public SuspendAndResumeArgs(SuspensionState suspensionState, Type target)
        {
            SuspensionState = suspensionState;
            Target = target;
        }

        #endregion
    }

    #endregion
}