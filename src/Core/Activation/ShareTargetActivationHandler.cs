#region Imports

using Lunox.Views;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Activation
{
    #region ShareTargetActivationHandler

    /// <summary>
    /// 
    /// </summary>
    internal class ShareTargetActivationHandler : ActivationHandler<ShareTargetActivatedEventArgs>
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override async Task HandleInternalAsync(ShareTargetActivatedEventArgs args)
        {
            // Activation from ShareTarget opens the app as a new modal window which requires a new activation.
            Frame frame = new Frame();
            Window.Current.Content = frame;
            frame.Navigate(typeof(ShareTargetPage), args.ShareOperation);
            Window.Current.Activate();

            await Task.CompletedTask;
        }

        #endregion
    }

    #endregion
}