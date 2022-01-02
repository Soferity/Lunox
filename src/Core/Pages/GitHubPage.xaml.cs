﻿#region Imports

using Lunox.Cores.Util;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Pages
{
    #region GitHubPagePages

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class GitHubPage : Page
    {
        #region Functions

        #region Main Function

        /// <summary>
        /// 
        /// </summary>
        public GitHubPage()
        {
            InitializeComponent();

            string Uri = "https://github.com/Taiizor/ReaLTaiizor";

            if (Settings.Browser == "WebView")
            {
                WebView.Source = new Uri(Uri);
            }
            else
            {
                WebViewM.Source = new Uri(Uri);
            }
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
        private void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (WebView.Opacity == 0D)
            {
                WebView.Opacity = 1D;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void WebViewM_NavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            if (WebViewM.Opacity == 0D)
            {
                WebViewM.Opacity = 1D;
                //WebViewM.CoreWebView2.Settings.AreDevToolsEnabled = false;
                //WebViewM.CoreWebView2.Settings.IsZoomControlEnabled = false;
                //WebViewM.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            }
        }

        #endregion

        #endregion

        #endregion
    }

    #endregion
}