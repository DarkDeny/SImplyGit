using System.Collections.Generic;
using System.Linq;
using ICSharpCode.AvalonEdit.Document;
using LibGit2Sharp;
using SimplyGit.Views;

namespace SimplyGit.ViewModels {
    internal class DiffViewModel {
        private readonly PatchEntryChanges _difference;
        private readonly List<DiffStatus> _lineStyles = new List<DiffStatus>();

        public DiffViewModel(PatchEntryChanges difference) {
            _difference = difference;

            // TODO: be able to show changes as hunks!
            var lines = _difference.Patch.Split('\n').ToList();
            lines = lines.Skip(5).ToList();
            LineCount = lines.Count;

            for (int i = 0; i < lines.Count; i++) {
                if (lines[i].StartsWith("+")) {
                    _lineStyles.Add(DiffStatus.Added);
                    lines[i] = lines[i].Substring(1);
                    continue;
                }

                if (lines[i].StartsWith("-")) {
                    _lineStyles.Add(DiffStatus.Removed);
                    lines[i] = lines[i].Substring(1);
                    continue;
                }

                if (lines[i].StartsWith("!")) {
                    _lineStyles.Add(DiffStatus.Modified);
                    lines[i] = lines[i].Substring(1);
                    continue;
                }

                _lineStyles.Add(DiffStatus.Context);
            }

            Diff = string.Join("\n", lines);

            Document = new TextDocument();
            Document.Text = string.Join("\n", lines);
        }

        public TextDocument Document { get; }

        public ChangeKind ChangeKind => _difference.Status;
        public string FileName => _difference.Path;
        public string Diff { get; }

        public int LineCount { get; }

        public DiffStatus GetLineStyle(int lineNumber) {
            return _lineStyles[lineNumber];
        }
    }
}