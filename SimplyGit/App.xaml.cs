using System.Windows;
using SimplyGit.ViewModels;
using SimplyGit.Views;

namespace SimplyGit {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            MainWindow = new MainWindow();
            var vm = new MainViewModel(this);
            MainWindow.DataContext = vm;
            MainWindow.Show();
        }
    }
}
