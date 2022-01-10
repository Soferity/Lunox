#region Imports

using Lunox.Library.Enum;
using Lunox.Library.Value;
using Lunox.Services;
using Lunox.ViewModels;
using MUXC = Microsoft.UI.Xaml.Controls;
using WUXC = Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Views
{
    #region ShellPage

    /// <summary>
    /// TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    /// </summary>
    public sealed partial class ShellPage : WUXC.Page
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
        private void navigationView_SelectionChanged(MUXC.NavigationView sender, MUXC.NavigationViewSelectionChangedEventArgs args)
        {
            MUXC.NavigationViewItem SelectedItem = (MUXC.NavigationViewItem)args.SelectedItem;

            if (SelectedItem.Tag == null)
            {
                AppCenterService.TrackEvent($"{Default.Events[EventType.Page]}{SelectedItem.Content}");
            }
            else
            {
                AppCenterService.TrackEvent($"{Default.Events[EventType.Page]}{SelectedItem.Tag}");
            }
        }

        #endregion
    }

    #endregion
}