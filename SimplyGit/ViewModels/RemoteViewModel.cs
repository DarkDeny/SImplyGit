using System.Collections.ObjectModel;
using Infrastructure;
using LibGit2Sharp;

namespace SimplyGit.ViewModels {
    internal class RemoteViewModel : ViewModelBase {
        public RemoteViewModel(string remoteName) {
            DisplayName = remoteName;
            RemoteBranches = new ObservableCollection<RemoteBranchViewModel>();
        }

        public void AddBranch(Branch branch) {
            var vm = new RemoteBranchViewModel(branch);
            RemoteBranches.Add(vm);
        }

        public string DisplayName { get; }

        public ObservableCollection<RemoteBranchViewModel> RemoteBranches { get; }
    }
}