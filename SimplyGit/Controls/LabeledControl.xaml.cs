using System.Windows;
using System.Windows.Markup;

namespace SimplyGit.Controls {
    [ContentProperty("Data")]
    public partial class LabeledControl {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(string), typeof(LabeledControl), new PropertyMetadata(default(string)));

        public string Label {
            get { return (string) GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data", typeof(object), typeof(LabeledControl), new PropertyMetadata(default(object)));

        public object Data {
            get { return (object) GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty LabelAlignmentProperty = DependencyProperty.Register(
            "LabelAlignment", typeof(HorizontalAlignment), typeof(LabeledControl),
            new PropertyMetadata(HorizontalAlignment.Right));

        public HorizontalAlignment LabelAlignment {
            get { return (HorizontalAlignment) GetValue(LabelAlignmentProperty); }
            set { SetValue(LabelAlignmentProperty, value); }
        }

        public static readonly DependencyProperty DetailsProperty = DependencyProperty.Register(
            "Details", typeof(object), typeof(LabeledControl), new PropertyMetadata(default(object)));

        public object Details {
            get { return (object) GetValue(DetailsProperty); }
            set { SetValue(DetailsProperty, value); }
        }

        public LabeledControl() {
            InitializeComponent();
        }
    }
}
