#region Imports

using Lunox.Library.Helper;
using Lunox.Library.Value;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

#endregion

namespace Lunox.Core.Helpers
{
    #region SettingsStorageExtensions

    /// <summary>
    /// Use these extension methods to store and retrieve local and roaming app data
    /// More details regarding storing and retrieving app data at https://docs.microsoft.com/windows/uwp/app-settings/store-and-retrieve-app-data
    /// </summary>
    public static class SettingsStorageExtensions
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private static readonly string FileExtension = Default.StorageExtension;

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appData"></param>
        /// <returns></returns>
        public static bool IsRoamingStorageAvailable(this ApplicationData appData)
        {
            return appData.RoamingStorageQuota == 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task SaveAsync<T>(this StorageFolder folder, string name, T content)
        {
            StorageFile file = await folder.CreateFileAsync(GetFileName(name), CreationCollisionOption.ReplaceExisting);
            string fileContent = await Json.StringifyAsync(content);

            await FileIO.WriteTextAsync(file, fileContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<T> ReadAsync<T>(this StorageFolder folder, string name)
        {
            if (!File.Exists(Path.Combine(folder.Path, GetFileName(name))))
            {
                return default;
            }

            StorageFile file = await folder.GetFileAsync($"{name}{FileExtension}");
            string fileContent = await FileIO.ReadTextAsync(file);

            return await Json.ToObjectAsync<T>(fileContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task SaveAsync<T>(this ApplicationDataContainer settings, string key, T value)
        {
            settings.SaveString(key, await Json.StringifyAsync(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SaveString(this ApplicationDataContainer settings, string key, string value)
        {
            settings.Values[key] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> ReadAsync<T>(this ApplicationDataContainer settings, string key)
        {

            if (settings.Values.TryGetValue(key, out object obj))
            {
                return await Json.ToObjectAsync<T>((string)obj);
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<StorageFile> SaveFileAsync(this StorageFolder folder, byte[] content, string fileName, CreationCollisionOption options = CreationCollisionOption.ReplaceExisting)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("File name is null or empty. Specify a valid file name", nameof(fileName));
            }

            StorageFile storageFile = await folder.CreateFileAsync(fileName, options);
            await FileIO.WriteBytesAsync(storageFile, content);
            return storageFile;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<byte[]> ReadFileAsync(this StorageFolder folder, string fileName)
        {
            IStorageItem item = await folder.TryGetItemAsync(fileName).AsTask().ConfigureAwait(false);

            if ((item != null) && item.IsOfType(StorageItemTypes.File))
            {
                StorageFile storageFile = await folder.GetFileAsync(fileName);
                byte[] content = await storageFile.ReadBytesAsync();
                return content;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<byte[]> ReadBytesAsync(this StorageFile file)
        {
            if (file != null)
            {
                using IRandomAccessStream stream = await file.OpenReadAsync();
                using DataReader reader = new(stream.GetInputStreamAt(0));
                await reader.LoadAsync((uint)stream.Size);
                byte[] bytes = new byte[stream.Size];
                reader.ReadBytes(bytes);
                return bytes;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetFileName(string name)
        {
            return string.Concat(name, FileExtension);
        }

        #endregion
    }

    #endregion
}