#region Imports

using Lunox.ViewModels;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.TemplateSelectors
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
            SharedDataViewModelBase sharedData = item as SharedDataViewModelBase;
            if (sharedData != null)
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