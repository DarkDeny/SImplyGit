using System.Linq;
using LibGit2Sharp;

namespace SimplyGit.ViewModels {
    internal class DiffViewModel {
        private readonly PatchEntryChanges _difference;

        public DiffViewModel(PatchEntryChanges difference) {
            _difference = difference;

            var lines = _difference.Patch.Split('\n').ToList();
            lines = lines.Skip(5).ToList();
            Diff = string.Join("\n", lines);
        }

        public ChangeKind ChangeKind => _difference.Status;
        public string FileName => _difference.Path;
        public string Diff { get; }
    }
}