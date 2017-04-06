using SimplyGit.ViewModels;

namespace SimplyGit.Interfaces {
    internal interface IRepositoryActivator {
        void Activate(RepositoryBookmarkViewModel repo);
    }
}
