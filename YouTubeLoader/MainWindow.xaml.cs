using System.Windows;
using System.Windows.Input;

namespace YouTubeLoader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Point _offset;

        private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(this, CaptureMode.Element);

            var cursorPos = PointToScreen(Mouse.GetPosition(this));
            var windowPos = new Point(Left, Top);
            _offset = (Point)(cursorPos - windowPos);
        }

        private void MainWindow_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
        }

        private void MainWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!Equals(Mouse.Captured, this) || Mouse.LeftButton != MouseButtonState.Pressed) return;

            var cursorPos = PointToScreen(Mouse.GetPosition(this));
            var newLeft = cursorPos.X - _offset.X;
            var newTop = cursorPos.Y - _offset.Y;
            Left = newLeft;
            Top = newTop;
        }
    }
}
