#region Imports

using Lunox.Core.Helpers;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;

#endregion

namespace Lunox.Core.ViewModels
{
    #region SharedDataWebLinkViewModel

    /// <summary>
    /// 
    /// </summary>
    public class SharedDataWebLinkViewModel : SharedDataViewModelBase
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private Uri _uri;

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public Uri Uri
        {
            get => _uri;
            set => SetProperty(ref _uri, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public SharedDataWebLinkViewModel()
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

            PageTitle = "ShareTarget_WebLinkTitle".GetLocalized();
            DataFormat = StandardDataFormats.WebLink;
            Uri = await shareOperation.GetWebLinkAsync();
        }

        #endregion
    }

    #endregion
}