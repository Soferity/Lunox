#region Imports

using Lunox.Core.ViewModels;
using System.Linq;
using Windows.UI.Xaml;
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
            repeater2.ItemsSource = Enumerable.Range(0, 500);
        }

        #endregion
    }

    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Normal { get; set; }
        public DataTemplate Accent { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if ((int)item % 2 == 0)
            {
                return Normal;
            }
            else
            {
                return Accent;
            }
        }
    }

    #endregion
}