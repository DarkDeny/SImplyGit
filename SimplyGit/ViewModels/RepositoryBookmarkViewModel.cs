using System;
using System.Collections.ObjectModel;
using Infrastructure;
using LibGit2Sharp;
using Microsoft.Practices.Prism.Commands;
using SimplyGit.Interfaces;
using SimplyGit.Models;

namespace SimplyGit.ViewModels {
    internal class RepositoryBookmarkViewModel : ViewModelBase {
        private readonly RepositoryModel _repositoryModel;
        private readonly IRepositoryActivator _activator;

        public RepositoryStatusViewModelBase RepositoryStatus { get; }

        public RepositoryModel Model => _repositoryModel;

        public RepositoryBookmarkViewModel(RepositoryModel repositoryModel, IRepositoryActivator activator) {
            _repositoryModel = repositoryModel;
            _activator = activator;
            try {
                var repository = new Repository(_repositoryModel.WorkingFolder);
                RepositoryStatus = new RepositoryStatusViewModel(repository);
            }
            catch (Exception ex) {
                RepositoryStatus = new BrokenRepositoryStatusViewModel(ex.Message);
            }

            OpenRepositoryTab = new DelegateCommand(DoOpenRepositoryTab, CanOpenRepositoryTab);
            LocalBranches = new ObservableCollection<LocalBranchViewModel> {
                new LocalBranchViewModel("STATIC_DATA_1"),
                new LocalBranchViewModel("STATIC_DATA_2"),
                new LocalBranchViewModel("STATIC_DATA_3"),
            };

            Remotes = new ObservableCollection<RemoteViewModel> {
                new RemoteViewModel("STATIC_origin"),
            };

            Stashes = new ObservableCollection<StashViewModel> {
                new StashViewModel("STATIC_test1"),
                new StashViewModel("STATIC_cleanup"),
                new StashViewModel("STATIC_performance"),
            };

            Submodules = new ObservableCollection<SubmoduleViewModel> {
                new SubmoduleViewModel("STATIC_core"),
                new SubmoduleViewModel("STATIC_extern1"),
            };
        }

        private void DoOpenRepositoryTab() {
            _activator.Activate(this);
        }

        private bool CanOpenRepositoryTab() {
            return RepositoryStatus.CanOpenRepositoryTab;
        }

        public string Name {
            get => _repositoryModel.Name;
            set {
                if (value != _repositoryModel.Name) {
                    _repositoryModel.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WorkingFolder => _repositoryModel.WorkingFolder;

        public DelegateCommand OpenRepositoryTab { get; }

        public ObservableCollection<LocalBranchViewModel> LocalBranches { get; }

        public ObservableCollection<RemoteViewModel> Remotes { get; }

        public object CommitHistoryCollection { get; set; }

        public object SelectedCommit { get; set; }

        public ObservableCollection<StashViewModel> Stashes { get; }

        public ObservableCollection<SubmoduleViewModel> Submodules { get; }
    }

    internal class SubmoduleViewModel {
        public string DisplayName { get; }

        public SubmoduleViewModel(string displayName) {
            DisplayName = displayName;
        }
    }
}