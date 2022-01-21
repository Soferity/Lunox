#region Imports

using Lunox.Core.Helpers;
using Lunox.Core.Services;
using Lunox.Language.Enum;
using Lunox.Library.Enum;
using Lunox.Library.Value;
using Microsoft.Services.Store.Engagement;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI.Xaml;
using MUXC = Microsoft.UI.Xaml.Controls;
using WUXC = Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.ViewModels
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
        private BrowserType _browserType = BrowserSelectorService.Browser;

        /// <summary>
        /// 
        /// </summary>
        private MUXC.NavigationViewPaneDisplayMode _navigationType = NavigationSelectorService.Navigation;

        /// <summary>
        /// 
        /// </summary>
        private WUXC.ComboBox _glance;

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
        public BrowserType BrowserType
        {
            get => _browserType;

            set => SetProperty(ref _browserType, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public MUXC.NavigationViewPaneDisplayMode NavigationType
        {
            get => _navigationType;

            set => SetProperty(ref _navigationType, value);
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
                            if (ElementTheme != param)
                            {
                                AppCenterService.TrackEvent(Default.Events[EventType.Theme], $"{ElementTheme}", $"{param}");
                                ElementTheme = param;
                                await ThemeSelectorService.SetThemeAsync(param);
                            }
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
                            if (LanguageType != param)
                            {
                                AppCenterService.TrackEvent(Default.Events[EventType.Language], $"{LanguageType}", $"{param}");
                                LanguageType = param;
                                await LanguageSelectorService.SetLanguageAsync(param);
                            }
                        });
                }

                return _switchLanguageCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _switchBrowserCommand;

        /// <summary>
        /// 
        /// </summary>
        public ICommand SwitchBrowserCommand
        {
            get
            {
                if (_switchBrowserCommand == null)
                {
                    _switchBrowserCommand = new RelayCommand<BrowserType>(
                        async (param) =>
                        {
                            if (BrowserType != param)
                            {
                                AppCenterService.TrackEvent(Default.Events[EventType.Browser], $"{BrowserType}", $"{param}");
                                BrowserType = param;
                                await BrowserSelectorService.SetBrowserAsync(param);
                            }
                        });
                }

                return _switchBrowserCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _switchNavigationCommand;

        /// <summary>
        /// 
        /// </summary>
        public ICommand SwitchNavigationCommand
        {
            get
            {
                if (_switchNavigationCommand == null)
                {
                    _switchNavigationCommand = new RelayCommand<MUXC.NavigationViewPaneDisplayMode>(
                        async (param) =>
                        {
                            if (NavigationType != param)
                            {
                                AppCenterService.TrackEvent(Default.Events[EventType.Navigation], $"{NavigationType}", $"{param}");
                                NavigationType = param;
                                await NavigationSelectorService.SetNavigationAsync(param);
                            }
                        });
                }

                return _switchNavigationCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _glanceSelectionChanged;

        /// <summary>
        /// 
        /// </summary>
        public ICommand GlanceSelectionChangedCommand
        {
            get
            {
                if (_glanceSelectionChanged == null)
                {
                    _glanceSelectionChanged = new RelayCommand<WUXC.SelectionChangedEventArgs>(GlanceSelectionChanged);
                }

                return _glanceSelectionChanged;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private async void GlanceSelectionChanged(WUXC.SelectionChangedEventArgs e)
        {
            WUXC.ComboBoxItem Item = (_glance.SelectedItem as WUXC.ComboBoxItem);

            AppCenterService.TrackEvent(Default.Events[EventType.Glance], GlanceSelectorService.Glance.ToString().Split('.').LastOrDefault().Replace("Page", ""), Item.Tag.ToString().Split('.').LastOrDefault().Replace("Page", ""));

            await GlanceSelectorService.SetGlanceAsync($"{Item.Tag}");
        }

        /// <summary>
        /// 
        /// </summary>
        public Visibility FeedbackLinkVisibility => Default.FeedbackLinkVisibility;

        /// <summary>
        /// 
        /// </summary>
        public Visibility PrivacyLinkVisibility => Default.PrivacyLinkVisibility;

        /// <summary>
        /// 
        /// </summary>
        private ICommand _launchFeedbackHubCommand;
        /// <summary>
        /// 
        /// </summary>
        private ICommand _launchPrivacyTermsCommand;

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
        public ICommand LaunchPrivacyTermsCommand
        {
            get
            {
                if (_launchPrivacyTermsCommand == null)
                {
                    _launchPrivacyTermsCommand = new RelayCommand(
                        async () =>
                        {
                            Uri Uri = Default.PrivacyTerms[LanguageType.en_GB];

                            if (Default.PrivacyTerms.ContainsKey(LanguageSelectorService.Language))
                            {
                                Uri = Default.PrivacyTerms[LanguageSelectorService.Language];
                            }

                            await Launcher.LaunchUriAsync(Uri);
                        });
                }

                return _launchPrivacyTermsCommand;
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
        /// <param name="Glance"></param>
        /// <returns></returns>
        public async Task InitializeAsync(WUXC.ComboBox Glance)
        {
            GlanceSetProperties(Glance);
            VersionDescription = GetVersionDescription();
            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        private List<MUXC.NavigationViewItem> Navigation_Item
        {
            get
            {
                List<MUXC.NavigationViewItem> Items = new();

                if (NavigationService.Navigation.MenuItems.Count > 0)
                {
                    foreach (MUXC.NavigationViewItem Item in NavigationService.Navigation.MenuItems.OfType<MUXC.NavigationViewItem>().Concat(NavigationService.Navigation.FooterMenuItems.OfType<MUXC.NavigationViewItem>()))
                    {
                        Items.Add(Item);
                    }
                }

                return Items;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Glance"></param>
        private void GlanceSetProperties(WUXC.ComboBox Glance)
        {
            _glance = Glance;

            foreach (MUXC.NavigationViewItem Item in Navigation_Item)
            {
                if (Item.MenuItems.Count > 0)
                {
                    foreach (MUXC.NavigationViewItem Menu in Item.MenuItems.OfType<MUXC.NavigationViewItem>())
                    {
                        Type Page = NavHelper.GetNavigateTo(Menu);

                        if (Page != null)
                        {
                            GlanceAddItem(Page, string.Concat(Item.Content, Default.NavigationSplit, Menu.Content));
                        }
                    }
                }
                else
                {
                    Type Page = NavHelper.GetNavigateTo(Item);

                    if (Page != null)
                    {
                        GlanceAddItem(Page, Item.Content);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tag"></param>
        /// <param name="Content"></param>
        private void GlanceAddItem(Type Tag, object Content)
        {
            WUXC.ComboBoxItem Item = new()
            {
                Tag = Tag,
                Content = Content
            };

            _glance.Items.Add(Item);

            if (GlanceSelectorService.Glance == Tag)
            {
                _glance.SelectedItem = Item;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetVersionDescription()
        {
            Package package = Package.Current;
            string appName = package.DisplayName;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        #endregion
    }

    #endregion
}