using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using LibGit2Sharp;
using SimplyGit.Models;

namespace SimplyGit.ViewModels {
    internal class RepositoryViewModel : ViewModelBase {
        private readonly RepositoryModel _repositoryModel;
        private readonly Repository _repository;
        private List<string> _branches;
        private readonly List<Commit> _commits;
        public RepositoryState RepositoryState { get; }

        public RepositoryModel Model => _repositoryModel;

        public RepositoryViewModel(RepositoryModel repositoryModel) {
            _repositoryModel = repositoryModel;
            try {
                _repository = new Repository(_repositoryModel.WorkingFolder);
                _branches = _repository.Branches.Select(b => b.FriendlyName).ToList();
                ActiveBranch = _repository.Branches.FirstOrDefault(b => b.IsCurrentRepositoryHead)?.FriendlyName;
                var filter = new CommitFilter();
                _commits = _repository.Commits.QueryBy(filter).ToList();
                RepositoryState = RepositoryState.Ok;
            }
            catch {
                RepositoryState = RepositoryState.Broken;
            }
        }

        public string Name {
            get { return _repositoryModel.Name; }
            set {
                if (value != _repositoryModel.Name) {
                    _repositoryModel.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public int CommitCount => _commits.Count;
        public int BranchesCount => _branches.Count;

        public string WorkingFolder => _repositoryModel.WorkingFolder;

        private string _activeBranch;
        public string ActiveBranch {
            get { return _activeBranch; }
            set {
                if (value == _activeBranch) return;
                _activeBranch = value;
                OnPropertyChanged();
            }
        }
    }
}