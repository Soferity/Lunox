#region Imports

using Lunox.Core.ViewModels;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Views
{
    #region DiscordPage

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class DiscordPage : Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public DiscordViewModel ViewModel { get; } = new DiscordViewModel();

        #region Main

        /// <summary>
        /// 
        /// </summary>
        public DiscordPage()
        {
            InitializeComponent();
            ViewModel.Initialize(WebViewOld, WebViewNew);
        }

        #endregion

        #endregion
    }

    #endregion
}