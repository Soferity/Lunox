#region Imports

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Lunox.Core.Activation
{
    #region SchemeActivationConfig

    /// <summary>
    /// 
    /// </summary>
    public static class SchemeActivationConfig
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        private static readonly Dictionary<string, Type> _activationPages = new()
        {
            // TODO WTS: Add the pages that can be opened from scheme activation in your app here.
            { "sample", typeof(Views.SchemeActivationSamplePage) }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageKey"></param>
        /// <returns></returns>
        public static Type GetPage(string pageKey)
        {
            return _activationPages
                    .FirstOrDefault(p => p.Key == pageKey)
                    .Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public static string GetPageKey(Type pageType)
        {
            return _activationPages
                    .FirstOrDefault(v => v.Value == pageType)
                    .Key;
        }

        #endregion
    }

    #endregion
}