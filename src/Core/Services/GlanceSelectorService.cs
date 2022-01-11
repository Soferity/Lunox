#region Imports

using Lunox.Core.Helpers;
using Lunox.Library.Value;
using System;
using System.Threading.Tasks;
using Windows.Storage;

#endregion

namespace Lunox.Core.Services
{
    #region GlanceSelectorService

    /// <summary>
    /// 
    /// </summary>
    public static class GlanceSelectorService
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private static readonly string SettingsKey = Default.GlanceKey;

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public static Type Glance { get; set; } = Default.DefaultGlance;

        /// <summary>
        /// 
        /// </summary>
        public static string GlanceClass { get; set; } = Default.DefaultGlanceClass;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task InitializeAsync()
        {
            GlanceClass = await LoadGlanceFromSettingsAsync();
            Glance = Type.GetType(GlanceClass);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="glanceName"></param>
        /// <returns></returns>
        public static async Task SetGlanceAsync(string glanceName)
        {
            if (Type.GetType(glanceName) == null)
            {
                GlanceClass = Default.DefaultGlancePrefix + glanceName;
            }
            else
            {
                GlanceClass = glanceName;
            }
            
            if (Type.GetType(GlanceClass) == null)
            {
                GlanceClass = Default.DefaultGlanceClass;
            }

            Glance = Type.GetType(GlanceClass);
            await SaveGlanceInSettingsAsync(GlanceClass);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static async Task<string> LoadGlanceFromSettingsAsync()
        {
            string cacheGlanceClass = Default.DefaultGlanceClass;
            string glanceName = await ApplicationData.Current.LocalSettings.ReadAsync<string>(SettingsKey);

            if (!string.IsNullOrEmpty(glanceName))
            {
                if (Type.GetType(glanceName) != null)
                {
                    return glanceName;
                }
                else
                {
                    string glanceClassName = Default.DefaultGlancePrefix + glanceName;

                    if (Type.GetType(glanceClassName) != null)
                    {
                        return glanceClassName;
                    }
                }
            }

            return cacheGlanceClass;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="glanceClass"></param>
        /// <returns></returns>
        private static async Task SaveGlanceInSettingsAsync(string glanceClass)
        {
            await ApplicationData.Current.LocalSettings.SaveAsync(SettingsKey, glanceClass);
        }

        #endregion
    }

    #endregion
}