#region Imports

using Lunox.Core.ViewModels;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.TemplateSelectors
{
    #region SharedContentTemplateSelector

    /// <summary>
    /// 
    /// </summary>
    public class SharedContentTemplateSelector : DataTemplateSelector
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public DataTemplate DefaultTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DataTemplate StorageItemsTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DataTemplate WebLinkTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SharedContentTemplateSelector()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is SharedDataViewModelBase sharedData)
            {
                if (sharedData.DataFormat == StandardDataFormats.WebLink)
                {
                    return WebLinkTemplate;
                }
                else if (sharedData.DataFormat == StandardDataFormats.StorageItems)
                {
                    return StorageItemsTemplate;
                }
            }

            return DefaultTemplate;
        }

        #endregion
    }

    #endregion
}