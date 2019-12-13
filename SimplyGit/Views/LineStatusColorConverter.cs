using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SimplyGit.Views {
    public class LineStatusColorConverter : IValueConverter {
        static LineStatusColorConverter() {
            AddedBrush = new SolidColorBrush(Color.FromRgb(146, 236, 146));
            AddedBrush.Freeze();

            RemovedBrush = new SolidColorBrush(Color.FromRgb(238, 167, 167));
            RemovedBrush.Freeze();

            BlankBackground = new SolidColorBrush(Color.FromRgb(109, 109, 109));
        }

        public static SolidColorBrush AddedBrush { get; }
        public static SolidColorBrush RemovedBrush { get; }
        public static SolidColorBrush BlankBackground { get; }

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var typedValue = (DiffStatus)value;

            switch (typedValue) {
                case DiffStatus.Added:
                    return AddedBrush;
                case DiffStatus.Removed:
                    return RemovedBrush;
                default:
                    return BlankBackground;
            }
        }

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}