using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Txiribimakula.ExpertWatch.Geometries.Contracts;
using Txiribimakula.ExpertWatch.Graphics.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class AxesDrawer
    {
        public ICoordinateSystem CoordinateSystem { get; set; }
        public Line XAxisLine { get; set; }
        public Line YAxisLine { get; set; }
        public Canvas Canvas { get; set; }

        public AxesDrawer(Canvas canvas) {
            Canvas = canvas;
            Canvas.Children.Clear();

            XAxisLine = new Line();
            YAxisLine = new Line();

            Color axesColor = Color.FromArgb(196, 0, 0, 0);
            SolidColorBrush axesStroke = new SolidColorBrush(axesColor);

            XAxisLine.Stroke = axesStroke;
            YAxisLine.Stroke = axesStroke;
            XAxisLine.StrokeThickness = 1;
            YAxisLine.StrokeThickness = 1;

            Canvas.Children.Add(XAxisLine);
            Canvas.Children.Add(YAxisLine);
        }

        public void Draw() {
            IPoint initialXAxisPoint = new Geometries.Point(0, CoordinateSystem.ConvertYToWorld(0));
            IPoint finalXAxisPoint = new Geometries.Point(CoordinateSystem.WorldWidth, CoordinateSystem.ConvertYToWorld(0));
            IPoint initialYAxisPoint = new Geometries.Point(CoordinateSystem.ConvertXToWorld(0), 0);
            IPoint finalYAxisPoint = new Geometries.Point(CoordinateSystem.ConvertXToWorld(0), CoordinateSystem.WorldHeight);

            YAxisLine.X1 = initialYAxisPoint.X;
            YAxisLine.Y1 = initialYAxisPoint.Y;
            YAxisLine.X2 = finalYAxisPoint.X;
            YAxisLine.Y2 = finalYAxisPoint.Y;

            XAxisLine.X1 = initialXAxisPoint.X;
            XAxisLine.Y1 = initialXAxisPoint.Y;
            XAxisLine.X2 = finalXAxisPoint.X;
            XAxisLine.Y2 = finalXAxisPoint.Y;
        }
    }
}