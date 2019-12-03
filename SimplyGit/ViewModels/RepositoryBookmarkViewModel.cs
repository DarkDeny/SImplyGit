using System;
using System.Collections.ObjectModel;
using System.Linq;
using Infrastructure;
using LibGit2Sharp;
using SimplyGit.Models;

namespace SimplyGit.ViewModels {
    internal class RepositoryBookmarkViewModel : ViewModelBase {
        private readonly RepositoryModel _repositoryModel;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly Repository _repository;

        public RepositoryStatusViewModelBase RepositoryStatus { get; }

        public RepositoryModel Model => _repositoryModel;

        public RepositoryBookmarkViewModel(RepositoryModel repositoryModel) {
            _repositoryModel = repositoryModel;
            try {
                _repository = new Repository(_repositoryModel.WorkingFolder);
                RepositoryStatus = new RepositoryStatusViewModel(_repository);

                LocalBranches = new ObservableCollection<LocalBranchViewModel>();
                Remotes = new ObservableCollection<RemoteViewModel>();
                Stashes = new ObservableCollection<StashViewModel>();
                Submodules = new ObservableCollection<SubmoduleViewModel>();
                CommitHistoryCollection = new ObservableCollection<CommitViewModel>();

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

                foreach (var commit in _repository.Commits) {
                    var vm = new CommitViewModel(commit, _repository);
                    CommitHistoryCollection.Add(vm);
                    if (CommitHistoryCollection.Count > 15) {
                        break;
                    }
                }

                if (CommitHistoryCollection.Any()) {
                    SelectedCommit = CommitHistoryCollection.FirstOrDefault();
                }
            }
            catch (Exception ex) {
                RepositoryStatus = new BrokenRepositoryStatusViewModel(ex.Message);
            }
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

        public ObservableCollection<LocalBranchViewModel> LocalBranches { get; }

        public ObservableCollection<RemoteViewModel> Remotes { get; }

        public ObservableCollection<CommitViewModel> CommitHistoryCollection { get; }

        private CommitViewModel _selectedCommit;
        public CommitViewModel SelectedCommit {
            get => _selectedCommit;
            set {
                if (Equals(value, _selectedCommit))
                    return;

                _selectedCommit?.OnDeactivate();
                _selectedCommit = value;
                _selectedCommit?.OnActivate();
                OnPropertyChanged();
            }
        }

        public ObservableCollection<StashViewModel> Stashes { get; }

        public ObservableCollection<SubmoduleViewModel> Submodules { get; }
    }
}