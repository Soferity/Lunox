#region Imports

using Lunox.Library.Helper;
using Lunox.Services;
using Lunox.Views;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using WUX = Windows.UI.Xaml;

#endregion

namespace Lunox
{
    #region AppLunox

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class App : WUX.Application
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private readonly Lazy<ActivationService> _activationService;

        /// <summary>
        /// 
        /// </summary>
        private ActivationService ActivationService => _activationService.Value;

        #endregion

        #region Functions

        #region App Function

        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            UnhandledException += OnAppUnhandledException;
            EnteredBackground += App_EnteredBackground;
            Resuming += App_Resuming;

            // TODO WTS: Add your app in the app center and set your secret here. More at https://docs.microsoft.com/appcenter/sdk/getting-started/uwp
            AppCenterService.Engine();

            // Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        #endregion

        #region Helper Function

        #region Normal Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            Windows.Foundation.Deferral deferral = e.GetDeferral();
            await Singleton<SuspendAndResumeService>.Instance.SaveStateAsync();
            deferral.Complete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_Resuming(object sender, object e)
        {
            Singleton<SuspendAndResumeService>.Instance.ResumeApp();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAppUnhandledException(object sender, WUX.UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
            AppCenterService.Exception(e);
        }

        #endregion

        #region Event Functions

        /// <summary>
        /// Invoked when the application is launched normally by the end user. Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args"></param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(MainPage), new Lazy<WUX.UIElement>(CreateShell));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private WUX.UIElement CreateShell()
        {
            return new ShellPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override async void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            await ActivationService.ActivateFromShareTargetAsync(args);
        }

        #endregion

        #endregion

        #endregion
    }

    #endregion
}