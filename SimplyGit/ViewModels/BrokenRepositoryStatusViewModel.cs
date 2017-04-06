namespace SimplyGit.ViewModels {
    internal class BrokenRepositoryStatusViewModel : RepositoryStatusViewModelBase {
        public BrokenRepositoryStatusViewModel(string errorMessage) {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
        public override bool CanOpenRepositoryTab => false;
    }
}