#region Imports

using Lunox.Cores.Util;
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

        #endregion

        #endregion

        #endregion
    }

    #endregion
}