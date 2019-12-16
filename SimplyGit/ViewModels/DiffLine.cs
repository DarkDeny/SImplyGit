using SimplyGit.Helpers;
using SimplyGit.Views;

namespace SimplyGit.ViewModels {
    internal class DiffLine {
        public int? LineNumberLeft { get; set; }
        public int? LineNumberRight { get; set; }
        public string LineText { get; set; }
        public DiffStatus LineStatus { get; set; }
    }
}