namespace ChatworkApi.Tester.Presentation.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using Domain;
    using Domain.Models;

    /// <summary>
    /// <see cref="WorkTask"/> から期限となる日時文字列へ変換するクラスです。
    /// </summary>
    public sealed class WorkTaskToLimitConverter : IValueConverter
    {
        /// <summary>Converts a value.</summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        public object Convert(object      value
                            , Type        targetType
                            , object      parameter
                            , CultureInfo culture)
        {
            if (!(value is WorkTask workTask)) return DependencyProperty.UnsetValue;

            switch (workTask.LimitType)
            {
                case TaskLimitType.Date:     return $"{workTask.Limit:yyyy/MM/dd}";
                case TaskLimitType.DateTime: return $"{workTask.Limit:yyyy/MM/dd HH/mm}";
                case TaskLimitType.None:     return $"なし";
                default:                     throw new ArgumentOutOfRangeException($"workTask.LimitType");
            }
        }

        /// <summary>Converts a value.</summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        public object ConvertBack(object      value
                                , Type        targetType
                                , object      parameter
                                , CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}