#region Imports

using Lunox.Core.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#endregion

namespace Lunox.Core.ViewModels
{
    #region SharedDataStorageItemsViewModel

    /// <summary>
    /// 
    /// </summary>
    public class SharedDataStorageItemsViewModel : SharedDataViewModelBase
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ImageSource> Images { get; } = new ObservableCollection<ImageSource>();

        /// <summary>
        /// 
        /// </summary>
        public SharedDataStorageItemsViewModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareOperation"></param>
        /// <returns></returns>
        public override async Task LoadDataAsync(ShareOperation shareOperation)
        {
            await base.LoadDataAsync(shareOperation);

            PageTitle = "ShareTarget_ImagesTitle".GetLocalized();
            DataFormat = StandardDataFormats.StorageItems;
            System.Collections.Generic.IReadOnlyList<IStorageItem> files = await shareOperation.GetStorageItemsAsync();
            foreach (IStorageItem file in files)
            {
                StorageFile storageFile = file as StorageFile;
                if (storageFile != null)
                {
                    using (Windows.Storage.Streams.IRandomAccessStreamWithContentType inputStream = await storageFile.OpenReadAsync())
                    {
                        BitmapImage img = new BitmapImage();
                        img.SetSource(inputStream);
                        Images.Add(img);
                    }
                }
            }
        }

        #endregion
    }

    #endregion
}