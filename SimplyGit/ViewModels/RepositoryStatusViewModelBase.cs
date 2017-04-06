using Infrastructure;

namespace SimplyGit.ViewModels {
    internal abstract class RepositoryStatusViewModelBase : ViewModelBase {
        public abstract bool CanOpenRepositoryTab { get; }
    }
}