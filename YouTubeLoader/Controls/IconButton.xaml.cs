using System;
using System.Windows;
using System.Windows.Media;

namespace YouTubeLoader.Controls
{
    /// <summary>
    /// Interaction logic for IconButton.xaml
    /// </summary>
    public partial class IconButton
    {
        public IconButton()
        {
            InitializeComponent();
        }

        public PathGeometry IconGeometry
        {
            get { return (PathGeometry)GetValue(IconGeometryProperty); }
            set { SetValue(IconGeometryProperty, value); }
        }

        public double ViewboxWidth
        {
            get { return (double)GetValue(ViewboxWidthProperty); }
            set { SetValue(ViewboxWidthProperty, value); }
        }

        public static readonly DependencyProperty IconGeometryProperty = DependencyProperty.Register("IconGeometry", typeof(PathGeometry),
            typeof(IconButton), new PropertyMetadata(default(PathGeometry)));

        public static readonly DependencyProperty ViewboxWidthProperty = DependencyProperty.Register("ViewboxWidth", typeof(double),
            typeof(IconButton), new PropertyMetadata(default(double)));
    }
}
