namespace SimplyGit.ViewModels {
    internal class StashViewModel {
        public string DisplayName { get; }

        public StashViewModel(string displayName) {
            DisplayName = displayName;
        }
    }
}