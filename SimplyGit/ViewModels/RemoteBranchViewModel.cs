using Infrastructure;
using LibGit2Sharp;

namespace SimplyGit.ViewModels {
    internal class RemoteBranchViewModel : ViewModelBase {
        public RemoteBranchViewModel(Branch branch) {
            _branch = branch;
            DisplayName = branch.CanonicalName;
        }

        private readonly Branch _branch;
        public string DisplayName { get; }
    }
}