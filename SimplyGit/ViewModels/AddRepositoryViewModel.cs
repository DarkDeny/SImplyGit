using System;
using System.Windows.Forms;
using Infrastructure;
using LibGit2Sharp;
using Microsoft.Practices.Prism.Commands;

namespace SimplyGit.ViewModels {
    internal class AddRepositoryViewModel :ViewModelBase {
        public AddRepositoryViewModel() {
            SelectWorkingFolder = new DelegateCommand(DoSelectWorkingFolder);
        }

        private void DoSelectWorkingFolder() {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();
            if (result == DialogResult.OK) {
                WorkingFolder = fbd.SelectedPath;
                try {
                    _repo = new Repository(WorkingFolder);
                    var activeBranchName = string.Empty;
                    foreach (var branch in _repo.Branches) {
                        if (branch.IsCurrentRepositoryHead) {
                            activeBranchName = branch.FriendlyName;
                            break;
                        }
                    }
                    WorkingFolderStatus = $"Active branch: {activeBranchName}";
                    CheckCanComplete();
                }
                catch (RepositoryNotFoundException) {
                    WorkingFolderStatus = "Selected path doesn't point at a valid Git repository or workdir.";
                    CanComplete = false;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Unwind());
                    CanComplete = false;
                }
            }
        }

        private void CheckCanComplete() {
            var hasName = !string.IsNullOrEmpty(RepositoryName);
            var folderSelected = !string.IsNullOrEmpty(WorkingFolder);
            var repoIsOk = null != _repo;

            CanComplete = hasName && folderSelected && repoIsOk;
        }

        private bool _canComplete;
        public bool CanComplete {
            get { return _canComplete; }
            set {
                if (value == _canComplete) return;
                _canComplete = value;
                OnPropertyChanged();
            }
        }

        private string _repositoryName;
        public string RepositoryName {
            get { return _repositoryName; }
            set {
                if (value == _repositoryName) return;
                _repositoryName = value;
                OnPropertyChanged();
                CheckCanComplete();
            }
        }

        private string _workingFolder;
        public string WorkingFolder {
            get { return _workingFolder; }
            set {
                if (value == _workingFolder) return;
                _workingFolder = value;
                OnPropertyChanged();
            }
        }

        private string _workingFolderStatus;
        private Repository _repo;

        public string WorkingFolderStatus {
            get { return _workingFolderStatus; }
            set {
                if (value == _workingFolderStatus) return;
                _workingFolderStatus = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand SelectWorkingFolder { get; }
    }
}
