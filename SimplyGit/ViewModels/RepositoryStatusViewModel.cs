using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace SimplyGit.ViewModels {
    internal class RepositoryStatusViewModel : RepositoryStatusViewModelBase {
        private readonly Repository _repository;
        private readonly List<string> _branches;
        private readonly List<Commit> _commits;

        public RepositoryStatusViewModel(Repository repository) {
            _repository = repository;
            ActiveBranch = _repository.Branches.FirstOrDefault(b => b.IsCurrentRepositoryHead);
            _branches = _repository.Branches.Select(b => b.FriendlyName).ToList();
            var filter = new CommitFilter();
            _commits = _repository.Commits.QueryBy(filter).ToList();
        }

        private Branch _activeBranch;
        public Branch ActiveBranch {
            get { return _activeBranch; }
            set {
                if (Equals(value, _activeBranch)) return;
                _activeBranch = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ActiveBranchName));
            }
        }

        public int CommitCount => _commits.Count;
        public int BranchesCount => _branches.Count;

        public int ChangesCount => _repository.Index.Count;

        public string ActiveBranchName => ActiveBranch?.FriendlyName;
        public override bool CanOpenRepositoryTab => true;
    }
}