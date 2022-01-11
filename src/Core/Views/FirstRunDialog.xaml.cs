#region Imports

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Views
{
    #region FirstRunDialog

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class FirstRunDialog : ContentDialog
    {
        #region Functions

        #region Main
        /// <summary>
        /// 
        /// </summary>
        public FirstRunDialog()
        {
            // TODO WTS: Update the contents of this dialog with any important information you want to show when the app is used for the first time.
            RequestedTheme = (Window.Current.Content as FrameworkElement).RequestedTheme;
            InitializeComponent();
        }

        #endregion

        #endregion
    }

    #endregion
}