using System;
using Windows.UI.Xaml.Controls;

namespace Lunox.Views
{
    public sealed partial class MainPage : Page
    {
        private string ActivatePage = null;
        private string ActivateControl = null;

        private readonly string PagePrefix = "Lunox.Pages.";
        private readonly string PageSuffix = "Page";

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
                ContentFrame.Navigate(Type.GetType(ActivatePage));
                SendDialog("Changed", ActivatePage);
            }
        }

        private void SendDialog(string Title = "Title", string Content = "Content")
        {
            try
            {
                _ = new ContentDialog
                {
                    Title = Title,
                    Content = Content,
                    CloseButtonText = "Çıkış",
                    PrimaryButtonText = "Öncelik",
                    IsPrimaryButtonEnabled = true,
                    IsSecondaryButtonEnabled = true
                }.ShowAsync();
            }
            catch
            {
                //
            }
        }
    }
}