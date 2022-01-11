#region Imports

using Lunox.Language.Enum;
using Lunox.Library.Enum;
using Lunox.Core.Views;
using Microsoft.Services.Store.Engagement;
using System;
using System.Collections.Generic;
using Windows.Globalization;
using Windows.UI.Xaml;

#endregion

namespace Lunox.Library.Value
{
    #region DefaultValue

    /// <summary>
    /// 
    /// </summary>
    public static class Default
    {
        #region Variables

        public static string StorageExtension => ".json";

        /// <summary>
        /// 
        /// </summary>
        public static string ThemeKey => "AppRequestedTheme";

        /// <summary>
        /// 
        /// </summary>
        public static ElementTheme DefaultTheme => ElementTheme.Default;

        /// <summary>
        /// 
        /// </summary>
        public static string LanguageKey => "AppRequestedLanguage";

        /// <summary>
        /// 
        /// </summary>
        public static LanguageType DefaultLanguage => LanguageType.en_GB;

        /// <summary>
        /// 
        /// </summary>
        public static IReadOnlyList<string> Languages = ApplicationLanguages.ManifestLanguages;

        /// <summary>
        /// 
        /// </summary>
        public static string GlanceKey => "AppRequestedGlance";

        /// <summary>
        /// 
        /// </summary>
        public static Type DefaultGlance => typeof(MainPage);

        /// <summary>
        /// 
        /// </summary>
        public static string DefaultGlanceClass => typeof(MainPage).ToString();

        /// <summary>
        /// 
        /// </summary>
        public static string DefaultGlancePrefix => "Lunox.Views.";

        /// <summary>
        /// 
        /// </summary>
        public static UIElement DefaultShell => new ShellPage();

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<EventType, string> Events => new()
        {
            { EventType.Page, "Page-" },
            { EventType.Theme, "Switch-Theme" },
            { EventType.Glance, "Switch-Glance" },
            { EventType.Language, "Switch-Language" }
        };

        /// <summary>
        /// 
        /// </summary>
        public static Visibility FeedbackLinkVisibility => StoreServicesFeedbackLauncher.IsSupported() ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// 
        /// </summary>
        public static Visibility PrivacyLinkVisibility => Visibility.Visible;

        /// <summary>
        /// 
        /// </summary>
        public static Uri PrivacyTerms => new("https://github.com/Soferity/Lunox/blob/develop/CODE_OF_CONDUCT.md");

        #endregion
    }

    #endregion
}