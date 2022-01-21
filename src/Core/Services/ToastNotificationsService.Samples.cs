#region Imports

using Lunox.Core.Helpers;
using Microsoft.Toolkit.Uwp.Notifications;

using Windows.UI.Notifications;

#endregion

namespace Lunox.Core.Services
{
    #region ToastNotificationsService

    /// <summary>
    /// 
    /// </summary>
    internal partial class ToastNotificationsService
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public void ShowToastNotificationSample()
        {
            string _sampleResource = "Toast|Sample";

            // Create the toast content
            ToastContent content = new()
            {
                // More about the Launch property at https://docs.microsoft.com/dotnet/api/microsoft.toolkit.uwp.notifications.toastcontent
                Launch = ResourceExtensions.GetLocalizedLaunch(_sampleResource),

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = ResourceExtensions.GetLocalizedTitle(_sampleResource)
                            },

                            new AdaptiveText()
                            {
                                Text = ResourceExtensions.GetLocalizedMessage(_sampleResource)
                            }
                        }
                    }
                },

                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        // More about Toast Buttons at https://docs.microsoft.com/dotnet/api/microsoft.toolkit.uwp.notifications.toastbutton
                        new ToastButton(ResourceExtensions.GetLocalizedContent("Toast|Okay"), ResourceExtensions.GetLocalizedArgument("Toast|Okay"))
                        {
                            ActivationType = ToastActivationType.Foreground
                        },

                        new ToastButtonDismiss(ResourceExtensions.GetLocalizedContent("Toast|Dismiss"))
                    }
                }
            };

            // Add the content to the toast
            ToastNotification toast = new(content.GetXml())
            {
                // TODO WTS: Set a unique identifier for this notification within the notification group. (optional)
                // More details at https://docs.microsoft.com/uwp/api/windows.ui.notifications.toastnotification.tag
                Tag = ResourceExtensions.GetLocalizedTag(_sampleResource)
            };

            // And show the toast
            ShowToastNotification(toast);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowToastNotificationReminder()
        {
            string _reminderResource = "Toast|Reminder";

            // Create the toast content
            ToastContent content = new()
            {
                // More about the Launch property at https://docs.microsoft.com/dotnet/api/microsoft.toolkit.uwp.notifications.toastcontent
                Launch = ResourceExtensions.GetLocalizedLaunch(_reminderResource),

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = ResourceExtensions.GetLocalizedTitle(_reminderResource)
                            },

                            new AdaptiveText()
                            {
                                 Text = ResourceExtensions.GetLocalizedMessage(_reminderResource)
                            }
                        }
                    }
                }
            };

            // Add the content to the toast
            ToastNotification toast = new(content.GetXml())
            {
                // TODO WTS: Set a unique identifier for this notification within the notification group. (optional)
                // More details at https://docs.microsoft.com/uwp/api/windows.ui.notifications.toastnotification.tag
                Tag = ResourceExtensions.GetLocalizedTag(_reminderResource)
            };

            // And show the toast
            ShowToastNotification(toast);
        }

        #endregion
    }

    #endregion
}