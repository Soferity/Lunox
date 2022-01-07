#region Imports

using Windows.ApplicationModel.Resources;

#endregion

namespace Lunox.Helpers
{
    #region ResourceExtensions

    /// <summary>
    /// 
    /// </summary>
    internal static class ResourceExtensions
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private static readonly ResourceLoader _resLoader = new ResourceLoader();

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalized(this string resourceKey)
        {
            return _resLoader.GetString(resourceKey);
        }

        #endregion
    }

    #endregion
}