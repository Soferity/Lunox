using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Lunox.ViewModels
{
    public class WebViewViewModel : ObservableObject
    {
        // TODO WTS: Set the URI of the page to show by default
        private const string DefaultUrl = "https://docs.microsoft.com/windows/apps/";

        private Uri _source;

        public Uri Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        private bool _isLoading;

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

        private Visibility _isLoadingVisibility;

        public Visibility IsLoadingVisibility
        {
            get => _isLoadingVisibility;
            set => SetProperty(ref _isLoadingVisibility, value);
        }

        private bool _isShowingFailedMessage;

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

        private Visibility _failedMesageVisibility;

        public Visibility FailedMesageVisibility
        {
            get => _failedMesageVisibility;
            set => SetProperty(ref _failedMesageVisibility, value);
        }

        private ICommand _navCompleted;

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

        private void NavCompleted(WebViewNavigationCompletedEventArgs e)
        {
            IsLoading = false;
            OnPropertyChanged(nameof(BrowserBackCommand));
            OnPropertyChanged(nameof(BrowserForwardCommand));
        }

        private ICommand _navFailed;

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

        private void NavFailed(WebViewNavigationFailedEventArgs e)
        {
            // Use `e.WebErrorStatus` to vary the displayed message based on the error reason
            IsShowingFailedMessage = true;
        }

        private ICommand _retryCommand;

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

        private void Retry()
        {
            IsShowingFailedMessage = false;
            IsLoading = true;

            _webView?.Refresh();
        }

        private ICommand _browserBackCommand;

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

        private ICommand _browserForwardCommand;

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

        private ICommand _refreshCommand;

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

        private ICommand _openInBrowserCommand;

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

        private WebView _webView;

        public WebViewViewModel()
        {
            IsLoading = true;
            Source = new Uri(DefaultUrl);
        }

        public void Initialize(WebView webView)
        {
            _webView = webView;
        }
    }
}
