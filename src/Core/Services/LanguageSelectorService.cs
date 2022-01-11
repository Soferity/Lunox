#region Imports

using Lunox.Core.Helpers;
using Lunox.Language.Enum;
using Lunox.Library.Helper;
using Lunox.Library.Value;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources.Core;
using Windows.Globalization;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

#endregion

namespace Lunox.Core.Services
{
    #region LanguageSelectorService

    /// <summary>
    /// 
    /// </summary>
    public static class LanguageSelectorService
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private static readonly string SettingsKey = Default.LanguageKey;

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public static LanguageType Language { get; set; } = Default.DefaultLanguage;

        /// <summary>
        /// 
        /// </summary>
        public static IReadOnlyList<string> Languages = Default.DefaultLanguages;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task InitializeAsync()
        {
            Language = await LoadLanguageFromSettingsAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static async Task SetLanguageAsync(LanguageType language)
        {
            Language = language;

            await SetRequestedLanguageAsync();
            await SaveLanguageInSettingsAsync(Language);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task SetRequestedLanguageAsync()
        {
            string Lang = Language.ToString().Replace("_", "-");

            ApplicationLanguages.PrimaryLanguageOverride = Lang;

            foreach (CoreApplicationView view in CoreApplication.Views)
            {
                await view.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (Window.Current.Content is FrameworkElement frameworkElement)
                    {
                        frameworkElement.Language = Lang;
                        frameworkElement.UpdateLayout();
                    }
                });
            }

            ResourceContext.GetForCurrentView().Reset();
            ResourceContext.GetForViewIndependentUse().Reset();

            NavigationService.Display = NavigationSelectorService.Navigation;

            string Text = string.Empty;

            if (NavigationService.Navigation.MenuItems.Count > 0)
            {
                foreach (IList<Microsoft.UI.Xaml.Controls.NavigationViewItem> Item in NavigationService.Navigation.MenuItems)
                {
                    Text += Item.ToString();
                }
            }

            if (NavigationService.Frame.SourcePageType != null)
            {
                NavigationService.Frame.NavigateToType(NavigationService.Frame.SourcePageType, null, new FrameNavigationOptions() { IsNavigationStackEnabled = false });
            }

            Dialog.SendDialog(Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static async Task<LanguageType> LoadLanguageFromSettingsAsync()
        {
            LanguageType cacheLanguage = Default.DefaultLanguage;
            string languageName = await ApplicationData.Current.LocalSettings.ReadAsync<string>(SettingsKey);

            if (!string.IsNullOrEmpty(languageName))
            {
                Enum.TryParse(languageName, out cacheLanguage);
            }

            return cacheLanguage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        private static async Task SaveLanguageInSettingsAsync(LanguageType language)
        {
            await ApplicationData.Current.LocalSettings.SaveAsync(SettingsKey, language.ToString());
        }

        #endregion
    }

    #endregion
}