#region Imports

using Lunox.Core.ViewModels;
using WUXC = Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Views
{
    #region ShellPage

    /// <summary>
    /// TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    /// </summary>
    public sealed partial class ShellPage : WUXC.Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        /// <summary>
        /// 
        /// </summary>
        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, suggestBox, KeyboardAccelerators);
        }

        #endregion
    }

    #endregion
}