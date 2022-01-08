#region Imports

using Lunox.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

#endregion

namespace Lunox.Views
{
    #region SettingsPage

    /// <summary>
    /// TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public SettingsViewModel ViewModel { get; } = new SettingsViewModel();

        /// <summary>
        /// 
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeAsync();
        }

        #endregion
    }

    #endregion
}