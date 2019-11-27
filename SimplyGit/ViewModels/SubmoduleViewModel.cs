using LibGit2Sharp;

namespace SimplyGit.ViewModels {
    internal class SubmoduleViewModel {
        public SubmoduleViewModel(Submodule submodule) {
            _submodule = submodule;
            DisplayName = submodule.Name;
        }

        private readonly Submodule _submodule;
        public string DisplayName { get; }
    }
}