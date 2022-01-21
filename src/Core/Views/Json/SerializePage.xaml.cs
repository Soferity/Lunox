#region Imports

using Lunox.Core.ViewModels.Json;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Views.Json
{
    #region SerializePage

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SerializePage : Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public SerializeViewModel ViewModel { get; } = new SerializeViewModel();

        /// <summary>
        /// 
        /// </summary>
        public SerializePage()
        {
            InitializeComponent();
        }

        #endregion
    }

    #endregion
}