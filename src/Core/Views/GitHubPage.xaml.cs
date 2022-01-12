#region Imports

using Lunox.Core.ViewModels;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Views
{
    #region GitHubPage

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class GitHubPage : Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public GitHubViewModel ViewModel { get; } = new GitHubViewModel();

        #region Main

        /// <summary>
        /// 
        /// </summary>
        public GitHubPage()
        {
            InitializeComponent();
            ViewModel.Initialize(WebViewOld, WebViewNew);
        }

        #endregion

        #endregion
    }

    #endregion
}