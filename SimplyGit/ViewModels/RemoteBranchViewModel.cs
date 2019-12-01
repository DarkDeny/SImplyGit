using Infrastructure;
using LibGit2Sharp;

namespace SimplyGit.ViewModels {
    internal class RemoteBranchViewModel : ViewModelBase {
        public RemoteBranchViewModel(Branch branch) {
            _branch = branch;
            var allWords = branch.CanonicalName.Split('/');
            DisplayName = allWords[allWords.Length - 1];
        }

        private readonly Branch _branch;
        public string DisplayName { get; }
    }
}