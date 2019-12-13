using System.Collections.Generic;
using System.Windows;
using Infrastructure;
using Microsoft.Practices.Prism.Commands;

namespace SimplyGit.ViewModels {
    internal class Hunk :ViewModelBase {
        public Hunk() {
            DiffLines = new List<DiffLine>();
            ReverseHunk = new DelegateCommand(DoReverseHunk);
        }

        private void DoReverseHunk() {
            MessageBox.Show("not implemented yet", "Wow");
        }

        public IList<DiffLine> DiffLines { get; }

        public DelegateCommand ReverseHunk { get; }
    }
}