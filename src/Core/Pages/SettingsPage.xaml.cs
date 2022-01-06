#region Imports

using Lunox.Library.Util;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Pages
{
    #region SettingsPagePages

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        #region Functions

        #region Main Function

        /// <summary>
        /// 
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Helper Functions

        #region Normal Functions

        //

        #endregion

        #region Event Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Browser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Browser = Browser.SelectedItem as string;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Navigation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Navigation = (Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode)Navigation.SelectedIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            Browser.SelectedIndex = Browser.Items.ToList().IndexOf(Settings.Browser);
            Navigation.SelectedIndex = Navigation.Items.ToList().IndexOf(Settings.Navigation.ToString());
        }

        #endregion

        #endregion

        #endregion
    }

    #endregion
}