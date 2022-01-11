#region Imports

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

#endregion

namespace Lunox.Core.Models
{
    #region ShareSourceData

    /// <summary>
    /// 
    /// </summary>
    public class ShareSourceData
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal List<ShareSourceItem> Items { get; }

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="desciption"></param>
        /// <exception cref="ArgumentException"></exception>
        public ShareSourceData(string title, string desciption = null)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("The parameter title can not be null or empty.", nameof(title));
            }

            Items = new List<ShareSourceItem>();
            Title = title;
            Description = desciption;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <exception cref="ArgumentException"></exception>
        public void SetText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("The parameter title can not be null or empty.", nameof(text));
            }

            Items.Add(ShareSourceItem.FromText(text));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webLink"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetWebLink(Uri webLink)
        {
            if (webLink == null)
            {
                throw new ArgumentNullException(nameof(webLink));
            }

            Items.Add(ShareSourceItem.FromWebLink(webLink));
        }

        /// <summary>
        /// To share a link to your app you must first register it to handle URI activation
        /// More details at https://docs.microsoft.com/windows/uwp/launch-resume/handle-uri-activation
        /// </summary>
        /// <param name="applicationLink"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetApplicationLink(Uri applicationLink)
        {
            if (applicationLink == null)
            {
                throw new ArgumentNullException(nameof(applicationLink));
            }

            Items.Add(ShareSourceItem.FromApplicationLink(applicationLink));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <exception cref="ArgumentException"></exception>
        public void SetHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                throw new ArgumentException("The Parameter html is null or empty.", nameof(html));
            }

            Items.Add(ShareSourceItem.FromHtml(html));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetImage(StorageFile image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            Items.Add(ShareSourceItem.FromImage(image));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storageItems"></param>
        /// <exception cref="ArgumentException"></exception>
        public void SetStorageItems(IEnumerable<IStorageItem> storageItems)
        {
            if (storageItems == null || !storageItems.Any())
            {
                throw new ArgumentException("The Parameter StorageItems is null or does not contains any element.", nameof(storageItems));
            }

            Items.Add(ShareSourceItem.FromStorageItems(storageItems));
        }

        /// <summary>
        /// Use this method to add content to share when you do not want to process the data until the target app actually requests it.
        /// The deferredDataFormatId parameter must be a const value from StandardDataFormats class.
        /// The getDeferredDataAsyncFunc parameter is the function that returns the object you want to share.
        /// </summary>
        /// <param name="deferredDataFormatId"></param>
        /// <param name="getDeferredDataAsyncFunc"></param>
        /// <exception cref="ArgumentException"></exception>
        public void SetDeferredContent(string deferredDataFormatId, Func<Task<object>> getDeferredDataAsyncFunc)
        {
            if (string.IsNullOrEmpty(deferredDataFormatId))
            {
                throw new ArgumentException("The Parameter DeferredDataFormatId is null or does not contains any element.", nameof(deferredDataFormatId));
            }

            Items.Add(ShareSourceItem.FromDeferredContent(deferredDataFormatId, getDeferredDataAsyncFunc));
        }

        #endregion
    }

    #endregion
}