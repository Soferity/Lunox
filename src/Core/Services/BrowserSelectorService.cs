#region Imports

using Lunox.Core.Helpers;
using Lunox.Library.Enum;
using Lunox.Library.Value;
using System;
using System.Threading.Tasks;
using Windows.Storage;

#endregion

namespace Lunox.Core.Services
{
    #region BrowserSelectorService

    /// <summary>
    /// 
    /// </summary>
    public static class BrowserSelectorService
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private static readonly string SettingsKey = Default.BrowserKey;

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public static BrowserType Browser { get; set; } = Default.DefaultBrowser;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task InitializeAsync()
        {
            Browser = await LoadBrowserFromSettingsAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public static async Task SetBrowserAsync(BrowserType browser)
        {
            Browser = browser;

            await SaveBrowserInSettingsAsync(Browser);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static async Task<BrowserType> LoadBrowserFromSettingsAsync()
        {
            BrowserType cacheBrowser = Default.DefaultBrowser;
            string browserName = await ApplicationData.Current.LocalSettings.ReadAsync<string>(SettingsKey);

            if (!string.IsNullOrEmpty(browserName))
            {
                Enum.TryParse(browserName, out cacheBrowser);
            }

            return cacheBrowser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        private static async Task SaveBrowserInSettingsAsync(BrowserType browser)
        {
            await ApplicationData.Current.LocalSettings.SaveAsync(SettingsKey, browser.ToString());
        }

        #endregion
    }

    #endregion
}