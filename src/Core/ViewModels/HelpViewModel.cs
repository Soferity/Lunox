#region Imports

using Lunox.Library.Value;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

#endregion

namespace Lunox.Core.ViewModels
{
    #region HelpViewModel

    /// <summary>
    /// 
    /// </summary>
    public class HelpViewModel : ObservableObject
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public Uri Bug => Default.Bug;

        /// <summary>
        /// 
        /// </summary>
        public Uri Feature => Default.Feature;

        /// <summary>
        /// 
        /// </summary>
        public Uri Discussions => Default.Discussions;

        /// <summary>
        /// 
        /// </summary>
        public Uri Contribute => Default.Contribute;

        /// <summary>
        /// 
        /// </summary>
        public HelpViewModel()
        {
        }

        #endregion
    }

    #endregion
}