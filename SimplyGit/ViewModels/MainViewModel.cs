using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Newtonsoft.Json;
using SimplyGit.Models;
using SimplyGit.Views;

namespace SimplyGit.ViewModels {
    internal class MainViewModel : ViewModelBase {
        private readonly App _app;
        private readonly ConfigurationModel _configuration;
        private readonly string _cfgFilePath;

        public MainViewModel(App app) {
            _app = app;
            _app.MainWindow.Closing += MainWindow_Closing;
            var appData = GetProductFolder();
            _cfgFilePath = Path.Combine(appData, ConfigurationFile);
            if (File.Exists(_cfgFilePath)) {
                try {
                    var text = File.ReadAllText(_cfgFilePath);
                    _configuration = JsonConvert.DeserializeObject<ConfigurationModel>(text);
                }
                catch (Exception ) {
                    // TODO: log?
                }
            }

            if (null == _configuration) {
                _configuration = new ConfigurationModel();
            }

            BookmarkedRepositories = new ObservableCollection<RepositoryViewModel>();
            if (_configuration.Repositories?.Count > 0) {
                foreach (var repository in _configuration.Repositories) {
                    var vm = new RepositoryViewModel(repository);
                    BookmarkedRepositories.Add(vm);
                }
            }

            AddRepository = new DelegateCommand(DoAddRepo);
            RemoveRepository = new DelegateCommand(DoRemoveRepository, CanRemoveRepository);
            CloseApplication = new DelegateCommand(DoCloseApplication);
        }

        private bool CanRemoveRepository() {
            return null != _selectedRepository;
        }

        private void DoRemoveRepository() {
            var result = MessageBox.Show("Are you sure you want to remove repository?", "Confirm",
                MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK) {
                BookmarkedRepositories.Remove(_selectedRepository);
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            SaveConfiguration();
        }

        private void DoCloseApplication() {
            SaveConfiguration();
            _app.Shutdown();
        }

        private void SaveConfiguration() {
            try {
                _configuration.Repositories = BookmarkedRepositories.Select(rvm => rvm.Model).ToList();
                var cfgString = JsonConvert.SerializeObject(_configuration);
                File.WriteAllText(_cfgFilePath, cfgString);
            } catch (Exception) {
                // TODO: log?
            }
        }

        private void DoAddRepo() {
            var arvm = new AddRepositoryViewModel();
            var dialog= new DialogShell();
            dialog.Owner = _app.MainWindow;
            dialog.DataContext = arvm;
            var result = dialog.ShowDialog();
            if (result == true) {
                var newRepo = new RepositoryModel(arvm.RepositoryName, arvm.WorkingFolder);
                var vm = new RepositoryViewModel(newRepo);
                BookmarkedRepositories.Add(vm);
            }
        }

        public DelegateCommand AddRepository { get; }
        public DelegateCommand RemoveRepository { get; }
        public DelegateCommand CloseApplication { get; }

        public ObservableCollection<RepositoryViewModel> BookmarkedRepositories { get; }

        private RepositoryViewModel _selectedRepository;
        public RepositoryViewModel SelectedRepository {
            get { return _selectedRepository; }
            set {
                if (Equals(value, _selectedRepository)) return;
                _selectedRepository = value;
                OnPropertyChanged();
            }
        }

        internal static string GetProductFolder() {
            var companyFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            var productFolder = Path.Combine(companyFolder, ProductName);
            if (!Directory.Exists(productFolder)) {
                Directory.CreateDirectory(productFolder);
            }
            return productFolder;
        }

        public static string ProductName => "SimplyGit";
        public static string ConfigurationFile => "simplygit.cfg";
    }
}
