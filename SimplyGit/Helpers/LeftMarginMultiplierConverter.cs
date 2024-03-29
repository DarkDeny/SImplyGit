using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SimplyGit.Helpers {
    public class LeftMarginMultiplierConverter : IValueConverter {
        public double Length { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            TreeViewItem item = value as TreeViewItem;
            return item == null ? new Thickness(0) : new Thickness(Length * item.GetDepth(), 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}