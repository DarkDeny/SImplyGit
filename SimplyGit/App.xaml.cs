using SimplyGit.ViewModels;
using SimplyGit.Views;

namespace SimplyGit {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {
        public MainWindow MainWindow { get; }

        public App() {
            MainWindow = new MainWindow();
            var vm = new MainViewModel(this);
            MainWindow.DataContext = vm;
            MainWindow.Show();
        }
    }
}
