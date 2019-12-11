using System.Windows;
using ICSharpCode.AvalonEdit;

namespace SimplyGit.Views {
    public partial class CommitDetailsView {
        public CommitDetailsView() {
            InitializeComponent();
        }

        private void OnTextEditorLoaded(object sender, RoutedEventArgs e) {
            var avalonEdit = sender as TextEditor;
            avalonEdit?.TextArea.TextView.BackgroundRenderers.Add(new CustomDiffBackgroundRenderer());
        }
    }
}