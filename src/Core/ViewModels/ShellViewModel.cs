#region Imports

using Lunox.Core.Helpers;
using Lunox.Core.Services;
using Lunox.Core.Views;
using Lunox.Library.Enum;
using Lunox.Library.Value;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;
using WUXC = Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.ViewModels
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
        private MUXC.NavigationView _navigationView;

        /// <summary>
        /// 
        /// </summary>
        private WUXC.AutoSuggestBox _suggestBox;

        /// <summary>
        /// 
        /// </summary>
        private MUXC.NavigationViewItem _selected;

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
        public MUXC.NavigationViewItem Selected
        {
            get => _selected;
            set
            {
                if (_selected != value && value.MenuItems.Count == 0)
                {
                    if (value.Tag == null)
                    {
                        AppCenterService.TrackEvent($"{Default.Events[EventType.Page]}Unknown", "Content", $"{value.Content}");
                    }
                    else
                    {
                        AppCenterService.TrackEvent($"{Default.Events[EventType.Page]}{value.Tag}");
                    }
                }

                SetProperty(ref _selected, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mode"></param>
        /// <returns></returns>
        private List<MUXC.NavigationViewItem> Navigation_Item(bool Mode = true)
        {
            List<MUXC.NavigationViewItem> Items = new();

            if (_navigationView.MenuItems.Count > 0)
            {
                foreach (MUXC.NavigationViewItem Item in _navigationView.MenuItems.OfType<MUXC.NavigationViewItem>().Concat(_navigationView.FooterMenuItems.OfType<MUXC.NavigationViewItem>()))
                {
                    if (Mode && Item.MenuItems.Count > 0)
                    {
                        foreach (MUXC.NavigationViewItem Menu in Item.MenuItems.OfType<MUXC.NavigationViewItem>())
                        {
                            Items.Add(Menu);
                        }
                    }

                    Items.Add(Item);
                }
            }

            return Items;
        }

        /// <summary>
        /// 
        /// </summary>
        private List<string> Navigation_Item_Content
        {
            get
            {
                List<string> Items = new();

                if (_navigationView.MenuItems.Count > 0)
                {
                    foreach (MUXC.NavigationViewItem Item in Navigation_Item(true))
                    {
                        if (Item.MenuItems.Count == 0)
                        {
                            Items.Add(Item.Content.ToString());
                        }
                    }
                }

                return Items;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

        /// <summary>
        /// 
        /// </summary>
        public ICommand ItemInvokedCommand => _itemInvokedCommand ?? (_itemInvokedCommand = new RelayCommand<MUXC.NavigationViewItemInvokedEventArgs>(OnItemInvoked));

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
        public void Initialize(WUXC.Frame frame, MUXC.NavigationView navigationView, WUXC.AutoSuggestBox suggestBox, IList<KeyboardAccelerator> keyboardAccelerators)
        {
            _navigationView = navigationView;
            _suggestBox = suggestBox;
            _keyboardAccelerators = keyboardAccelerators;
            NavigationService.Frame = frame;
            NavigationService.Navigation = _navigationView;
            NavigationService.NavigationFailed += Frame_NavigationFailed;
            NavigationService.Navigated += Frame_Navigated;
            _navigationView.BackRequested += OnBackRequested;
            _suggestBox.TextChanged += TextChanged;
            _suggestBox.SuggestionChosen += SuggestionChosen;
            NavigationService.Initialize();
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
        private void OnItemInvoked(MUXC.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavigationService.Navigate(typeof(SettingsPage), null); //args.RecommendedNavigationTransitionInfo
            }
            else
            {
                MUXC.NavigationViewItem selectedItem = args.InvokedItemContainer as MUXC.NavigationViewItem;
                Type pageType = selectedItem?.GetValue(NavHelper.NavigateToProperty) as Type;

                if (pageType != null)
                {
                    NavigationService.Navigate(pageType, null); //args.RecommendedNavigationTransitionInfo
                }
                else if (selectedItem.MenuItems.Count == 0)
                {
                    Selected = selectedItem;
                    NavigationService.Navigate(Default.NotFoundPage, null); //args.RecommendedNavigationTransitionInfo
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnBackRequested(MUXC.NavigationView sender, MUXC.NavigationViewBackRequestedEventArgs args)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// Handle text change and present suitable items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TextChanged(WUXC.AutoSuggestBox sender, WUXC.AutoSuggestBoxTextChangedEventArgs args)
        {
            // Since selecting an item will also change the text,
            // only listen to changes caused by user entering text.
            if (args.Reason == WUXC.AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suitableItems = new List<string>();

                if (string.IsNullOrEmpty(sender.Text))
                {
                    sender.ItemsSource = suitableItems;
                }
                else
                {
                    var splitText = TextLower(sender.Text).Split(" ");

                    foreach (string Item in Navigation_Item_Content)
                    {
                        bool found = splitText.All((Key) =>
                        {
                            return TextLower(Item).Contains(Key);
                        });

                        if (found)
                        {
                            suitableItems.Add(Item.ToString());
                        }
                    }

                    if (suitableItems.Count == 0)
                    {
                        suitableItems.Add(ResourceExtensions.GetLocalizedCustom($"Shell|{sender.Tag}"));
                    }

                    sender.ItemsSource = suitableItems;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        private string TextLower(string Text)
        {
            return Text.ToLower(new CultureInfo(LanguageSelectorService.Language.ToString().Replace("_", "-")));
        }

        /// <summary>
        /// Handle user selecting an item, in our case just output the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SuggestionChosen(WUXC.AutoSuggestBox sender, WUXC.AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            _suggestBox.Text = string.Empty;

            foreach (MUXC.NavigationViewItem Item in Navigation_Item(true))
            {
                if (Item.Content.ToString() == args.SelectedItem.ToString())
                {
                    Type Page = NavHelper.GetNavigateTo(Item);

                    if (Page == null)
                    {
                        Page = Default.NotFoundPage;

                        Selected = Item;
                        Item.IsSelected = true;
                        Item.IsChildSelected = true;
                        Item.SelectsOnInvoked = true;
                    }

                    NavigationService.Navigate(Page, null);

                    //NavHelper.SetNavigateTo(Item, Default.NotFoundPage);
                    //NavigationService.Navigate(Item.GetValue(NavHelper.NavigateToProperty) as Type, null);
                    
                    break;
                }
            }
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

            if (_navigationView.IsSettingsVisible && e.SourcePageType == typeof(SettingsPage))
            {
                Selected = _navigationView.SettingsItem as MUXC.NavigationViewItem;
                return;
            }

            MUXC.NavigationViewItem selectedItem = GetSelectedItem(_navigationView.MenuItems.Concat(_navigationView.FooterMenuItems), e.SourcePageType);
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
        private MUXC.NavigationViewItem GetSelectedItem(IEnumerable<object> menuItems, Type pageType)
        {
            foreach (MUXC.NavigationViewItem item in menuItems.OfType<MUXC.NavigationViewItem>())
            {
                if (IsMenuItemForPageType(item, pageType))
                {
                    return item;
                }

                MUXC.NavigationViewItem selectedChild = GetSelectedItem(item.MenuItems, pageType);
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
        private bool IsMenuItemForPageType(MUXC.NavigationViewItem menuItem, Type sourcePageType)
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