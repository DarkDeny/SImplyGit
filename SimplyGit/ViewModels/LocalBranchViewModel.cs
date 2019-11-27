using Infrastructure;
using LibGit2Sharp;

namespace SimplyGit.ViewModels {
    internal class LocalBranchViewModel : ViewModelBase {
        public LocalBranchViewModel(Branch branch) {
            _branch = branch;
            DisplayName = branch.FriendlyName;
        }

        private Branch _branch;
        public string DisplayName { get; }
    }
}