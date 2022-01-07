#region Imports

using System;
using Windows.UI.Xaml.Data;

#endregion

namespace Lunox.Helpers
{
    #region EnumToBooleanConverter

    /// <summary>
    /// 
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public Type EnumType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter is string enumString)
            {
                if (!Enum.IsDefined(EnumType, value))
                {
                    throw new ArgumentException("value must be an Enum!");
                }

                object enumValue = Enum.Parse(EnumType, enumString);

                return enumValue.Equals(value);
            }

            throw new ArgumentException("parameter must be an Enum name!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (parameter is string enumString)
            {
                return Enum.Parse(EnumType, enumString);
            }

            throw new ArgumentException("parameter must be an Enum name!");
        }

        #endregion
    }

    #endregion
}