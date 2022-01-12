#region Imports

using Lunox.Core.Views;
using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

#endregion

namespace Lunox.Core.Services
{
    #region WhatsNewDisplayService

    /// <summary>
    /// For instructions on testing this service see https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/features/whats-new-prompt.md
    /// </summary>
    public static class WhatsNewDisplayService
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private static bool shown = false;

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static async Task ShowIfAppropriateAsync()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync
            (
                CoreDispatcherPriority.Normal, async () =>
                {
                    if (SystemInformation.Instance.IsAppUpdated && !shown)
                    {
                        shown = true;
                        WhatsNewDialog dialog = new WhatsNewDialog();
                        await dialog.ShowAsync();
                    }
                }
            );
        }

        #endregion
    }

    #endregion
}