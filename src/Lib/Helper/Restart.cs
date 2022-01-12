#region Imports

using Lunox.Library.Util;
using System;
using System.Diagnostics;
using Windows.ApplicationModel.Core;

#endregion

namespace Lunox.Library.Helper
{
    #region RestartHelper

    /// <summary>
    /// 
    /// </summary>
    public static class Restart
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public static async void RestartRequest()
        {
            AppRestartFailureReason Result = await CoreApplication.RequestRestartAsync(Settings.Restart);

            if (Result == AppRestartFailureReason.NotInForeground || Result == AppRestartFailureReason.RestartPending || Result == AppRestartFailureReason.Other)
            {
                Debug.WriteLine("RequestRestartAsync failed: {0}", Result);
            }
        }

        #endregion
    }

    #endregion
}