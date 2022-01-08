#region Imports

using Lunox.ViewModels;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

#endregion

namespace Lunox.Views
{
    #region ShareTargetPage

    /// <summary>
    /// TODO WTS: Remove this example page when/if it's not needed.
    /// This page is an example of how to handle data that is shared with your app.
    /// You can either change this page to meet your needs, or use another and delete this page.
    /// </summary>
    public sealed partial class ShareTargetPage : Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public ShareTargetViewModel ViewModel { get; } = new ShareTargetViewModel();

        /// <summary>
        /// 
        /// </summary>
        public ShareTargetPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.LoadAsync(e.Parameter as ShareOperation);
        }

        #endregion
    }

    #endregion
}