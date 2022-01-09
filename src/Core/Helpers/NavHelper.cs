#region Imports

using Microsoft.UI.Xaml.Controls;
using System;
using Windows.UI.Xaml;

#endregion

namespace Lunox.Helpers
{
    #region NavHelper

    /// <summary>
    /// 
    /// </summary>
    public class NavHelper
    {
        #region Functions

        /// <summary>
        /// This helper class allows to specify the page that will be shown when you click on a NavigationViewItem
        ///
        /// Usage in xaml:
        /// <muxc:NavigationViewItem x:Uid="Shell_Main" Icon="Document" helpers:NavHelper.NavigateTo="views:MainPage" />
        ///
        /// Usage in code:
        /// NavHelper.SetNavigateTo(navigationViewItem, typeof(MainPage));
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Type GetNavigateTo(NavigationViewItem item)
        {
            return (Type)item.GetValue(NavigateToProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        public static void SetNavigateTo(NavigationViewItem item, Type value)
        {
            item.SetValue(NavigateToProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty NavigateToProperty = DependencyProperty.RegisterAttached("NavigateTo", typeof(Type), typeof(NavHelper), new PropertyMetadata(null));

        #endregion
    }

    #endregion
}