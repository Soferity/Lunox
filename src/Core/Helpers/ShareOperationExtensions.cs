#region Imports

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Storage;
using Windows.Storage.Streams;

#endregion

namespace Lunox.Helpers
{
    #region ShareOperationExtensions

    /// <summary>
    /// 
    /// </summary>
    public static class ShareOperationExtensions
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareOperation"></param>
        /// <returns></returns>
        public static async Task<Uri> GetApplicationLinkAsync(this ShareOperation shareOperation)
        {
            return await GetOperationDataAsync<Uri>(shareOperation, StandardDataFormats.ApplicationLink);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareOperation"></param>
        /// <returns></returns>
        public static async Task<RandomAccessStreamReference> GetBitmapAsync(this ShareOperation shareOperation)
        {
            return await GetOperationDataAsync<RandomAccessStreamReference>(shareOperation, StandardDataFormats.Bitmap);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareOperation"></param>
        /// <returns></returns>
        public static async Task<string> GetHtmlFormatAsync(this ShareOperation shareOperation)
        {
            return await GetOperationDataAsync<string>(shareOperation, StandardDataFormats.Html);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareOperation"></param>
        /// <returns></returns>
        public static async Task<string> GetRtfAsync(this ShareOperation shareOperation)
        {
            return await GetOperationDataAsync<string>(shareOperation, StandardDataFormats.Rtf);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareOperation"></param>
        /// <returns></returns>
        public static async Task<IReadOnlyList<IStorageItem>> GetStorageItemsAsync(this ShareOperation shareOperation)
        {
            return await GetOperationDataAsync<IReadOnlyList<IStorageItem>>(shareOperation, StandardDataFormats.StorageItems);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareOperation"></param>
        /// <returns></returns>
        public static async Task<string> GetTextAsync(this ShareOperation shareOperation)
        {
            return await GetOperationDataAsync<string>(shareOperation, StandardDataFormats.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareOperation"></param>
        /// <returns></returns>
        public static async Task<Uri> GetWebLinkAsync(this ShareOperation shareOperation)
        {
            return await GetOperationDataAsync<Uri>(shareOperation, StandardDataFormats.WebLink) as Uri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shareOperation"></param>
        /// <param name="dataFormat"></param>
        /// <returns></returns>
        private static async Task<T> GetOperationDataAsync<T>(this ShareOperation shareOperation, string dataFormat) where T : class
        {
            try
            {
                if (dataFormat == StandardDataFormats.ApplicationLink)
                {
                    return await shareOperation.Data.GetApplicationLinkAsync() as T;
                }

                if (dataFormat == StandardDataFormats.Bitmap)
                {
                    return await shareOperation.Data.GetBitmapAsync() as T;
                }

                if (dataFormat == StandardDataFormats.Html)
                {
                    return await shareOperation.Data.GetHtmlFormatAsync() as T;
                }

                if (dataFormat == StandardDataFormats.Rtf)
                {
                    return await shareOperation.Data.GetRtfAsync() as T;
                }

                if (dataFormat == StandardDataFormats.StorageItems)
                {
                    return await shareOperation.Data.GetStorageItemsAsync() as T;
                }

                if (dataFormat == StandardDataFormats.Text)
                {
                    return await shareOperation.Data.GetTextAsync() as T;
                }

                if (dataFormat == StandardDataFormats.WebLink)
                {
                    return await shareOperation.Data.GetWebLinkAsync() as T;
                }
            }
            catch (Exception)
            {
                return default;
            }

            return default;
        }

        #endregion
    }

    #endregion
}