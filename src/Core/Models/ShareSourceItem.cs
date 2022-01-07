#region Imports

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

#endregion

namespace Lunox.Models
{
    #region ShareSourceItemType

    /// <summary>
    /// 
    /// </summary>
    internal enum ShareSourceItemType
    {
        /// <summary>
        /// 
        /// </summary>
        Text = 0,
        /// <summary>
        /// 
        /// </summary>
        WebLink = 1,
        /// <summary>
        /// 
        /// </summary>
        ApplicationLink = 2,
        /// <summary>
        /// 
        /// </summary>
        Html = 3,
        /// <summary>
        /// 
        /// </summary>
        Image = 4,
        /// <summary>
        /// 
        /// </summary>
        StorageItems = 5,
        /// <summary>
        /// 
        /// </summary>
        DeferredContent = 6
    }

    #endregion

    #region ShareSourceItem

    /// <summary>
    /// 
    /// </summary>
    internal class ShareSourceItem
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public ShareSourceItemType DataType { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Uri WebLink { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Uri ApplicationLink { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Html { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public StorageFile Image { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IStorageItem> StorageItems { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string DeferredDataFormatId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Func<Task<object>> GetDeferredDataAsyncFunc { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataType"></param>
        private ShareSourceItem(ShareSourceItemType dataType)
        {
            DataType = dataType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal static ShareSourceItem FromText(string text)
        {
            return new ShareSourceItem(ShareSourceItemType.Text)
            {
                Text = text
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webLink"></param>
        /// <returns></returns>
        internal static ShareSourceItem FromWebLink(Uri webLink)
        {
            return new ShareSourceItem(ShareSourceItemType.WebLink)
            {
                WebLink = webLink
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationLink"></param>
        /// <returns></returns>
        internal static ShareSourceItem FromApplicationLink(Uri applicationLink)
        {
            return new ShareSourceItem(ShareSourceItemType.ApplicationLink)
            {
                ApplicationLink = applicationLink
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        internal static ShareSourceItem FromHtml(string html)
        {
            return new ShareSourceItem(ShareSourceItemType.Html)
            {
                Html = html
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        internal static ShareSourceItem FromImage(StorageFile image)
        {
            return new ShareSourceItem(ShareSourceItemType.Image)
            {
                Image = image
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storageItems"></param>
        /// <returns></returns>
        internal static ShareSourceItem FromStorageItems(IEnumerable<IStorageItem> storageItems)
        {
            return new ShareSourceItem(ShareSourceItemType.StorageItems)
            {
                StorageItems = storageItems
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deferredDataFormatId"></param>
        /// <param name="getDeferredDataAsyncFunc"></param>
        /// <returns></returns>
        internal static ShareSourceItem FromDeferredContent(string deferredDataFormatId, Func<Task<object>> getDeferredDataAsyncFunc)
        {
            return new ShareSourceItem(ShareSourceItemType.DeferredContent)
            {
                DeferredDataFormatId = deferredDataFormatId,
                GetDeferredDataAsyncFunc = getDeferredDataAsyncFunc
            };
        }

        #endregion
    }

    #endregion
}