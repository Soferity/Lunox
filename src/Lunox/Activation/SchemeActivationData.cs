using System;
using System.Collections.Generic;
using System.Web;

namespace Lunox.Activation
{
    public class SchemeActivationData
    {
        // TODO WTS: Open package.appxmanifest and change the declaration for the scheme (from the default of 'wtsapp') to what you want for your app.
        // More details about this functionality can be found at https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/features/deep-linking.md
        // TODO WTS: Change the image in Assets/Logo.png to one for display if the OS asks the user which app to launch.
        // Also update this protocol name with the same value as package.appxmanifest.
        private const string ProtocolName = "wtsapp";

        public Type PageType { get; private set; }

        public Uri Uri { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; } = new Dictionary<string, string>();

        public bool IsValid => PageType != null;

        public SchemeActivationData(Uri activationUri)
        {
            PageType = SchemeActivationConfig.GetPage(activationUri.AbsolutePath);

            if (!IsValid || string.IsNullOrEmpty(activationUri.Query))
            {
                return;
            }

            System.Collections.Specialized.NameValueCollection uriQuery = HttpUtility.ParseQueryString(activationUri.Query);
            foreach (string paramKey in uriQuery.AllKeys)
            {
                Parameters.Add(paramKey, uriQuery.Get(paramKey));
            }
        }

        public SchemeActivationData(Type pageType, Dictionary<string, string> parameters = null)
        {
            PageType = pageType;
            Parameters = parameters;
            Uri = BuildUri();
        }

        private Uri BuildUri()
        {
            string pageKey = SchemeActivationConfig.GetPageKey(PageType);
            UriBuilder uriBuilder = new UriBuilder($"{ProtocolName}:{pageKey}");
            System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);

            foreach (KeyValuePair<string, string> parameter in Parameters)
            {
                query.Set(parameter.Key, parameter.Value);
            }

            uriBuilder.Query = query.ToString();
            return new Uri(uriBuilder.ToString());
        }
    }
}
