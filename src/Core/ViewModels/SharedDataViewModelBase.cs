#region Imports

using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer.ShareTarget;

#endregion

namespace Lunox.ViewModels
{
    #region SharedDataViewModelBase

    /// <summary>
    /// 
    /// </summary>
    public class SharedDataViewModelBase : ObservableObject
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        private string _dataFormat;

        /// <summary>
        /// 
        /// </summary>
        public string DataFormat
        {
            get => _dataFormat;
            set => SetProperty(ref _dataFormat, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private string _pageTitle;

        /// <summary>
        /// 
        /// </summary>
        public string PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private string _title;

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public SharedDataViewModelBase()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareOperation"></param>
        /// <returns></returns>
        public virtual async Task LoadDataAsync(ShareOperation shareOperation)
        {
            Title = shareOperation.Data.Properties.Title;
            await Task.CompletedTask;
        }

        #endregion
    }

    #endregion
}