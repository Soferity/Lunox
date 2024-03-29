﻿#region Imports

using Lunox.Library.Helper;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using Windows.UI.Xaml;

#endregion

namespace Lunox.Library.Util
{
    #region SettingsUtil

    /// <summary>
    /// 
    /// </summary>
    public static class Settings
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private static readonly ApplicationDataContainer AppSettings = ApplicationData.Current.LocalSettings;

        #endregion

        #region Functions

        #region Helper Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        private static bool GetKey(string Key)
        {
            try
            {
                if (AppSettings.Values[Key] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        private static object GetValue(string Key)
        {
            if (GetKey(Key))
            {
                return null;
            }
            else
            {
                return AppSettings.Values[Key];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        private static bool KeyValue(string Key, object Value)
        {
            try
            {
                if (AppSettings.Values[Key] == Value)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        private static void SetKey(string Key)
        {
            AppSettings.Values[Key] = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        private static void SetValue(string Key, object Value)
        {
            AppSettings.Values[Key] = Value;
        }

        #endregion

        #region Setting Functions

        /// <summary>
        /// 
        /// </summary>
        public static ApplicationTheme Theme
        {
            get
            {
                if (GetKey("Theme"))
                {
                    SetValue("Theme", (int)ApplicationTheme.Dark);
                }

                return (ApplicationTheme)GetValue("Theme");
            }
            set
            {
                if (KeyValue("Theme", (int)value))
                {
                    SetValue("Theme", (int)value);
                    Dialog.SendDialog("Ayarların geçerli olabilmesi için uygulamayı yeniden başlatmanız gerekmektedir.", "Uygulama yeniden başlatılsın mı?");
                    Helper.Restart.RestartRequest();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Language
        {
            get
            {
                if (GetKey("Language"))
                {
                    Language = "en-GB";
                }

                return (string)GetValue("Language");
            }
            set
            {
                if (KeyValue("Language", value))
                {
                    SetValue("Language", value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Restart
        {
            get
            {
                if (GetKey("Restart"))
                {
                    Restart = "Restart";
                }

                return (string)AppSettings.Values["Restart"];
            }
            set
            {
                if (KeyValue("Restart", value))
                {
                    SetValue("Restart", value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Glance
        {
            get
            {
                if (GetKey("Glance"))
                {
                    Glance = "Welcome";
                }

                return (string)AppSettings.Values["Glance"];
            }
            set
            {
                if (KeyValue("Glance", value))
                {
                    SetValue("Glance", value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Browser
        {
            get
            {
                if (GetKey("Browser"))
                {
                    Browser = "WebView";
                }

                return (string)AppSettings.Values["Browser"];
            }
            set
            {
                if (KeyValue("Browser", value))
                {
                    SetValue("Browser", value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static NavigationViewPaneDisplayMode Navigation
        {
            get
            {
                if (GetKey("Navigation"))
                {
                    Navigation = NavigationViewPaneDisplayMode.Left;
                }

                return (NavigationViewPaneDisplayMode)GetValue("Navigation");
            }
            set
            {
                if (KeyValue("Navigation", (int)value))
                {
                    SetValue("Navigation", (int)value);
                }
            }
        }

        #endregion

        #endregion
    }

    #endregion
}