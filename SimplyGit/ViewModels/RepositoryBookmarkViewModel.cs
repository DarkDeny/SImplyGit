using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Infrastructure;
using LibGit2Sharp;
using Microsoft.Practices.Prism.Commands;
using SimplyGit.Interfaces;
using SimplyGit.Models;

namespace SimplyGit.ViewModels {
    internal class RepositoryBookmarkViewModel : ViewModelBase {
        private readonly RepositoryModel _repositoryModel;
        private readonly IRepositoryActivator _activator;
        private Repository _repository;

        public RepositoryStatusViewModelBase RepositoryStatus { get; }

        public RepositoryModel Model => _repositoryModel;

        public RepositoryBookmarkViewModel(RepositoryModel repositoryModel, IRepositoryActivator activator) {
            _repositoryModel = repositoryModel;
            _activator = activator;
            try {
                _repository = new Repository(_repositoryModel.WorkingFolder);
                RepositoryStatus = new RepositoryStatusViewModel(_repository);

                LocalBranches = new ObservableCollection<LocalBranchViewModel>();
                Remotes = new ObservableCollection<RemoteViewModel>();
                Stashes = new ObservableCollection<StashViewModel>();
                Submodules = new ObservableCollection<SubmoduleViewModel>();

                foreach (var branch in _repository.Branches) {
                    if (branch.IsRemote) {
                        var existing = Remotes.FirstOrDefault(r => r.DisplayName == branch.RemoteName);
                        if (null == existing) {
                            existing = new RemoteViewModel(branch.RemoteName);
                            Remotes.Add(existing);
                        }

                        existing.AddBranch(branch);
                    }
                    else {
                        var vm = new LocalBranchViewModel(branch);
                        LocalBranches.Add(vm);
                    }
                }

                foreach (var stash in _repository.Stashes) {
                    var vm = new StashViewModel(stash);
                    Stashes.Add(vm);
                }

                foreach (var submodule in _repository.Submodules) {
                    var vm = new SubmoduleViewModel(submodule);
                    Submodules.Add(vm);
                }
            }
            catch (Exception ex) {
                RepositoryStatus = new BrokenRepositoryStatusViewModel(ex.Message);
            }

            OpenRepositoryTab = new DelegateCommand(DoOpenRepositoryTab, CanOpenRepositoryTab);

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
}