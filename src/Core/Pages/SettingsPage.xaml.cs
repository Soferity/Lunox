using Lunox.Core.Util;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Lunox.Pages
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void Tema_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.Theme == ApplicationTheme.Light)
            {
                Settings.Theme = ApplicationTheme.Dark;
            }
            else
            {
                Settings.Theme = ApplicationTheme.Light;
            }
        }

        private void Dil_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.Language == "en-GB")
            {
                Settings.Language = "tr-TR";
            }
            else
            {
                Settings.Language = "en-GB";
            }
        }
    }
}