#region Imports

using Lunox.Services;
using Lunox.ViewModels;
using WUIX = Windows.UI.Xaml.Controls;
using MUIX = Microsoft.UI.Xaml.Controls;

#endregion

namespace Lunox.Views
{
    #region ShellPage

    /// <summary>
    /// TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    /// </summary>
    public sealed partial class ShellPage : WUIX.Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        /// <summary>
        /// 
        /// </summary>
        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void navigationView_SelectionChanged(MUIX.NavigationView sender, MUIX.NavigationViewSelectionChangedEventArgs args)
        {
            MUIX.NavigationViewItem SelectedItem = (MUIX.NavigationViewItem)args.SelectedItem;

            if (SelectedItem.Tag == null)
            {
                AppCenterService.TrackEvent($"{SelectedItem.Content}");
            }
            else
            {
                AppCenterService.TrackEvent($"{SelectedItem.Tag}");
            }
        }

        #endregion
    }

    #endregion
}