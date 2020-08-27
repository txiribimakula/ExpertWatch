using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class CursorDrawer
    {
        public Canvas CursorCanvas { get; set; }
        Line CursorVLine { get; set; }
        Line CursorHLine { get; set; }

        public CursorDrawer(Canvas canvas) {
            CursorCanvas = canvas;

            CursorVLine = new Line();
            CursorHLine = new Line();

            Color linesColor = Color.FromArgb(32, 0, 0, 0);
            CursorHLine.Stroke = new SolidColorBrush(linesColor);
            CursorHLine.Visibility = Visibility.Hidden;
            CursorVLine.Stroke = new SolidColorBrush(linesColor);
            CursorVLine.Visibility = Visibility.Hidden;
            CursorCanvas.Children.Add(CursorHLine);
            CursorCanvas.Children.Add(CursorVLine);
        }

        public void Draw(IPoint point) {
            CursorHLine.X1 = 0;
            CursorHLine.Y1 = point.Y;
            CursorHLine.X2 = CursorCanvas.ActualWidth;
            CursorHLine.Y2 = point.Y;
            CursorHLine.Visibility = Visibility.Visible;

            CursorVLine.X1 = point.X;
            CursorVLine.Y1 = 0;
            CursorVLine.X2 = point.X;
            CursorVLine.Y2 = CursorCanvas.ActualHeight;
            CursorVLine.Visibility = Visibility.Visible;
        }

        public void Clear() {
            CursorHLine.Visibility = Visibility.Hidden;
            CursorVLine.Visibility = Visibility.Hidden;
        }
    }
}
