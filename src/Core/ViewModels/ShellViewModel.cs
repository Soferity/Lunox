#region Imports

using Lunox.Helpers;
using Lunox.Services;
using Lunox.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using MUIX = Microsoft.UI.Xaml.Controls;
using WUIX = Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.ViewModels
{
    #region ShellViewModel

    /// <summary>
    /// 
    /// </summary>
    public class ShellViewModel : ObservableObject
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        private readonly KeyboardAccelerator _altLeftKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu);

        /// <summary>
        /// 
        /// </summary>
        private readonly KeyboardAccelerator _backKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.GoBack);

        /// <summary>
        /// 
        /// </summary>
        private bool _isBackEnabled;

        /// <summary>
        /// 
        /// </summary>
        private IList<KeyboardAccelerator> _keyboardAccelerators;

        /// <summary>
        /// 
        /// </summary>
        private MUIX.NavigationView _navigationView;

        /// <summary>
        /// 
        /// </summary>
        private MUIX.NavigationViewItem _selected;

        /// <summary>
        /// 
        /// </summary>
        private ICommand _loadedCommand;

        /// <summary>
        /// 
        /// </summary>
        private ICommand _itemInvokedCommand;

        /// <summary>
        /// 
        /// </summary>
        public bool IsBackEnabled
        {
            get => _isBackEnabled;
            set => SetProperty(ref _isBackEnabled, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public MUIX.NavigationViewItem Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

        /// <summary>
        /// 
        /// </summary>
        public ICommand ItemInvokedCommand => _itemInvokedCommand ?? (_itemInvokedCommand = new RelayCommand<MUIX.NavigationViewItemInvokedEventArgs>(OnItemInvoked));

        /// <summary>
        /// 
        /// </summary>
        public ShellViewModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="navigationView"></param>
        /// <param name="keyboardAccelerators"></param>
        public void Initialize(WUIX.Frame frame, MUIX.NavigationView navigationView, IList<KeyboardAccelerator> keyboardAccelerators)
        {
            _navigationView = navigationView;
            _keyboardAccelerators = keyboardAccelerators;
            NavigationService.Frame = frame;
            NavigationService.NavigationFailed += Frame_NavigationFailed;
            NavigationService.Navigated += Frame_Navigated;
            _navigationView.BackRequested += OnBackRequested;
        }

        /// <summary>
        /// 
        /// </summary>
        private async void OnLoaded()
        {
            // Keyboard accelerators are added here to avoid showing 'Alt + left' tooltip on the page.
            // More info on tracking issue https://github.com/Microsoft/microsoft-ui-xaml/issues/8
            _keyboardAccelerators.Add(_altLeftKeyboardAccelerator);
            _keyboardAccelerators.Add(_backKeyboardAccelerator);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        private void OnItemInvoked(MUIX.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavigationService.Navigate(typeof(SettingsPage), null, args.RecommendedNavigationTransitionInfo);
            }
            else
            {
                MUIX.NavigationViewItem selectedItem = args.InvokedItemContainer as MUIX.NavigationViewItem;
                Type pageType = selectedItem?.GetValue(NavHelper.NavigateToProperty) as Type;

                if (pageType != null)
                {
                    NavigationService.Navigate(pageType, null, args.RecommendedNavigationTransitionInfo);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnBackRequested(MUIX.NavigationView sender, MUIX.NavigationViewBackRequestedEventArgs args)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw e.Exception;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            IsBackEnabled = NavigationService.CanGoBack;
            if (e.SourcePageType == typeof(SettingsPage))
            {
                Selected = _navigationView.SettingsItem as MUIX.NavigationViewItem;
                return;
            }

            MUIX.NavigationViewItem selectedItem = GetSelectedItem(_navigationView.MenuItems, e.SourcePageType);
            if (selectedItem != null)
            {
                Selected = selectedItem;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuItems"></param>
        /// <param name="pageType"></param>
        /// <returns></returns>
        private MUIX.NavigationViewItem GetSelectedItem(IEnumerable<object> menuItems, Type pageType)
        {
            foreach (MUIX.NavigationViewItem item in menuItems.OfType<MUIX.NavigationViewItem>())
            {
                if (IsMenuItemForPageType(item, pageType))
                {
                    return item;
                }

                MUIX.NavigationViewItem selectedChild = GetSelectedItem(item.MenuItems, pageType);
                if (selectedChild != null)
                {
                    return selectedChild;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="sourcePageType"></param>
        /// <returns></returns>
        private bool IsMenuItemForPageType(MUIX.NavigationViewItem menuItem, Type sourcePageType)
        {
            Type pageType = menuItem.GetValue(NavHelper.NavigateToProperty) as Type;
            return pageType == sourcePageType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
        {
            KeyboardAccelerator keyboardAccelerator = new KeyboardAccelerator() { Key = key };
            if (modifiers.HasValue)
            {
                keyboardAccelerator.Modifiers = modifiers.Value;
            }

            keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;
            return keyboardAccelerator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            bool result = NavigationService.GoBack();
            args.Handled = result;
        }

        #endregion
    }

    #endregion
}