using System;
using System.Globalization;
using System.Windows.Data;
using LibGit2Sharp;

namespace SimplyGit.Helpers {
    public class EnumFileStatusToIconConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var partialPath = "/Icons/FileStatus/";

            switch ((ChangeKind) value) {
                case ChangeKind.Added:
                    return $"{partialPath}added.png";
                case ChangeKind.Deleted:
                    return $"{partialPath}deleted.png";
                case ChangeKind.Modified:
                    return $"{partialPath}modified.png";
                case ChangeKind.Copied:
                    return $"{partialPath}copied.png";
                case ChangeKind.Conflicted:
                    return $"{partialPath}conflicted.png";
                case ChangeKind.Renamed:
                    return $"{partialPath}renamed.png";
                case ChangeKind.Unreadable:
                    return $"{partialPath}unreadable.png";
                case ChangeKind.Untracked:
                    return $"{partialPath}untracked.png";
                case ChangeKind.Ignored:
                    return $"{partialPath}ignored.png";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}