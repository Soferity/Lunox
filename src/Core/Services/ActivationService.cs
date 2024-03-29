﻿#region Imports

using Lunox.Core.Activation;
using Lunox.Library.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Services
{
    #region ActivationService

    /// <summary>
    /// For more information on understanding and extending activation flow see
    /// https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/activation.md
    /// </summary>
    internal class ActivationService
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private readonly App _app;

        /// <summary>
        /// 
        /// </summary>
        private Type _defaultNavItem;

        /// <summary>
        /// 
        /// </summary>
        private readonly Lazy<UIElement> _shell;

        /// <summary>
        /// 
        /// </summary>
        private object _lastActivationArgs;

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="defaultNavItem"></param>
        /// <param name="shell"></param>
        public ActivationService(App app, Type defaultNavItem, Lazy<UIElement> shell = null)
        {
            _app = app;
            _shell = shell;
            _defaultNavItem = defaultNavItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activationArgs"></param>
        /// <returns></returns>
        public async Task ActivateAsync(object activationArgs)
        {
            if (IsInteractive(activationArgs))
            {
                // Initialize services that you need before app activation
                // take into account that the splash screen is shown while this code runs.
                await InitializeAsync();
                await Personalization();

                // Do not repeat app initialization when the Window already has content,
                // just ensure that the window is active
                if (Window.Current.Content == null)
                {
                    // Create a Shell or Frame to act as the navigation context
                    Window.Current.Content = _shell?.Value ?? new Frame();
                }
            }

            // Depending on activationArgs one of ActivationHandlers or DefaultActivationHandler
            // will navigate to the first page
            await HandleActivationAsync(activationArgs);
            _lastActivationArgs = activationArgs;

            if (IsInteractive(activationArgs))
            {
                IActivatedEventArgs activation = activationArgs as IActivatedEventArgs;
                if (activation.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    await Singleton<SuspendAndResumeService>.Instance.RestoreSuspendAndResumeData();
                }

                // Ensure the current window is active
                Window.Current.Activate();

                // Extend acrylic
                //ExtendAcrylicIntoTitleBar();

                // Tasks after activation
                await StartupAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ExtendAcrylicIntoTitleBar()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar TitleBar = ApplicationView.GetForCurrentView().TitleBar;
            TitleBar.ButtonBackgroundColor = Colors.Transparent;
            TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task InitializeAsync()
        {
            await ThemeSelectorService.InitializeAsync().ConfigureAwait(false);
            await GlanceSelectorService.InitializeAsync().ConfigureAwait(false);
            await BrowserSelectorService.InitializeAsync().ConfigureAwait(false);
            await LanguageSelectorService.InitializeAsync().ConfigureAwait(false);
            await NavigationSelectorService.InitializeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task Personalization()
        {
            if (ThemeSelectorService.Theme != ElementTheme.Default)
            {
                //App.Current.RequestedTheme = (ApplicationTheme)ThemeSelectorService.Theme;
                await ThemeSelectorService.SetRequestedThemeAsync();
            }

            //ApplicationLanguages.PrimaryLanguageOverride = LanguageSelectorService.Language.ToString().Replace("_", "-");
            await LanguageSelectorService.SetRequestedLanguageAsync();

            await NavigationSelectorService.SetNavigationLanguage();

            _defaultNavItem = GlanceSelectorService.Glance;

            //Sounds
            ElementSoundPlayer.Volume = 1.0;
            ElementSoundPlayer.State = ElementSoundPlayerState.On;
            ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.On;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activationArgs"></param>
        /// <returns></returns>
        private async Task HandleActivationAsync(object activationArgs)
        {
            ActivationHandler activationHandler = GetActivationHandlers().FirstOrDefault(h => h.CanHandle(activationArgs));

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync(activationArgs);
            }

            if (IsInteractive(activationArgs))
            {
                DefaultActivationHandler defaultHandler = new(_defaultNavItem);
                if (defaultHandler.CanHandle(activationArgs))
                {
                    await defaultHandler.HandleAsync(activationArgs);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task StartupAsync()
        {
            await ThemeSelectorService.SetRequestedThemeAsync();
            await FirstRunDisplayService.ShowIfAppropriateAsync();
            await WhatsNewDisplayService.ShowIfAppropriateAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ActivationHandler> GetActivationHandlers()
        {
            yield return Singleton<SuspendAndResumeService>.Instance;
            yield return Singleton<SchemeActivationHandler>.Instance;
            yield return Singleton<ToastNotificationsService>.Instance;
            yield return Singleton<ShareTargetActivationHandler>.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private bool IsInteractive(object args)
        {
            return args is IActivatedEventArgs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activationArgs"></param>
        /// <returns></returns>
        internal async Task ActivateFromShareTargetAsync(ShareTargetActivatedEventArgs activationArgs)
        {
            ActivationHandler shareTargetHandler = GetActivationHandlers().FirstOrDefault(h => h.CanHandle(activationArgs));
            if (shareTargetHandler != null)
            {
                await shareTargetHandler.HandleAsync(activationArgs);
            }
        }

        #endregion
    }

    #endregion
}