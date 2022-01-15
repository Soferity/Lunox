#region Imports

using Lunox.Core.ViewModels;
using Microsoft.Web.WebView2.Core;
using MUXC = Microsoft.UI.Xaml.Controls;
using WUXC = Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Views
{
    #region GitHubPage

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class GitHubPage : WUXC.Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public GitHubViewModel ViewModel { get; } = new GitHubViewModel();

        /// <summary>
        /// 
        /// </summary>
        public GitHubPage()
        {
            InitializeComponent();
            ViewModel.Initialize(WebViewOld, WebViewNew);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void WebViewNew_NavigationCompleted(MUXC.WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            ViewModel.NewNavCompletedCommand.Execute(args);
        }

        #endregion
    }

    #endregion
}