#region Imports

using Lunox.Helpers;
using Lunox.Language.Enum;
using Lunox.Services;
using Microsoft.Services.Store.Engagement;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.UI.Xaml;

#endregion

namespace Lunox.ViewModels
{
    #region SettingsViewModel

    /// <summary>
    /// TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/pages/settings.md
    /// </summary>
    public class SettingsViewModel : ObservableObject
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        /// <summary>
        /// 
        /// </summary>
        private LanguageType _languageType = LanguageSelectorService.Language;

        /// <summary>
        /// 
        /// </summary>
        public ElementTheme ElementTheme
        {
            get => _elementTheme;

            set => SetProperty(ref _elementTheme, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public LanguageType LanguageType
        {
            get => _languageType;

            set => SetProperty(ref _languageType, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private string _versionDescription;

        /// <summary>
        /// 
        /// </summary>
        public string VersionDescription
        {
            get => _versionDescription;

            set => SetProperty(ref _versionDescription, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _switchThemeCommand;

        /// <summary>
        /// 
        /// </summary>
        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            //AppCenterService.TrackEvent("ChangeTheme", $"{ElementTheme}", $"{param}");
                            AppCenterService.TrackEvent("EVENT", "ChangeTheme", $"{ElementTheme}->{param}");
                            //AppCenterService.TrackEvent("Settings", "ChangeTheme", $"{ElementTheme}->{param}");
                            ElementTheme = param;
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return _switchThemeCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _switchLanguageCommand;

        /// <summary>
        /// 
        /// </summary>
        public ICommand SwitchLanguageCommand
        {
            get
            {
                if (_switchLanguageCommand == null)
                {
                    _switchLanguageCommand = new RelayCommand<LanguageType>(
                        async (param) =>
                        {
                            //AppCenterService.TrackEvent("ChangeLanguage", $"{LanguageType}", $"{param}");
                            AppCenterService.TrackEvent("EVENT", "ChangeLanguage", $"{LanguageType}->{param}");
                            //AppCenterService.TrackEvent("Settings", "ChangeLanguage", $"{LanguageType}->{param}");
                            LanguageType = param;
                            await LanguageSelectorService.SetLanguageAsync(param);
                        });
                }

                return _switchLanguageCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Visibility FeedbackLinkVisibility => StoreServicesFeedbackLauncher.IsSupported() ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// 
        /// </summary>
        private ICommand _launchFeedbackHubCommand;

        /// <summary>
        /// 
        /// </summary>
        public ICommand LaunchFeedbackHubCommand
        {
            get
            {
                if (_launchFeedbackHubCommand == null)
                {
                    _launchFeedbackHubCommand = new RelayCommand(
                        async () =>
                        {
                            // This launcher is part of the Store Services SDK https://docs.microsoft.com/windows/uwp/monetize/microsoft-store-services-sdk
                            StoreServicesFeedbackLauncher launcher = StoreServicesFeedbackLauncher.GetDefault();
                            await launcher.LaunchAsync();
                        });
                }

                return _launchFeedbackHubCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SettingsViewModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            VersionDescription = GetVersionDescription();
            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetVersionDescription()
        {
            string appName = "AppDisplayName".GetLocalized();
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        #endregion
    }

    #endregion
}