using Lunox.Core.Helper;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Lunox
{
    public static class Settings
    {
        private static readonly ApplicationDataContainer AppSettings = ApplicationData.Current.LocalSettings;

        public static ApplicationTheme Theme
        {
            get
            {
                if (AppSettings.Values["Theme"] == null)
                {
                    Theme = ApplicationTheme.Dark;
                }

                return (ApplicationTheme)AppSettings.Values["Theme"];
            }
            set
            {
                if ((int)AppSettings.Values["Theme"] != (int)value)
                {
                    AppSettings.Values["Theme"] = (int)value;
                    Dialog.SendDialog("Ayarların geçerli olabilmesi için uygulamayı yeniden başlatmanız gerekmektedir.", "Uygulama yeniden başlatılsın mı?");
                    Core.Helper.Restart.RestartRequest();
                }
            }
        }

        public static string Language
        {
            get
            {
                if (AppSettings.Values["Language"] == null)
                {
                    Language = "en-GB";
                }

                return (string)AppSettings.Values["Language"];
            }
            set => AppSettings.Values["Language"] = (string)value;
        }

        public static string Restart
        {
            get
            {
                if (AppSettings.Values["Restart"] == null)
                {
                    Restart = "Restart";
                }

                return (string)AppSettings.Values["Restart"];
            }
            set => AppSettings.Values["Restart"] = (string)value;
        }

        public static string Welcome
        {
            get
            {
                if (AppSettings.Values["Welcome"] == null)
                {
                    Welcome = "Welcome";
                }

                return (string)AppSettings.Values["Welcome"];
            }
            set => AppSettings.Values["Welcome"] = (string)value;
        }
    }
}