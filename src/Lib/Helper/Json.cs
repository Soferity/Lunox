#region Imports

using Newtonsoft.Json;
using System.Threading.Tasks;

#endregion

namespace Lunox.Library.Helper
{
    #region JsonHelper

    /// <summary>
    /// 
    /// </summary>
    public static class Json
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<T> ToObjectAsync<T>(string value)
        {
            return await Task.Run<T>(() =>
            {
                return JsonConvert.DeserializeObject<T>(value);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<string> StringifyAsync(object value)
        {
            return await Task.Run<string>(() =>
            {
                return JsonConvert.SerializeObject(value);
            });
        }

        #endregion
    }

    #endregion
}