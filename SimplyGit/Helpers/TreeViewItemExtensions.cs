using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimplyGit.Helpers {
    public static class TreeViewItemExtensions {
        public static int GetDepth(this TreeViewItem item) {
            TreeViewItem parent;
            while ((parent = GetParent(item)) != null) return GetDepth(parent) + 1;
            return 0;
        }

        private static TreeViewItem GetParent(DependencyObject item) {
            var parent = VisualTreeHelper.GetParent(item);
            // ReSharper disable once AssignNullToNotNullAttribute
            while (!(parent is TreeViewItem || parent is TreeView)) parent = VisualTreeHelper.GetParent(parent);
            return parent as TreeViewItem;
        }
    }
}