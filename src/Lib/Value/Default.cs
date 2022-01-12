#region Imports

using Lunox.Core.Views;
using Lunox.Language.Enum;
using Lunox.Library.Enum;
using Microsoft.Services.Store.Engagement;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

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
        public static IReadOnlyList<string> DefaultLanguages = ApplicationLanguages.ManifestLanguages;

        /// <summary>
        /// 
        /// </summary>
        public static string BrowserKey => "AppRequestedBrowser";

        /// <summary>
        /// 
        /// </summary>
        public static BrowserType DefaultBrowser => BrowserType.WebView;

        /// <summary>
        /// 
        /// </summary>
        public static string NavigationKey => "AppRequestedNavigation";

        /// <summary>
        /// 
        /// </summary>
        public static NavigationViewPaneDisplayMode DefaultNavigation => NavigationViewPaneDisplayMode.Auto;

        /// <summary>
        /// 
        /// </summary>
        public static string GlanceKey => "AppRequestedGlance";

        /// <summary>
        /// 
        /// </summary>
        public static Type DefaultGlance => typeof(GlancePage);

        /// <summary>
        /// 
        /// </summary>
        public static string DefaultGlanceClass => DefaultGlance.ToString();

        /// <summary>
        /// 
        /// </summary>
        public static string DefaultGlancePrefix => string.Join(".", DefaultGlanceClass.Split('.').SkipLast(1)) + ".";

        /// <summary>
        /// 
        /// </summary>
        public static UIElement DefaultShell => new ShellPage();

        /// <summary>
        /// 
        /// </summary>
        public static NavigationTransitionInfo ShellTransition => new DrillInNavigationTransitionInfo();

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<EventType, string> Events => new()
        {
            { EventType.Page, "Page-" },
            { EventType.Theme, "Switch-Theme" },
            { EventType.Glance, "Switch-Glance" },
            { EventType.Browser, "Switch-Browser" },
            { EventType.Language, "Switch-Language" },
            { EventType.Navigation, "Switch-Navigation" }
        };

        /// <summary>
        /// 
        /// </summary>
        public static Visibility FeedbackLinkVisibility => StoreServicesFeedbackLauncher.IsSupported() ? Visibility.Collapsed : Visibility.Collapsed;

        /// <summary>
        /// 
        /// </summary>
        public static Visibility PrivacyLinkVisibility => Visibility.Collapsed;

        /// <summary>
        /// 
        /// </summary>
        public static Uri PrivacyTerms => new("https://github.com/Soferity/Lunox/blob/develop/CODE_OF_CONDUCT.md");

        #endregion
    }

    #endregion
}