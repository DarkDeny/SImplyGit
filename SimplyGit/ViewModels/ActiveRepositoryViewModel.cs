using Infrastructure;

namespace SimplyGit.ViewModels {
    internal class ActiveRepositoryViewModel : ViewModelBase {
        private readonly RepositoryBookmarkViewModel _sourceBookmark;

        public ActiveRepositoryViewModel(RepositoryBookmarkViewModel sourceBookmark) {
            _sourceBookmark = sourceBookmark;
        }
    }
}
