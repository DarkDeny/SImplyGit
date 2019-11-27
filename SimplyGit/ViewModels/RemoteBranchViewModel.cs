using Infrastructure;

namespace SimplyGit.ViewModels {
    internal class RemoteBranchViewModel : ViewModelBase {
        public RemoteBranchViewModel(string displayName) {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }
}