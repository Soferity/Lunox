#region Imports

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

#endregion

namespace Lunox.Core.Activation
{
    #region SchemeActivationData

    /// <summary>
    /// 
    /// </summary>
    public class SchemeActivationData
    {
        #region Variables

        /// <summary>
        /// TODO WTS: Open package.appxmanifest and change the declaration for the scheme (from the default of 'wtsapp') to what you want for your app.
        /// More details about this functionality can be found at https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/features/deep-linking.md
        /// TODO WTS: Change the image in Assets/Logo.png to one for display if the OS asks the user which app to launch.
        /// Also update this protocol name with the same value as package.appxmanifest.
        /// </summary>
        private const string ProtocolName = "lnxapp";

        /// <summary>
        /// 
        /// </summary>
        public Type PageType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Uri Uri { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Parameters { get; private set; } = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid => PageType != null;

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activationUri"></param>
        public SchemeActivationData(Uri activationUri)
        {
            PageType = SchemeActivationConfig.GetPage(activationUri.AbsolutePath);

            if (!IsValid || string.IsNullOrEmpty(activationUri.Query))
            {
                return;
            }

            try
            {
                NameValueCollection uriQuery = HttpUtility.ParseQueryString(activationUri.Query);
                foreach (string paramKey in uriQuery.AllKeys)
                {
                    Parameters.Add(paramKey, uriQuery.Get(paramKey));
                }
            }
            catch
            {
                //
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="parameters"></param>
        public SchemeActivationData(Type pageType, Dictionary<string, string> parameters = null)
        {
            PageType = pageType;
            Parameters = parameters;
            Uri = BuildUri();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Uri BuildUri()
        {
            string pageKey = SchemeActivationConfig.GetPageKey(PageType);
            UriBuilder uriBuilder = new($"{ProtocolName}:{pageKey}");
            NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);

            foreach (KeyValuePair<string, string> parameter in Parameters)
            {
                query.Set(parameter.Key, parameter.Value);
            }

            uriBuilder.Query = query.ToString();
            return new Uri(uriBuilder.ToString());
        }

        #endregion
    }

    #endregion
}