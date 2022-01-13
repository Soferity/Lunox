#region Imports

using System;
using System.Collections.Concurrent;

#endregion

namespace Lunox.Library.Helper
{
    #region SingletonHelper

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Singleton<T> where T : new()
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private static readonly ConcurrentDictionary<Type, T> _instances = new();

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public static T Instance => _instances.GetOrAdd(typeof(T), (t) => new T());

        #endregion
    }

    #endregion
}