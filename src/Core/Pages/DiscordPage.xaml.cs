#region Imports

using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Pages
{
    #region DiscordPagePages

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class DiscordPage : Page
    {
        #region Functions

        #region Main Function

        /// <summary>
        /// 
        /// </summary>
        public DiscordPage()
        {
            InitializeComponent();
            WebView.Source = new Uri($"https://discord.com/widget?id=587709969852268564&theme={App.Current.RequestedTheme.ToString().ToLowerInvariant()}");
        }

        #endregion

        #region Helper Functions

        #region Normal Functions

        //

        #endregion

        #region Event Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void WebView_NavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            if (WebView.Opacity == 0D)
            {
                WebView.Opacity = 1D;
                //WebView.CoreWebView2.Settings.AreDevToolsEnabled = false;
                //WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
                //WebView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            }
        }

        #endregion

        #endregion

        #endregion
    }

    #endregion
}