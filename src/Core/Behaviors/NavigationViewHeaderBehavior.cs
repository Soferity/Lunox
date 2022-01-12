#region Imports

using Lunox.Core.Services;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;
using WUXC = Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Behaviors
{
    #region NavigationViewHeaderBehavior

    /// <summary>
    /// 
    /// </summary>
    public class NavigationViewHeaderBehavior : Behavior<MUXC.NavigationView>
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private static NavigationViewHeaderBehavior _current;

        /// <summary>
        /// 
        /// </summary>
        private WUXC.Page _currentPage;

        /// <summary>
        /// 
        /// </summary>
        public DataTemplate DefaultHeaderTemplate { get; set; }

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public object DefaultHeader
        {
            get => GetValue(DefaultHeaderProperty);
            set => SetValue(DefaultHeaderProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty DefaultHeaderProperty = DependencyProperty.Register("DefaultHeader", typeof(object), typeof(NavigationViewHeaderBehavior), new PropertyMetadata(null, (d, e) => _current.UpdateHeader()));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static NavigationViewHeaderMode GetHeaderMode(WUXC.Page item)
        {
            return (NavigationViewHeaderMode)item.GetValue(HeaderModeProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        public static void SetHeaderMode(WUXC.Page item, NavigationViewHeaderMode value)
        {
            item.SetValue(HeaderModeProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty HeaderModeProperty = DependencyProperty.RegisterAttached("HeaderMode", typeof(bool), typeof(NavigationViewHeaderBehavior), new PropertyMetadata(NavigationViewHeaderMode.Always, (d, e) => _current.UpdateHeader()));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static object GetHeaderContext(WUXC.Page item)
        {
            return item.GetValue(HeaderContextProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        public static void SetHeaderContext(WUXC.Page item, object value)
        {
            item.SetValue(HeaderContextProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty HeaderContextProperty = DependencyProperty.RegisterAttached("HeaderContext", typeof(object), typeof(NavigationViewHeaderBehavior), new PropertyMetadata(null, (d, e) => _current.UpdateHeader()));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static DataTemplate GetHeaderTemplate(WUXC.Page item)
        {
            return (DataTemplate)item.GetValue(HeaderTemplateProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        public static void SetHeaderTemplate(WUXC.Page item, DataTemplate value)
        {
            item.SetValue(HeaderTemplateProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.RegisterAttached("HeaderTemplate", typeof(DataTemplate), typeof(NavigationViewHeaderBehavior), new PropertyMetadata(null, (d, e) => _current.UpdateHeaderTemplate()));

        /// <summary>
        /// 
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            _current = this;
            NavigationService.Navigated += OnNavigated;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            NavigationService.Navigated -= OnNavigated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            WUXC.Frame frame = sender as WUXC.Frame;
            if (frame.Content is WUXC.Page page)
            {
                _currentPage = page;

                UpdateHeader();
                UpdateHeaderTemplate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateHeader()
        {
            if (_currentPage != null)
            {
                NavigationViewHeaderMode headerMode = GetHeaderMode(_currentPage);
                if (headerMode == NavigationViewHeaderMode.Never)
                {
                    AssociatedObject.Header = null;
                    AssociatedObject.AlwaysShowHeader = false;
                }
                else
                {
                    object headerFromPage = GetHeaderContext(_currentPage);
                    if (headerFromPage != null)
                    {
                        AssociatedObject.Header = headerFromPage;
                    }
                    else
                    {
                        AssociatedObject.Header = DefaultHeader;
                    }

                    if (headerMode == NavigationViewHeaderMode.Always)
                    {
                        AssociatedObject.AlwaysShowHeader = true;
                    }
                    else
                    {
                        AssociatedObject.AlwaysShowHeader = false;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateHeaderTemplate()
        {
            if (_currentPage != null)
            {
                DataTemplate headerTemplate = GetHeaderTemplate(_currentPage);
                AssociatedObject.HeaderTemplate = headerTemplate ?? DefaultHeaderTemplate;
            }
        }

        #endregion
    }

    #endregion
}