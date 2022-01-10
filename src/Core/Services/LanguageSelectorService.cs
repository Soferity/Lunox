#region Imports

using Lunox.Helpers;
using Lunox.Language.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources.Core;
using Windows.Globalization;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;

#endregion

namespace Lunox.Services
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
        private const string SettingsKey = "AppRequestedLanguage";

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public static LanguageType Language { get; set; } = LanguageType.en_GB;

        /// <summary>
        /// 
        /// </summary>
        public static IReadOnlyList<string> Languages = ApplicationLanguages.ManifestLanguages;

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
            //NavigationService.Navigate(typeof(SettingsPage), DateTime.Now.Ticks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static async Task<LanguageType> LoadLanguageFromSettingsAsync()
        {
            LanguageType cacheLanguage = LanguageType.en_GB;
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