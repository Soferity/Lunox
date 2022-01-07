using Lunox.Services;
using Lunox.ViewModels;
using WUIX = Windows.UI.Xaml.Controls;
using MUIX = Microsoft.UI.Xaml.Controls;

namespace Lunox.Views
{
    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : WUIX.Page
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
        }

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
    }
}
