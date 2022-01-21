#region Imports

using Windows.ApplicationModel.Resources;

#endregion

namespace Lunox.Core.Helpers
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
        private static readonly ResourceLoader _resLoader = new();

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalized(this object resourceKey)
        {
            return GetLocalized($"{resourceKey}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedTag(this object resourceKey)
        {
            return GetLocalized($"{resourceKey}.Tag");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedText(this object resourceKey)
        {
            return GetLocalized($"{resourceKey}.Text");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedTitle(this object resourceKey)
        {
            return GetLocalized($"{resourceKey}.Title");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedLaunch(this object resourceKey)
        {
            return GetLocalized($"{resourceKey}.Launch");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedMessage(this object resourceKey)
        {
            return GetLocalized($"{resourceKey}.Message");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedArgument(this object resourceKey)
        {
            return GetLocalized($"{resourceKey}.Argument");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedCustom(this object resourceKey)
        {
            return GetLocalized($"{resourceKey}.Custom");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedContent(this object resourceKey)
        {
            return GetLocalized($"{resourceKey}.Content");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedPlaceholderText(this object resourceKey)
        {
            return GetLocalized($"{resourceKey}.PlaceholderText");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedTag(this string resourceKey)
        {
            return GetLocalized($"{resourceKey}.Tag");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedText(this string resourceKey)
        {
            return GetLocalized($"{resourceKey}.Text");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedTitle(this string resourceKey)
        {
            return GetLocalized($"{resourceKey}.Title");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedLaunch(this string resourceKey)
        {
            return GetLocalized($"{resourceKey}.Launch");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedMessage(this string resourceKey)
        {
            return GetLocalized($"{resourceKey}.Message");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedArgument(this string resourceKey)
        {
            return GetLocalized($"{resourceKey}.Argument");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedCustom(this string resourceKey)
        {
            return GetLocalized($"{resourceKey}.Custom");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedContent(this string resourceKey)
        {
            return GetLocalized($"{resourceKey}.Content");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalizedPlaceholderText(this string resourceKey)
        {
            return GetLocalized($"{resourceKey}.PlaceholderText");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetLocalized(this string resourceKey)
        {
            string Key = resourceKey.Replace('.', '/');
            string Value = _resLoader.GetString(Key);

            if (string.IsNullOrEmpty(Value))
            {
                if (Key.Contains("|"))
                {
                    string[] Keygen = Key.Split('|');

                    ResourceLoader Loader = new(Keygen[0]);
                    Value = Loader.GetString(Keygen[1]);
                }
            }

            return string.IsNullOrEmpty(Value) ? $"[{resourceKey}]" : Value;
        }

        #endregion
    }

    #endregion
}