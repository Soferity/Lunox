#region Imports

using Lunox.Core.Behaviors;
using Lunox.Core.Helpers;
using Lunox.Library.Value;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources.Core;
using Windows.Globalization;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;

#endregion

namespace Lunox.Core.Services
{
    #region NavigationSelectorService

    /// <summary>
    /// 
    /// </summary>
    public static class NavigationSelectorService
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private static readonly string SettingsKey = Default.NavigationKey;

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public static NavigationViewPaneDisplayMode Navigation { get; set; } = Default.DefaultNavigation;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task InitializeAsync()
        {
            Navigation = await LoadNavigationFromSettingsAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigation"></param>
        /// <returns></returns>
        public static async Task SetNavigationAsync(NavigationViewPaneDisplayMode navigation)
        {
            Navigation = navigation;

            SetRequestedNavigationAsync();
            await SaveNavigationInSettingsAsync(Navigation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static void SetRequestedNavigationAsync()
        {
            NavigationService.Display = Navigation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static async Task<NavigationViewPaneDisplayMode> LoadNavigationFromSettingsAsync()
        {
            NavigationViewPaneDisplayMode cacheNavigation = Default.DefaultNavigation;
            string navigationName = await ApplicationData.Current.LocalSettings.ReadAsync<string>(SettingsKey);

            if (!string.IsNullOrEmpty(navigationName))
            {
                Enum.TryParse(navigationName, out cacheNavigation);
            }

            return cacheNavigation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigation"></param>
        /// <returns></returns>
        private static async Task SaveNavigationInSettingsAsync(NavigationViewPaneDisplayMode navigation)
        {
            await ApplicationData.Current.LocalSettings.SaveAsync(SettingsKey, navigation.ToString());
        }

        #endregion
    }

    #endregion
}