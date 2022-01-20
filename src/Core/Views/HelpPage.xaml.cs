#region Imports

using Lunox.Core.ViewModels;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Views
{
    #region HelpPage

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class HelpPage : Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public HelpViewModel ViewModel { get; } = new HelpViewModel();

        /// <summary>
        /// 
        /// </summary>
        public HelpPage()
        {
            InitializeComponent();
        }

        #endregion
    }

    #endregion
}