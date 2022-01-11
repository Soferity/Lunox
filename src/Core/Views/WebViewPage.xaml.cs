#region Imports

using Lunox.Core.ViewModels;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Views
{
    #region WevViewPage

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class WebViewPage : Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public WebViewViewModel ViewModel { get; } = new WebViewViewModel();

        #region Main

        /// <summary>
        /// 
        /// </summary>
        public WebViewPage()
        {
            InitializeComponent();
            ViewModel.Initialize(webView);
        }

        #endregion

        #endregion
    }

    #endregion
}