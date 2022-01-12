#region Imports

using Lunox.Core.Services;
using Lunox.Library.Helper;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

#endregion

namespace Lunox.Core.Activation
{
    #region DefaultActivationHandler

    /// <summary>
    /// 
    /// </summary>
    internal class DefaultActivationHandler : ActivationHandler<IActivatedEventArgs>
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private readonly Type _navElement;

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navElement"></param>
        public DefaultActivationHandler(Type navElement)
        {
            _navElement = navElement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override async Task HandleInternalAsync(IActivatedEventArgs args)
        {
            // When the navigation stack isn't restored, navigate to the first page and configure
            // the new page by passing required information in the navigation parameter
            object arguments = null;
            if (args is LaunchActivatedEventArgs launchArgs)
            {
                arguments = launchArgs.Arguments;
            }

            NavigationService.Navigate(_navElement, arguments);

            // TODO WTS: Remove or change this sample which shows a toast notification when the app is launched.
            // You can use this sample to create toast notifications where needed in your app.
            Singleton<ToastNotificationsService>.Instance.ShowToastNotificationSample();
            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override bool CanHandleInternal(IActivatedEventArgs args)
        {
            // None of the ActivationHandlers has handled the app activation
            return NavigationService.Frame.Content == null && _navElement != null;
        }

        #endregion
    }

    #endregion
}