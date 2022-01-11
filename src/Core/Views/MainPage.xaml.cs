#region Imports

using Lunox.Library.Util;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

#endregion

namespace Lunox.Core.Views
{
    #region MainPageViews

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private string ActivatePage = null;
        /// <summary>
        /// 
        /// </summary>
        private string ActivateControl = null;

        /// <summary>
        /// 
        /// </summary>
        private readonly string PagePrefix = "Lunox.Pages.";
        /// <summary>
        /// 
        /// </summary>
        private readonly string PageSuffix = "Page";

        /// <summary>
        /// 
        /// </summary>
        private bool IsRestart = false;

        #endregion

        #region Functions

        #region Main Function

        /// <summary>
        /// 
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            NavigationView.PaneDisplayMode = Settings.Navigation;
        }

        #endregion

        #region Helper Functions

        #region Normal Functions

        //

        #endregion

        #region Event Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ActivateControl = $"{PagePrefix}Settings{PageSuffix}";
            }
            else
            {
                Microsoft.UI.Xaml.Controls.NavigationViewItem SelectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;

                string ItemTag = $"{SelectedItem.Tag}";
                sender.Header = $"Activate Page {ItemTag}";

                string PageName = $"{PagePrefix}{ItemTag}{PageSuffix}";

                if (Type.GetType(PageName) == null)
                {
                    PageName = $"{PagePrefix}NotFound{PageSuffix}";
                }

                ActivateControl = PageName;
            }

            if (ActivatePage != ActivateControl)
            {
                ActivatePage = ActivateControl;
                ContentFrame.Navigate(Type.GetType(ActivatePage), null, new DrillInNavigationTransitionInfo());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter is string Parameter)
            {
                if (Parameter == Settings.Restart)
                {
                    IsRestart = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsRestart)
            {
                NavigationView.SelectedItem = NavigationView.SettingsItem;
            }
            else
            {
                NavigationView.SelectedItem = NavigationView.MenuItems
                         .OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>()
                         .Where(Item => Item.Tag.ToString() == Settings.Glance)
                         .First();
            }
        }

        #endregion

        #endregion

        #endregion
    }

    #endregion
}