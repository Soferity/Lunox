#region Imports

using Lunox.Library.Util;
using MUIX = Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System;
using WUIX = Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Pages
{
    #region GitHubPagePages

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class GitHubPage : WUIX.Page
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
                WebViewOld.Source = new Uri(Uri);
            }
            else
            {
                WebViewNew.Source = new Uri(Uri);
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
        private void WebViewOld_NavigationCompleted(WUIX.WebView sender, WUIX.WebViewNavigationCompletedEventArgs args)
        {
            if (WebViewOld.Opacity == 0D)
            {
                WebViewOld.Width = double.NaN;
                WebViewOld.Height = double.NaN;
                WebViewOld.Opacity = 1D;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void WebViewNew_NavigationCompleted(MUIX.WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            if (WebViewNew.Opacity == 0D)
            {
                WebViewNew.Width = double.NaN;
                WebViewNew.Height = double.NaN;
                WebViewNew.Opacity = 1D;
                //WebViewNew.CoreWebView2.Settings.AreDevToolsEnabled = false;
                //WebViewNew.CoreWebView2.Settings.IsZoomControlEnabled = false;
                //WebViewNew.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            }
        }

        #endregion

        #endregion

        #endregion
    }

    #endregion
}