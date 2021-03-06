namespace ChatworkApi.Tester.Presentation.Converters
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// <see cref="DateTime"/> を書式化した文字列へのコンバータークラスです。
    /// </summary>
    public sealed class DateTimeFormatConverter : IValueConverter
    {
        private static readonly bool Japanese;

        static DateTimeFormatConverter()
        {
            Japanese =   Thread.CurrentThread.CurrentCulture.LCID == 1041;
        }

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
            if (!(value is DateTime actualValue)) return DependencyProperty.UnsetValue;

            return actualValue.ToString(Japanese ? "yyyy年MM月dd日(ddd) HH:mm" : "g");
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