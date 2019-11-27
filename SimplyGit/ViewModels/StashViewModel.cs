using LibGit2Sharp;

namespace SimplyGit.ViewModels {
    internal class StashViewModel {
        public StashViewModel(Stash stash) {
            _stash = stash;
            DisplayName = stash.Message;
        }

        private Stash _stash;
        public string DisplayName { get; }
    }
}