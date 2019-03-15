using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimplyGit.Controls {
    public class ImageButton : Button {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(Geometry), typeof(ImageButton), new PropertyMetadata(default(Geometry)));

        public Geometry Icon {
            get { return (Geometry) GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(ImageButton), new PropertyMetadata(default(string)));

        public string Text {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty IconColorProperty = DependencyProperty.Register(
            "IconColor", typeof(Brush), typeof(ImageButton),
            new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 133, 223))));

        public Brush IconColor {
            get { return (Brush) GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register(
            "IconHeight", typeof(double), typeof(ImageButton), new PropertyMetadata((double) 17));

        public double IconHeight {
            get { return (double) GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }
    }
}