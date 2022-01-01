using Lunox.Core.Util;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Lunox.Views
{
    public sealed partial class MainPage : Page
    {
        private string ActivatePage = null;
        private string ActivateControl = null;

        private readonly string PagePrefix = "Lunox.Pages.";
        private readonly string PageSuffix = "Page";

        private bool IsRestart = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void NavigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ActivateControl = $"{PagePrefix}Settings{PageSuffix}";
            }
            else
            {
                Microsoft.UI.Xaml.Controls.NavigationViewItem SelectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;

                string ItemTag = $"{SelectedItem.Tag}";
                sender.Header = $"Activate Page {ItemTag}";

                string PageName = $"{PagePrefix}{ItemTag}{PageSuffix}";

                if (Type.GetType(PageName) == null)
                {
                    PageName = $"{PagePrefix}NotFound{PageSuffix}";
                }

                ActivateControl = PageName;
            }

            if (ActivatePage != ActivateControl)
            {
                ActivatePage = ActivateControl;
                ContentFrame.Navigate(Type.GetType(ActivatePage), null, new DrillInNavigationTransitionInfo());
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter is string Parameter)
            {
                if (Parameter == Settings.Restart)
                {
                    IsRestart = true;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsRestart)
            {
                NavigationView.SelectedItem = NavigationView.SettingsItem;
            }
            else
            {
                NavigationView.SelectedItem = NavigationView.MenuItems
                         .OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>()
                         .Where(Item => Item.Tag.ToString() == Settings.Welcome)
                         .First();
            }
        }
    }
}