using System;
using System.Diagnostics;
using Windows.ApplicationModel.Core;

namespace Lunox.Core.Helper
{
    public static class Restart
    {
        public static async void RestartRequest()
        {
            AppRestartFailureReason Result = await CoreApplication.RequestRestartAsync(Settings.Restart);

            if (Result == AppRestartFailureReason.NotInForeground || Result == AppRestartFailureReason.RestartPending || Result == AppRestartFailureReason.Other)
            {
                Debug.WriteLine("RequestRestartAsync failed: {0}", Result);
            }
        }
    }
}