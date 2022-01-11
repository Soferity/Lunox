#region Imports

using Lunox.Core.Activation;
using Lunox.Core.Helpers;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Core.Services
{
    #region SuspendAndResumeService

    /// <summary>
    /// The SuspendAndResumeService allows you to save the App data before the App is being suspended (or enters in background state).
    /// In case the App is terminated during suspension, the data is restored during App launch by this ActivationHandler.
    /// In case the App is resumed without being terminated no data should be lost, a resume event is fired that allows you to refresh App data that might
    /// be outdated (e.g data from online feeds)
    /// Documentation:
    ///     * How to implement and test: https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/features/suspend-and-resume.md
    ///     * Application Lifecycle: https://docs.microsoft.com/windows/uwp/launch-resume/app-lifecycle
    /// </summary>
    internal class SuspendAndResumeService : ActivationHandler<LaunchActivatedEventArgs>
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private const string StateFilename = "SuspendAndResumeState";

        #endregion

        #region Events

        /// <summary>
        /// TODO WTS: Subscribe to the OnBackgroundEntering and OnDataRestored events from your current Page to save and restore the current App data.
        /// Only one Page should subscribe to OnBackgroundEntering and OnDataRestored at a time, as the App will navigate to that Page on resume.
        /// </summary>
        public event EventHandler<SuspendAndResumeArgs> OnBackgroundEntering;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<SuspendAndResumeArgs> OnDataRestored;

        /// <summary>
        /// TODO WTS: Subscribe to the OnResuming event from the current Page
        /// if you need to refresh online data when the App resumes without being terminated.
        /// </summary>
        public event EventHandler OnResuming;

        #endregion

        #region Functions

        /// <summary>
        /// This method saves the application state before entering background state. It fires the event OnBackgroundEntering to collect
        /// state data from the current subscriber and saves it to the local storage.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveStateAsync()
        {
            if (OnBackgroundEntering == null)
            {
                return false;
            }

            try
            {
                SuspensionState suspensionState = new SuspensionState()
                {
                    SuspensionDate = DateTime.Now
                };

                Type target = OnBackgroundEntering?.Target.GetType();
                SuspendAndResumeArgs onBackgroundEnteringArgs = new SuspendAndResumeArgs(suspensionState, target);

                OnBackgroundEntering?.Invoke(this, onBackgroundEnteringArgs);

                await ApplicationData.Current.LocalFolder.SaveAsync(StateFilename, onBackgroundEnteringArgs);
                return true;
            }
            catch (Exception)
            {
                // TODO WTS: Save state can fail in rare conditions, please handle exceptions as appropriate to your scenario.
                return false;
            }
        }

        /// <summary>
        /// This method allows subscribers to refresh data that might be outdated when the App is resuming from suspension.
        /// If the App was terminated during suspension this event will not fire, data restore is handled by the method HandleInternalAsync.
        /// </summary>
        public void ResumeApp()
        {
            OnResuming?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task RestoreSuspendAndResumeData()
        {
            SuspendAndResumeArgs saveState = await GetSuspendAndResumeData();
            if (saveState != null)
            {
                OnDataRestored?.Invoke(this, saveState);
            }
        }

        /// <summary>
        /// This method restores application state when the App is launched after termination, it navigates to the stored Page passing the recovered state data.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override async Task HandleInternalAsync(LaunchActivatedEventArgs args)
        {
            SuspendAndResumeArgs saveState = await GetSuspendAndResumeData();
            if (saveState?.Target != null && typeof(Page).IsAssignableFrom(saveState.Target))
            {
                NavigationService.Navigate(saveState.Target, saveState.SuspensionState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
        {
            // Application State must only be restored if the App was terminated during suspension.
            return args.PreviousExecutionState == ApplicationExecutionState.Terminated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<SuspendAndResumeArgs> GetSuspendAndResumeData()
        {
            SuspendAndResumeArgs saveState = await ApplicationData.Current.LocalFolder.ReadAsync<SuspendAndResumeArgs>(StateFilename);
            if (saveState?.Target != null && typeof(Page).IsAssignableFrom(saveState.Target))
            {
                return saveState;
            }

            return null;
        }

        #endregion
    }

    #endregion
}