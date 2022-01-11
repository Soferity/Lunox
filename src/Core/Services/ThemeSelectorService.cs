#region Imports

using Lunox.Core.Helpers;
using Lunox.Library.Value;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;

#endregion

namespace Lunox.Core.Services
{
    #region ThemeSelectorService

    /// <summary>
    /// 
    /// </summary>
    public static class ThemeSelectorService
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private static readonly string SettingsKey = Default.ThemeKey;

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public static ElementTheme Theme { get; set; } = Default.DefaultTheme;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task InitializeAsync()
        {
            Theme = await LoadThemeFromSettingsAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public static async Task SetThemeAsync(ElementTheme theme)
        {
            Theme = theme;

            await SetRequestedThemeAsync();
            await SaveThemeInSettingsAsync(Theme);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task SetRequestedThemeAsync()
        {
            foreach (CoreApplicationView view in CoreApplication.Views)
            {
                await view.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (Window.Current.Content is FrameworkElement frameworkElement)
                    {
                        frameworkElement.RequestedTheme = Theme;
                    }
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static async Task<ElementTheme> LoadThemeFromSettingsAsync()
        {
            ElementTheme cacheTheme = Default.DefaultTheme;
            string themeName = await ApplicationData.Current.LocalSettings.ReadAsync<string>(SettingsKey);

            if (!string.IsNullOrEmpty(themeName))
            {
                Enum.TryParse(themeName, out cacheTheme);
            }

            return cacheTheme;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        private static async Task SaveThemeInSettingsAsync(ElementTheme theme)
        {
            await ApplicationData.Current.LocalSettings.SaveAsync(SettingsKey, theme.ToString());
        }

        #endregion
    }

    #endregion
}