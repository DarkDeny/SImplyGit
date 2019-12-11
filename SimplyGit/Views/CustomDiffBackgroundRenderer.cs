using System.Windows;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Rendering;
using SimplyGit.ViewModels;

namespace SimplyGit.Views {
    public class CustomDiffBackgroundRenderer : IBackgroundRenderer {
        static CustomDiffBackgroundRenderer() {
            AddedBackground = new SolidColorBrush(Color.FromRgb(146, 236, 146));
            AddedBackground.Freeze();

            RemovedBackground = new SolidColorBrush(Color.FromRgb(238, 167, 167));
            RemovedBackground.Freeze();

            BlankBackground = new SolidColorBrush(Color.FromRgb(109, 109, 109));
            BlankBackground.Freeze();

            var transparentBrush = new SolidColorBrush(Colors.Transparent);
            transparentBrush.Freeze();

            BorderlessPen = new Pen(transparentBrush, 0.0);
            BorderlessPen.Freeze();
        }

        private static readonly Brush BlankBackground;
        private static readonly Brush ModifiedBackground;
        private static readonly Brush AddedBackground;
        private static readonly Brush RemovedBackground;
        private static readonly Pen BorderlessPen;

        /// <summary>Causes the background renderer to draw.</summary>
        public void Draw(TextView textView, DrawingContext drawingContext) {
            var dvm = textView.DataContext as DiffViewModel;
            if (null == dvm) {
                return;
            }

            foreach (var visualLine in textView.VisualLines) {
                var lineNumber = visualLine.FirstDocumentLine.LineNumber - 1;
                if (lineNumber >= dvm.LineCount) continue;

                var diffLineStyle = dvm.GetLineStyle(lineNumber);

                if (diffLineStyle == DiffStatus.Context) continue;

                var brush = default(Brush);
                switch (diffLineStyle) {
                    case DiffStatus.Added:
                        brush = AddedBackground;
                        break;
                    case DiffStatus.Removed:
                        brush = RemovedBackground;
                        break;
                    case DiffStatus.Context:
                        brush = BlankBackground;
                        break;
                    case DiffStatus.Modified:
                        brush = ModifiedBackground;
                        break;
                }

                foreach (var rc in BackgroundGeometryBuilder.GetRectsFromVisualSegment(textView, visualLine, 0, 1000)) {
                    drawingContext.DrawRectangle(brush, BorderlessPen,
                        new Rect(0, rc.Top, textView.ActualWidth, rc.Height));
                }
            }
        }

        /// <summary>
        /// Gets the layer on which this background renderer should draw.
        /// </summary>
        public KnownLayer Layer => KnownLayer.Background;
    }
}