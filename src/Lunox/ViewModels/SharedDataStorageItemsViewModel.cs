using Lunox.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Lunox.ViewModels
{
    public class SharedDataStorageItemsViewModel : SharedDataViewModelBase
    {
        public ObservableCollection<ImageSource> Images { get; } = new ObservableCollection<ImageSource>();

        public SharedDataStorageItemsViewModel()
        {
        }

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
    }
}
