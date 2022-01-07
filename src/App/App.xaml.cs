#region Imports

using Lunox.Library.Helper;
using Lunox.Library.Util;
using Lunox.Services;
using Lunox.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Channel;
using Microsoft.AppCenter.Crashes;
using System;
using System.Globalization;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Globalization;
using Windows.UI.Xaml;

#endregion

namespace Lunox
{
    #region AppLunox

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class App : Application
    {
        #region Variables

        private readonly Lazy<ActivationService> _activationService;

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
            App.Current.RequestedTheme = Settings.Theme;
            ApplicationLanguages.PrimaryLanguageOverride = Settings.Language;

            InitializeComponent();

            EnteredBackground += App_EnteredBackground;
            Resuming += App_Resuming;

            // TODO WTS: Add your app in the app center and set your secret here. More at https://docs.microsoft.com/appcenter/sdk/getting-started/uwp

            //Crashes.SetEnabledAsync(true);
            //AppCenter.LogLevel = LogLevel.Verbose;
            AppCenter.SetUserId(Environment.MachineName);
            //AppCenter.SetCountryCode(RegionInfo.CurrentRegion.TwoLetterISORegionName);
            AppCenter.SetCountryCode(CultureInfo.InstalledUICulture.TwoLetterISOLanguageName);
            AppCenter.Start("APP-CENTER", typeof(Analytics), typeof(Crashes), typeof(Channel));

            UnhandledException += OnAppUnhandledException;

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
        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
            Crashes.TrackError(e.Exception);
            Dialog.SendDialog(e.Exception.Message + "\n" + e.Exception.StackTrace, e.Exception.Source);
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
            return new ActivationService(this, typeof(MainPage), new Lazy<UIElement>(CreateShell));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private UIElement CreateShell()
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
