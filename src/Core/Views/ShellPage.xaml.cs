﻿#region Imports

using Lunox.Services;
using Lunox.ViewModels;
using Windows.ApplicationModel.Resources.Core;
using MUXC = Microsoft.UI.Xaml.Controls;
using WUXC = Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Views
{
    #region ShellPage

    /// <summary>
    /// TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    /// </summary>
    public sealed partial class ShellPage : WUXC.Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        /// <summary>
        /// 
        /// </summary>
        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void navigationView_SelectionChanged(MUXC.NavigationView sender, MUXC.NavigationViewSelectionChangedEventArgs args)
        {
            MUXC.NavigationViewItem SelectedItem = (MUXC.NavigationViewItem)args.SelectedItem;

            if (SelectedItem.Tag == null)
            {
                AppCenterService.TrackEvent($"{SelectedItem.Content}");
            }
            else
            {
                AppCenterService.TrackEvent($"{SelectedItem.Tag}");
            }
        }

        #endregion
    }

    #endregion
}