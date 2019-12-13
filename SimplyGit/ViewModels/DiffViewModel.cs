using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;
using SimplyGit.Views;

namespace SimplyGit.ViewModels {
    internal class DiffViewModel {
        private readonly PatchEntryChanges _difference;

        public DiffViewModel(PatchEntryChanges difference) {
            _difference = difference;
            Hunks = new List<Hunk>();

            var lines = _difference.Patch.Split('\n').ToList();
            lines = lines.Skip(4).ToList();

            Hunk currentHunk = null;
            int curLeftLine = 0;
            int curRightLine = 0;
            foreach (var line in lines) {
                if (line.StartsWith("@@")) {
                    // TODO: extract line numbers data!
                    var substring = line.Substring(2);
                    var index = substring.IndexOf("@@");
                    substring = substring.Substring(0, index);
                    var startIndex1 = substring.IndexOf("-");
                    var endIndex1 = substring.IndexOf(",");
                    var leftStartingLine = substring.Substring(startIndex1 + 1, endIndex1 - startIndex1 - 1);

                    var startIndex2 = substring.IndexOf("+");
                    var tail = substring.Substring(startIndex2);

                    var endIndex2 = tail.IndexOf(",");
                    var rightStartingLine = tail.Substring( 1, endIndex2 - 1);

                    int.TryParse(leftStartingLine, out curLeftLine);
                    int.TryParse(rightStartingLine, out curRightLine);

                    currentHunk = new Hunk();
                    Hunks.Add(currentHunk);
                    continue;
                }

                if (null == currentHunk) {
                    continue;
                }

                var diffLine = new DiffLine();
                currentHunk.DiffLines.Add(diffLine);
                diffLine.LineText = line;
                if (line.StartsWith("+")) {
                    diffLine.LineStatus = DiffStatus.Added;
                    diffLine.LineNumberRight = curRightLine;
                    curRightLine++;
                } else if (line.StartsWith("-")) {
                    diffLine.LineStatus = DiffStatus.Removed;
                    diffLine.LineNumberLeft = curLeftLine;
                    curLeftLine++;
                }
                else {
                    diffLine.LineStatus = DiffStatus.Context;
                    diffLine.LineNumberRight = curRightLine;
                    curRightLine++;
                    diffLine.LineNumberLeft = curLeftLine;
                    curLeftLine++;
                }
            }
        }

        public List<Hunk> Hunks { get; }
        public ChangeKind ChangeKind => _difference.Status;
        public string FileName => _difference.Path;
    }
}