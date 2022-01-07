using Lunox.ViewModels;
using Microsoft.AppCenter.Analytics;
using Windows.UI.Xaml.Controls;

namespace Lunox.Views
{
    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
        }

        private void navigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            Microsoft.UI.Xaml.Controls.NavigationViewItem SelectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;

            if (SelectedItem.Tag == null)
            {
                Analytics.TrackEvent($"{SelectedItem.Content}");
            }
            else
            {
                Analytics.TrackEvent($"{SelectedItem.Tag}");
            }
        }
    }
}
