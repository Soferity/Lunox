﻿#region Imports

using Lunox.Core.Services;
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

        /// <summary>
        /// 
        /// </summary>
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
        public static string NavigationSplit => "->";

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
        public static Type NotFoundPage => typeof(NotFoundPage);

        /// <summary>
        /// 
        /// </summary>
        public static Type[] NotRefresh => new[]
        {
            typeof(Nullable)
        };

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<EventType, string> Events => new()
        {
            { EventType.Page, "Page-" },
            { EventType.Theme, "Switch-Theme" },
            { EventType.Search, "Search-Page" },
            { EventType.Glance, "Switch-Glance" },
            { EventType.Browser, "Switch-Browser" },
            { EventType.Language, "Switch-Language" },
            { EventType.Navigation, "Switch-Navigation" }
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
        public static Dictionary<LanguageType, Uri> PrivacyTerms => new()
        {
            { LanguageType.en_GB, new("https://github.com/Soferity/Lunox/blob/develop/PRIVACY_POLICY.md") },
            { LanguageType.tr_TR, new("https://github.com/Soferity/Lunox/blob/develop/PRIVACY_POLICY.TR.md") },
            { LanguageType.es_ES, new("https://github.com/Soferity/Lunox/blob/develop/PRIVACY_POLICY.ES.md") }
        };

        /// <summary>
        /// 
        /// </summary>
        public static Uri GitHub => new("https://github.com/Soferity/Lunox");

        /// <summary>
        /// 
        /// </summary>
        public static Uri Discussions => new("https://github.com/Soferity/Lunox/discussions");

        /// <summary>
        /// 
        /// </summary>
        public static Uri Contribute => new("https://github.com/Soferity/Lunox/graphs/contributors");

        /// <summary>
        /// 
        /// </summary>
        public static Uri Bug => new("https://github.com/Soferity/Lunox/issues/new?assignees=&labels=&template=bug_report.md&title=");

        /// <summary>
        /// 
        /// </summary>
        public static Uri Feature => new("https://github.com/Soferity/Lunox/issues/new?assignees=&labels=&template=feature_request.md&title=");

        /// <summary>
        /// 
        /// </summary>
        public static Uri Discord => new($"https://discord.com/widget?id=932386235538878534&theme={ThemeSelectorService.Theme.ToString().ToLowerInvariant()}");

        #endregion
    }

    #endregion
}