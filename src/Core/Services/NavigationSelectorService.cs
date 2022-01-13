#region Imports

using Lunox.Core.Helpers;
using Lunox.Library.Value;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;
using WUXC = Windows.UI.Xaml.Controls;

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
        public static MUXC.NavigationViewPaneDisplayMode Navigation { get; set; } = Default.DefaultNavigation;

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
        public static async Task SetNavigationAsync(MUXC.NavigationViewPaneDisplayMode navigation)
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
        public static Task SetNavigationLanguage(bool Mode = false)
        {
            if (NavigationService.Navigation != null)
            {
                foreach (MUXC.NavigationViewItem Item in NavigationService.Navigation.MenuItems.OfType<MUXC.NavigationViewItem>().Concat(NavigationService.Navigation.FooterMenuItems.OfType<MUXC.NavigationViewItem>()))
                {
                    if (Item.MenuItems.Count > 0)
                    {
                        foreach (MUXC.NavigationViewItem Menu in Item.MenuItems.OfType<MUXC.NavigationViewItem>())
                        {
                            SetNavigationContent(Menu);
                        }
                    }

                    SetNavigationContent(Item);
                }

                foreach (MUXC.NavigationViewItemHeader Item in NavigationService.Navigation.MenuItems.OfType<MUXC.NavigationViewItemHeader>())
                {
                    SetNavigationContent(Item);
                }

                SetNavigationContent(NavigationService.Navigation.AutoSuggestBox);
            }

            SetFrameLanguage(Mode);

            //NavigationService.Display = NavigationSelectorService.Navigation;

            return Task.CompletedTask;
        }

        public static void SetFrameLanguage(bool Mode)
        {
            if (Mode && NavigationService.Frame.SourcePageType != null)
            {
                NavigationService.Frame.NavigateToType(NavigationService.Frame.SourcePageType, null, new FrameNavigationOptions() { IsNavigationStackEnabled = false, TransitionInfoOverride = Default.ShellTransition });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Item"></param>
        private static void SetNavigationContent(MUXC.NavigationViewItem Item)
        {
            Item.Content = ResourceExtensions.GetLocalizedContent($"Shell|{Item.Tag}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Item"></param>
        private static void SetNavigationContent(MUXC.NavigationViewItemHeader Item)
        {
            Item.Content = ResourceExtensions.GetLocalizedContent($"Shell|{Item.Tag}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Item"></param>
        private static void SetNavigationContent(WUXC.AutoSuggestBox Suggest)
        {
            Suggest.PlaceholderText = ResourceExtensions.GetLocalizedPlaceholderText($"Shell|{Suggest.Tag}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static async Task<MUXC.NavigationViewPaneDisplayMode> LoadNavigationFromSettingsAsync()
        {
            MUXC.NavigationViewPaneDisplayMode cacheNavigation = Default.DefaultNavigation;
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
        private static async Task SaveNavigationInSettingsAsync(MUXC.NavigationViewPaneDisplayMode navigation)
        {
            await ApplicationData.Current.LocalSettings.SaveAsync(SettingsKey, navigation.ToString());
        }

        #endregion
    }

    #endregion
}