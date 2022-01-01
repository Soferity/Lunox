using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using Windows.UI.Xaml.Controls;

namespace Lunox.Pages
{
    public sealed partial class GitHubPage : Page
    {
        public GitHubPage()
        {
            InitializeComponent();
        }

        private void WebView_NavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            if (WebView.Opacity == 0D)
            {
                WebView.Opacity = 1D;
                //WebView.CoreWebView2.Settings.AreDevToolsEnabled = false;
                //WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
                //WebView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            }
        }
    }
}