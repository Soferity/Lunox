#region Imports

using Lunox.Services;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

#endregion

namespace Lunox.Activation
{
    #region SchemeActivationHandler

    /// <summary>
    /// 
    /// </summary>
    internal class SchemeActivationHandler : ActivationHandler<ProtocolActivatedEventArgs>
    {
        #region Functions

        /// <summary>
        /// By default, this handler expects URIs of the format 'lnxapp:sample?paramName1=paramValue1&paramName2=paramValue2'
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override async Task HandleInternalAsync(ProtocolActivatedEventArgs args)
        {
            // Create data from activation Uri in ProtocolActivatedEventArgs
            SchemeActivationData data = new SchemeActivationData(args.Uri);
            if (data.IsValid)
            {
                NavigationService.Navigate(data.PageType, data.Parameters);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override bool CanHandleInternal(ProtocolActivatedEventArgs args)
        {
            // If your app has multiple handlers of ProtocolActivationEventArgs
            // use this method to determine which to use. (possibly checking args.Uri.Scheme)
            return true;
        }

        #endregion
    }

    #endregion
}