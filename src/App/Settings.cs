using Lunox.Core.Helper;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Lunox
{
    public static class Settings
    {
        private static readonly ApplicationDataContainer AppSettings = ApplicationData.Current.LocalSettings;

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

        private static void SetKey(string Key)
        {
            AppSettings.Values[Key] = null;
        }

        private static void SetValue(string Key, object Value)
        {
            AppSettings.Values[Key] = Value;
        }

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
                    Core.Helper.Restart.RestartRequest();
                }
            }
        }

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

        public static string Welcome
        {
            get
            {
                if (GetKey("Welcome"))
                {
                    Welcome = "Welcome";
                }

                return (string)AppSettings.Values["Welcome"];
            }
            set
            {
                if (KeyValue("Welcome", value))
                {
                    SetValue("Welcome", value);
                }
            }
        }
    }
}