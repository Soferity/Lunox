#region Imports

using Lunox.Core.ViewModels;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Views
{
    #region GlancePage

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class GlancePage : Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public GlanceViewModel ViewModel { get; } = new GlanceViewModel();

        /// <summary>
        /// 
        /// </summary>
        public GlancePage()
        {
            InitializeComponent();
        }

        #endregion
    }

    #endregion
}