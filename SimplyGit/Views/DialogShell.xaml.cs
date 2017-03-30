using System.Windows;

namespace SimplyGit.Views {
    /// <summary>
    /// Interaction logic for DialogShell.xaml
    /// </summary>
    public partial class DialogShell {
        public DialogShell() {
            InitializeComponent();
        }

        private void OnOkClick(object sender, RoutedEventArgs e) {
            DialogResult = true;
            Close();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e) {
            DialogResult = false;
            Close();
        }
    }
}
