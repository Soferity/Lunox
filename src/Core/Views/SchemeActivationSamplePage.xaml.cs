#region Imports

using Lunox.Core.ViewModels;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

#endregion

namespace Lunox.Core.Views
{
    #region SchemeActivationSamplePage

    /// <summary>
    /// TODO WTS: Remove this sample page when/if it's not needed.
    /// This page is an sample of how to launch a specific page in response to a protocol launch and pass it a value.
    /// It is expected that you will delete this page once you have changed the handling of a protocol launch to meet
    /// your needs and redirected to another of your pages.
    /// </summary>
    public sealed partial class SchemeActivationSamplePage : Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public SchemeActivationSampleViewModel ViewModel { get; } = new SchemeActivationSampleViewModel();

        /// <summary>
        /// 
        /// </summary>
        public SchemeActivationSamplePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Dictionary<string, string> parameters = e.Parameter as Dictionary<string, string>;
            if (parameters != null)
            {
                ViewModel.Initialize(parameters);
            }
        }

        #endregion
    }

    #endregion
}