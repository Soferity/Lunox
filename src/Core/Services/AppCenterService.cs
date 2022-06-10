#region Imports

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Channel;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Windows.System.UserProfile;
using WUX = Windows.UI.Xaml;

#endregion

namespace Lunox.Core.Services
{
    #region AppCenterService

    /// <summary>
    /// 
    /// </summary>
    public static class AppCenterService
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private const string SECRET = "e9e63317-6348-4bca-82d1-6756998cd803";

        #endregion

        #region Functions

        #region Public Functions

        /// <summary>
        /// 
        /// </summary>
        public static void Engine()
        {
            Enabled(); //
            Level(); //
            User();
            Country();
            Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public static void Exception(WUX.UnhandledExceptionEventArgs e)
        {
            //TrackError(e.Exception, "Message", e.Message);

            /*
            if (SHOWN)
            {
                SHOWN = false;
                Dialog.SendDialog(e.Exception.Message);
            }
            */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public static void Exception(Exception e)
        {
            //TrackError(e);

            /*
            if (SHOWN)
            {
                SHOWN = false;
                Dialog.SendDialog(e.Exception.Message);
            }
            */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        public static void TrackEvent(string Name)
        {
            Analytics.TrackEvent(Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public static void TrackEvent(string Name, string Key, string Value)
        {
            Analytics.TrackEvent(Name, new Dictionary<string, string>() { { Key, Value } });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Properties"></param>
        public static void TrackEvent(string Name, IDictionary<string, string> Properties = null)
        {
            Analytics.TrackEvent(Name, Properties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Exception"></param>
        public static void TrackError(Exception Exception)
        {
            Crashes.TrackError(Exception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Exception"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public static void TrackError(Exception Exception, string Key, string Value)
        {
            Crashes.TrackError(Exception, new Dictionary<string, string>() { { Key, Value } });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Exception"></param>
        /// <param name="Properties"></param>
        public static void TrackError(Exception Exception, IDictionary<string, string> Properties = null)
        {
            Crashes.TrackError(Exception, Properties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Exception"></param>
        /// <param name="Properties"></param>
        /// <param name="Text"></param>
        /// <param name="TextFile"></param>
        /// <param name="Image"></param>
        /// <param name="ImageFile"></param>
        /// <param name="Extension"></param>
        public static void TrackError(Exception Exception, IDictionary<string, string> Properties = null, string Text = "Hello World!", string TextFile = "Hello.txt", string Image = "Fake Image", string ImageFile = "fake_image.jpeg", string Extension = "image/jpeg")
        {
            TrackError(Exception, Properties, new ErrorAttachmentLog[]
            {
                ErrorAttachmentLog.AttachmentWithText(Text, TextFile),
                ErrorAttachmentLog.AttachmentWithBinary(Encoding.UTF8.GetBytes(Image), ImageFile, Extension)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Exception"></param>
        /// <param name="Properties"></param>
        /// <param name="Text"></param>
        /// <param name="TextFile"></param>
        /// <param name="Image"></param>
        /// <param name="ImageFile"></param>
        /// <param name="Extension"></param>
        public static void TrackError(Exception Exception, IDictionary<string, string> Properties = null, string Text = "Hello World!", string TextFile = "Hello.txt", byte[] Image = null, string ImageFile = "fake_image.jpeg", string Extension = "image/jpeg")
        {
            Crashes.TrackError(Exception, Properties, new ErrorAttachmentLog[]
            {
                ErrorAttachmentLog.AttachmentWithText(Text, TextFile),
                ErrorAttachmentLog.AttachmentWithBinary(Image, ImageFile, Extension)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Exception"></param>
        /// <param name="Properties"></param>
        /// <param name="Attachments"></param>
        public static void TrackError(Exception Exception, IDictionary<string, string> Properties = null, ErrorAttachmentLog[] Attachments = null)
        {
            Crashes.TrackError(Exception, Properties, Attachments);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// 
        /// </summary>
        private static void Start()
        {
            AppCenter.Start(SECRET, typeof(Analytics), typeof(Crashes), typeof(Channel));
        }

        /// <summary>
        /// 
        /// </summary>
        private static void Country()
        {
            AppCenter.SetCountryCode(GlobalizationPreferences.HomeGeographicRegion);
            //AppCenter.SetCountryCode(RegionInfo.CurrentRegion.TwoLetterISORegionName);
            //AppCenter.SetCountryCode(CultureInfo.InstalledUICulture.TwoLetterISOLanguageName);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void User()
        {
            string Name = WindowsIdentity.GetCurrent().Name;
            string Unique = "Unknown";

            if (Name.Contains("\\"))
            {
                Name = Name.Split('\\')[1];
            }
            else
            {
                Name = Environment.MachineName;
            }

            if (string.IsNullOrEmpty(Name))
            {
                Name = Environment.UserName;
            }

            string Try = WindowsIdentity.GetCurrent().User.Value;

            if (!string.IsNullOrEmpty(Try) && Try.Contains("-") && Try.Split('-').Length >= 6)
            {
                Unique = WindowsIdentity.GetCurrent().User.Value.Split('-')[5];
            }

            AppCenter.SetUserId($"{Name}-{Unique}");
        }

        /// <summary>
        /// 
        /// </summary>
        private static void Level()
        {
            AppCenter.LogLevel = LogLevel.Verbose;
        }

        /// <summary>
        /// 
        /// </summary>
        private static void Enabled()
        {
            Crashes.SetEnabledAsync(true);
        }

        #endregion

        #endregion
    }

    #endregion
}