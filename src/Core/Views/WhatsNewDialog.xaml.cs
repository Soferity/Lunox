#region Imports

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Views
{
    #region WhatsNewDialog

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class WhatsNewDialog : ContentDialog
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public WhatsNewDialog()
        {
            // TODO WTS: Update the contents of this dialog every time you release a new version of the app
            RequestedTheme = (Window.Current.Content as FrameworkElement).RequestedTheme;
            InitializeComponent();
        }

        #endregion
    }

    #endregion
}