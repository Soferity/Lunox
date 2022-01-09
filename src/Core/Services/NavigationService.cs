﻿#region Imports

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

#endregion

namespace Lunox.Services
{
    #region NavigationService

    /// <summary>
    /// 
    /// </summary>
    public static class NavigationService
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        public static event NavigatedEventHandler Navigated;

        /// <summary>
        /// 
        /// </summary>
        public static event NavigationFailedEventHandler NavigationFailed;

        /// <summary>
        /// 
        /// </summary>
        private static Frame _frame;
        /// <summary>
        /// 
        /// </summary>
        private static object _lastParamUsed;

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public static Frame Frame
        {
            get
            {
                if (_frame == null)
                {
                    _frame = Window.Current.Content as Frame;
                    RegisterFrameEvents();
                }

                return _frame;
            }

            set
            {
                UnregisterFrameEvents();
                _frame = value;
                RegisterFrameEvents();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool CanGoBack => Frame.CanGoBack;

        /// <summary>
        /// 
        /// </summary>
        public static bool CanGoForward => Frame.CanGoForward;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool GoBack()
        {
            if (CanGoBack)
            {
                Frame.GoBack();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void GoForward()
        {
            Frame.GoForward();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="parameter"></param>
        /// <param name="infoOverride"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool Navigate(Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            if (pageType == null || !pageType.IsSubclassOf(typeof(Page)))
            {
                throw new ArgumentException($"Invalid pageType '{pageType}', please provide a valid pageType.", nameof(pageType));
            }

            // Don't open the same page multiple times
            if (Frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParamUsed)))
            {
                bool navigationResult = Frame.Navigate(pageType, parameter, infoOverride);
                if (navigationResult)
                {
                    _lastParamUsed = parameter;
                }

                return navigationResult;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="infoOverride"></param>
        /// <returns></returns>
        public static bool Navigate<T>(object parameter = null, NavigationTransitionInfo infoOverride = null) where T : Page
        {
            return Navigate(typeof(T), parameter, infoOverride);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void RegisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated += Frame_Navigated;
                _frame.NavigationFailed += Frame_NavigationFailed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void UnregisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated -= Frame_Navigated;
                _frame.NavigationFailed -= Frame_NavigationFailed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            NavigationFailed?.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            Navigated?.Invoke(sender, e);
        }

        #endregion
    }

    #endregion
}