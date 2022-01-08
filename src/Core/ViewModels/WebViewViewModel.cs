#region Imports

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.ViewModels
{
    #region WebViewViewModel

    /// <summary>
    /// 
    /// </summary>
    public class WebViewViewModel : ObservableObject
    {
        #region Functions

        /// <summary>
        /// TODO WTS: Set the URI of the page to show by default
        /// </summary>
        private const string DefaultUrl = "https://docs.microsoft.com/windows/apps/";

        /// <summary>
        /// 
        /// </summary>
        private Uri _source;

        /// <summary>
        /// 
        /// </summary>
        public Uri Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
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
        private ICommand _navCompleted;

        /// <summary>
        /// 
        /// </summary>
        public ICommand NavCompletedCommand
        {
            get
            {
                if (_navCompleted == null)
                {
                    _navCompleted = new RelayCommand<WebViewNavigationCompletedEventArgs>(NavCompleted);
                }

                return _navCompleted;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void NavCompleted(WebViewNavigationCompletedEventArgs e)
        {
            IsLoading = false;
            OnPropertyChanged(nameof(BrowserBackCommand));
            OnPropertyChanged(nameof(BrowserForwardCommand));
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _navFailed;

        /// <summary>
        /// 
        /// </summary>
        public ICommand NavFailedCommand
        {
            get
            {
                if (_navFailed == null)
                {
                    _navFailed = new RelayCommand<WebViewNavigationFailedEventArgs>(NavFailed);
                }

                return _navFailed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void NavFailed(WebViewNavigationFailedEventArgs e)
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
            IsShowingFailedMessage = false;
            IsLoading = true;

            _webView?.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _browserBackCommand;

        /// <summary>
        /// 
        /// </summary>
        public ICommand BrowserBackCommand
        {
            get
            {
                if (_browserBackCommand == null)
                {
                    _browserBackCommand = new RelayCommand(() => _webView?.GoBack(), () => _webView?.CanGoBack ?? false);
                }

                return _browserBackCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _browserForwardCommand;

        /// <summary>
        /// 
        /// </summary>
        public ICommand BrowserForwardCommand
        {
            get
            {
                if (_browserForwardCommand == null)
                {
                    _browserForwardCommand = new RelayCommand(() => _webView?.GoForward(), () => _webView?.CanGoForward ?? false);
                }

                return _browserForwardCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _refreshCommand;

        /// <summary>
        /// 
        /// </summary>
        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new RelayCommand(() => _webView?.Refresh());
                }

                return _refreshCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private ICommand _openInBrowserCommand;

        /// <summary>
        /// 
        /// </summary>
        public ICommand OpenInBrowserCommand
        {
            get
            {
                if (_openInBrowserCommand == null)
                {
                    _openInBrowserCommand = new RelayCommand(async () => await Windows.System.Launcher.LaunchUriAsync(Source));
                }

                return _openInBrowserCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private WebView _webView;

        /// <summary>
        /// 
        /// </summary>
        public WebViewViewModel()
        {
            IsLoading = true;
            Source = new Uri(DefaultUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        public void Initialize(WebView webView)
        {
            _webView = webView;
        }

        #endregion
    }

    #endregion
}