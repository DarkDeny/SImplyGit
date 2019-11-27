using Infrastructure;

namespace SimplyGit.ViewModels {
    internal class LocalBranchViewModel : ViewModelBase {
        public LocalBranchViewModel(string branchName) {
            DisplayName = branchName;
        }

        public string DisplayName { get; }
    }
}