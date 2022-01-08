#region Imports

using Lunox.ViewModels;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Views
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