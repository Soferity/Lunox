#region Imports

using Lunox.Core.Services;
using Lunox.Library.Enum;
using Lunox.Library.Value;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Web.WebView2.Core;
using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;
using WUXC = Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.ViewModels
{
    #region DiscordViewModel

    /// <summary>
    /// 
    /// </summary>
    public class DiscordViewModel : ObservableObject
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        private Uri _oldSource;

        /// <summary>
        /// 
        /// </summary>
        public Uri OldSource
        {
            get => _oldSource;
            set => SetProperty(ref _oldSource, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private Uri _newSource;

        /// <summary>
        /// 
        /// </summary>
        public Uri NewSource
        {
            get => _newSource;
            set => SetProperty(ref _newSource, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _isLoading;

        /// <summary>
        /// 
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;

            set
            {
                if (value)
                {
                    IsShowingFailedMessage = false;
                }

                SetProperty(ref _isLoading, value);
                IsLoadingVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private Visibility _isLoadingVisibility;

        /// <summary>
        /// 
        /// </summary>
        public Visibility IsLoadingVisibility
        {
            get => _isLoadingVisibility;
            set => SetProperty(ref _isLoadingVisibility, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _isShowingFailedMessage;

        /// <summary>
        /// 
        /// </summary>
        public bool IsShowingFailedMessage
        {
            get => _isShowingFailedMessage;

            set
            {
                if (value)
                {
                    IsLoading = false;
                }

                SetProperty(ref _isShowingFailedMessage, value);
                FailedMesageVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private Visibility _failedMesageVisibility;

        /// <summary>
        /// 
        /// </summary>
        public Visibility FailedMesageVisibility
        {
            get => _failedMesageVisibility;
            set => SetProperty(ref _failedMesageVisibility, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _oldNavCompleted;

        /// <summary>
        /// 
        /// </summary>
        public ICommand OldNavCompletedCommand
        {
            get
            {
                if (_oldNavCompleted == null)
                {
                    _oldNavCompleted = new RelayCommand<WUXC.WebViewNavigationCompletedEventArgs>(OldNavCompleted);
                }

                return _oldNavCompleted;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OldNavCompleted(WUXC.WebViewNavigationCompletedEventArgs e)
        {
            IsLoading = false;

            if (_webViewOld.Opacity == 0D)
            {
                _webViewOld.Width = double.NaN;
                _webViewOld.Height = double.NaN;
                _webViewOld.Opacity = 1D;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _newNavCompleted;

        /// <summary>
        /// 
        /// </summary>
        public ICommand NewNavCompletedCommand
        {
            get
            {
                if (_newNavCompleted == null)
                {
                    _newNavCompleted = new RelayCommand<CoreWebView2NavigationCompletedEventArgs>(NewNavCompleted);
                }

                return _newNavCompleted;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void NewNavCompleted(CoreWebView2NavigationCompletedEventArgs e)
        {
            IsLoading = false;

            if (e.IsSuccess)
            {
                if (_webViewNew.Opacity == 0D)
                {
                    _webViewNew.Width = double.NaN;
                    _webViewNew.Height = double.NaN;
                    _webViewNew.Opacity = 1D;
                    //_webViewNew.CoreWebView2.Settings.AreDevToolsEnabled = false;
                    //_webViewNew.CoreWebView2.Settings.IsZoomControlEnabled = false;
                    //_webViewNew.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                }
            }
            else
            {
                // Use `e.WebErrorStatus` to vary the displayed message based on the error reason
                IsShowingFailedMessage = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _oldNavFailed;

        /// <summary>
        /// 
        /// </summary>
        public ICommand OldNavFailedCommand
        {
            get
            {
                if (_oldNavFailed == null)
                {
                    _oldNavFailed = new RelayCommand<WUXC.WebViewNavigationFailedEventArgs>(OldNavFailed);
                }

                return _oldNavFailed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OldNavFailed(WUXC.WebViewNavigationFailedEventArgs e)
        {
            // Use `e.WebErrorStatus` to vary the displayed message based on the error reason
            IsShowingFailedMessage = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _retryCommand;

        /// <summary>
        /// 
        /// </summary>
        public ICommand RetryCommand
        {
            get
            {
                if (_retryCommand == null)
                {
                    _retryCommand = new RelayCommand(Retry);
                }

                return _retryCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Retry()
        {
            //IsShowingFailedMessage = false;
            //IsLoading = true;

            //_webView?.Refresh();

            if (NavigationService.Frame.SourcePageType != null)
            {
                NavigationService.Frame.NavigateToType(NavigationService.Frame.SourcePageType, null, new FrameNavigationOptions() { IsNavigationStackEnabled = false, TransitionInfoOverride = Default.ShellTransition });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private WUXC.WebView _webViewOld;

        /// <summary>
        /// 
        /// </summary>
        private MUXC.WebView2 _webViewNew;

        /// <summary>
        /// 
        /// </summary>
        public DiscordViewModel()
        {
            IsLoading = true;

            OldSource = Default.Discord;
            NewSource = Default.Discord;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webViewOld"></param>
        /// <param name="webViewNew"></param>
        public void Initialize(WUXC.WebView webViewOld, MUXC.WebView2 webViewNew)
        {
            if (BrowserSelectorService.Browser == BrowserType.WebView)
            {
                _webViewOld = webViewOld;
                _webViewOld.Source = Default.Discord;
            }
            else
            {
                _webViewNew = webViewNew;
                _webViewNew.Source = Default.Discord;
            }
        }

        #endregion
    }

    #endregion
}