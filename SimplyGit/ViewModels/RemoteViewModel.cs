using System.Collections.ObjectModel;
using Infrastructure;

namespace SimplyGit.ViewModels {
    internal class RemoteViewModel : ViewModelBase {
        public RemoteViewModel(string displayName) {
            DisplayName = displayName;
            RemoteBranches = new ObservableCollection<RemoteBranchViewModel> {
                new RemoteBranchViewModel("STATIC_DATA_feature"),
                new RemoteBranchViewModel("STATIC_DATA_feature2"),
                new RemoteBranchViewModel("STATIC_DATA_bugfix3"),
            };
        }

        public string DisplayName { get; }

        public ObservableCollection<RemoteBranchViewModel> RemoteBranches { get; }
    }
}