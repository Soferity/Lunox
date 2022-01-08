#region Imports

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;

#endregion

namespace Lunox.ViewModels
{
    #region ShareTargetViewModel

    /// <summary>
    /// 
    /// </summary>
    public class ShareTargetViewModel : ObservableObject
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        private ShareOperation _shareOperation;

        /// <summary>
        /// 
        /// </summary>
        private SharedDataViewModelBase _sharedData;

        /// <summary>
        /// 
        /// </summary>
        public SharedDataViewModelBase SharedData
        {
            get => _sharedData;
            set => SetProperty(ref _sharedData, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _completeCommand;

        /// <summary>
        /// 
        /// </summary>
        public ICommand CompleteCommand => _completeCommand ?? (_completeCommand = new RelayCommand(OnComplete));

        /// <summary>
        /// 
        /// </summary>
        public ShareTargetViewModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareOperation"></param>
        /// <returns></returns>
        public async Task LoadAsync(ShareOperation shareOperation)
        {
            // TODO WTS: Configure the Share Target Declaration for the formats you require.
            // Share Target declarations are defined in Package.appxmanifest.
            // Current declarations allow to share WebLink and image files with the app.
            // ShareTarget can be tested sharing the WebLink from Microsoft Edge or sharing images from Windows Photos.

            // TODO WTS: Customize SharedDataModelBase or derived classes adding properties for data that you need to extract from _shareOperation
            _shareOperation = shareOperation;
            if (shareOperation.Data.Contains(StandardDataFormats.StorageItems))
            {
                SharedData = new SharedDataStorageItemsViewModel();
            }

            if (shareOperation.Data.Contains(StandardDataFormats.WebLink))
            {
                SharedData = new SharedDataWebLinkViewModel();
            }

            await SharedData?.LoadDataAsync(_shareOperation);
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnComplete()
        {
            // TODO WTS: Implement any other logic or add a QuickLink before completing the share operation.
            // More details at https://docs.microsoft.com/windows/uwp/app-to-app/receive-data
            _shareOperation.ReportCompleted();
        }

        #endregion
    }

    #endregion
}