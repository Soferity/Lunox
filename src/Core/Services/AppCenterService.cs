#region Imports

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Channel;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using WUX = Windows.UI.Xaml;

#endregion

namespace Lunox.Services
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
        private const string SECRET = "APP-SECRET";

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
            TrackError(e.Exception, Error(e.Exception, e.Message));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public static void Exception(Exception e)
        {
            TrackError(e, Error(e));
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
            AppCenter.SetCountryCode(RegionInfo.CurrentRegion.TwoLetterISORegionName);
            //AppCenter.SetCountryCode(CultureInfo.InstalledUICulture.TwoLetterISOLanguageName);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void User()
        {
            string Name = WindowsIdentity.GetCurrent().Name;

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

            Name += "-" + WindowsIdentity.GetCurrent().User.Value.Split('-')[5];

            AppCenter.SetUserId(Name);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Exception"></param>
        /// <returns></returns>
        private static Dictionary<string, string> Error(Exception Exception, string Message = null)
        {
            try
            {
                Dictionary<string, string> Properties = new()
                {
                    { "Data", $"{Exception.Data}" },
                    { "Source", Exception.Source },
                    { "HResult", $"{Exception.HResult}" },
                    { "Message", Exception.Message },
                    { "HelpLink", Exception.HelpLink },
                    { "StackTrace", Exception.StackTrace }
                };

                if (Message != null)
                {
                    Properties.Add("Description", Message);
                }

                if (Exception.TargetSite != null)
                {
                    Properties.Add("TargetSite->Name", Exception.TargetSite.Name);
                    Properties.Add("TargetSite->Module->Name", Exception.TargetSite.Module.Name);
                    Properties.Add("TargetSite->Module->ScopeName", Exception.TargetSite.Module.ScopeName);
                    Properties.Add("TargetSite->Module->FullyQualifiedName", Exception.TargetSite.Module.FullyQualifiedName);
                    Properties.Add("TargetSite->DeclaringType", $"{Exception.TargetSite.DeclaringType}");
                }

                if (Exception.InnerException != null)
                {
                    Properties.Add("InnerException->Data", $"{Exception.InnerException.Data}");
                    Properties.Add("InnerException->Source", Exception.InnerException.Source);
                    Properties.Add("InnerException->HResult", $"{Exception.InnerException.HResult}");
                    Properties.Add("InnerException->Message", Exception.InnerException.Message);
                    Properties.Add("InnerException->HelpLink", Exception.InnerException.HelpLink);
                    Properties.Add("InnerException->StackTrace", Exception.InnerException.StackTrace);
                    Properties.Add("InnerException->TargetSite->Name", Exception.InnerException.TargetSite.Name);
                    Properties.Add("InnerException->TargetSite->Module->Name", Exception.InnerException.TargetSite.Module.Name);
                    Properties.Add("InnerException->TargetSite->Module->ScopeName", Exception.InnerException.TargetSite.Module.ScopeName);
                    Properties.Add("InnerException->TargetSite->Module->FullyQualifiedName", Exception.InnerException.TargetSite.Module.FullyQualifiedName);
                    Properties.Add("InnerException->TargetSite->DeclaringType", $"{Exception.InnerException.TargetSite.DeclaringType}");
                }

                return Properties;
            }
            catch
            {
                try
                {
                    return new Dictionary<string, string>()
                    {
                        { "Data", $"{Exception.Data}" },
                        { "Source", Exception.Source },
                        { "HResult", $"{Exception.HResult}" },
                        { "Message", Exception.Message },
                        { "HelpLink", Exception.HelpLink },
                        { "StackTrace", Exception.StackTrace }
                    };
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #endregion
    }

    #endregion
}