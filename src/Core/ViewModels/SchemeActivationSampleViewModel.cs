#region Imports

using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace Lunox.Core.ViewModels
{
    #region SchemeActivationSampleViewModel

    /// <summary>
    /// TODO WTS: Remove this sample page when/if it's not needed.
    /// This page is an sample of how to launch a specific page in response to a protocol launch and pass it a value.
    /// It is expected that you will delete this page once you have changed the handling of a protocol launch to meet
    /// your needs and redirected to another of your pages.
    /// </summary>
    public class SchemeActivationSampleViewModel : ObservableObject
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<string> Parameters { get; } = new ObservableCollection<string>();

        /// <summary>
        /// 
        /// </summary>
        public SchemeActivationSampleViewModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        public void Initialize(Dictionary<string, string> parameters)
        {
            Parameters.Clear();
            foreach (KeyValuePair<string, string> param in parameters)
            {
                if (param.Key == "ticks" && long.TryParse(param.Value, out long ticks))
                {
                    DateTime dateTime = new(ticks);
                    Parameters.Add($"{param.Key}: {dateTime}");
                }
                else
                {
                    Parameters.Add($"{param.Key}: {param.Value}");
                }
            }
        }

        #endregion
    }

    #endregion
}