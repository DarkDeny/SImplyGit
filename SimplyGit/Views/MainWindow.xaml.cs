using System;
using System.Windows.Input;

namespace SimplyGit.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            Console.WriteLine("gotcha!");
        }
    }
}
