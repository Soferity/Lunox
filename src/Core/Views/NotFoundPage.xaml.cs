#region Imports

using Lunox.Core.ViewModels;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Views
{
    #region NotFoundPage

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class NotFoundPage : Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public NotFoundViewModel ViewModel { get; } = new NotFoundViewModel();

        /// <summary>
        /// 
        /// </summary>
        public NotFoundPage()
        {
            InitializeComponent();
        }

        #endregion
    }

    #endregion
}