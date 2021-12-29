using Lunox.Core.Enum;
using Lunox.Pages;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

// Boş Sayfa öğe şablonu https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x41f adresinde açıklanmaktadır

namespace Lunox.Views
{
    /// <summary>
    /// Kendi başına kullanılabilecek ya da bir Çerçeve içine taşınabilecek boş bir sayfa.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            _ = new ContentDialog
            {
                Title = "TEST",
                Content = $"{ImageType.ARW}",
                CloseButtonText = "Close",
                IsPrimaryButtonEnabled = true,
                IsSecondaryButtonEnabled = true
            }.ShowAsync();

        }

        private void NavigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(SampleSettingsPage));
            }
            else
            {
                var selectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;
                if (selectedItem != null)
                {
                    string selectedItemTag = ((string)selectedItem.Tag);
                    sender.Header = "Sample Page " + selectedItemTag.Substring(selectedItemTag.Length - 1);
                    string pageName = "Lunox.Pages." + selectedItemTag;
                    Type pageType = Type.GetType(pageName);
                    //contentFrame.Navigate(pageType);
                }
            }
        }
    }
}