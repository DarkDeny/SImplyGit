using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace SimplyGit.Controls {
    internal static class LocalExtensions {
        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action) {
            Window window = ((FrameworkElement) templateFrameworkElement).TemplatedParent as Window;
            if (window != null) action(window);
        }

        public static IntPtr GetWindowHandle(this Window window) {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            return helper.Handle;
        }
    }

    public partial class WindowStyle {
        private void IconMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount > 1)
                sender.ForWindowFromTemplate(SystemCommands.CloseWindow);
        }

        private void IconMouseUp(object sender, MouseButtonEventArgs e) {
            var element = sender as FrameworkElement;
            var point = element.PointToScreen(new Point(element.ActualWidth / 2, element.ActualHeight));
            sender.ForWindowFromTemplate(w => SystemCommands.ShowSystemMenu(w, point));
        }

        private void WindowLoaded(object sender, RoutedEventArgs e) {
            ((Window) sender).StateChanged += WindowStateChanged;
        }

        private static void WindowStateChanged(object sender, EventArgs e) {
            var w = ((Window) sender);
            var handle = w.GetWindowHandle();
            var containerBorder = (Border) w.Template.FindName("PART_Container", w);

            if (w.WindowState == WindowState.Maximized) {
                // Make sure window doesn't overlap with the taskbar.
                var screen = System.Windows.Forms.Screen.FromHandle(handle);
                //magic numbers are the calculated numbers not to cut window in maximized mode
                if (screen.Primary) {
                    containerBorder.Padding = new Thickness(
                        SystemParameters.WorkArea.Left + 6,
                        SystemParameters.WorkArea.Top + 6,
                        (SystemParameters.PrimaryScreenWidth - SystemParameters.WorkArea.Right) + 6,
                        (SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Bottom) + 4);
                }
            }
            else {
                containerBorder.Padding = new Thickness(6, 6, 6, 4);
            }
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e) {
            sender.ForWindowFromTemplate(SystemCommands.CloseWindow);
        }

        private void MinButtonClick(object sender, RoutedEventArgs e) {
            sender.ForWindowFromTemplate(SystemCommands.MinimizeWindow);
        }

        private void MaxButtonClick(object sender, RoutedEventArgs e) {
            sender.ForWindowFromTemplate(w => {
                if (w.WindowState == WindowState.Maximized) {
                    SystemCommands.RestoreWindow(w);
                }
                else {
                    SystemCommands.MaximizeWindow(w);
                }
            });
        }
    }
}