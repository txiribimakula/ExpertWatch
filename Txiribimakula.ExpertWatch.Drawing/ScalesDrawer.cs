using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Txiribimakula.ExpertWatch.Graphics.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class ScalesDrawer
    {
        public ICoordinateSystem CoordinateSystem { get; set; }
        public Canvas Canvas { get; set; }

        private SolidColorBrush scalesStroke;

        public ScalesDrawer(Canvas canvas) {
            Canvas = canvas;
            Canvas.Children.Clear();
            scalesStroke = new SolidColorBrush(Color.FromArgb(196, 0, 0, 0));
        }

        public void DrawScales() {
            Canvas.Children.Clear();
            DrawXScale();
            DrawYScale();
        }

        private void DrawXScale() {
            float xStep = (float)(Utils.AbsOuterPow10(CoordinateSystem.LocalWidth) / 10.0);
            int xDecimalDigits = 0;
            if (xStep < 1) {
                xDecimalDigits = (int)Math.Round(Math.Log10(1 / xStep));
            }
            float xSmallSteps = 10;
            if (xStep * 3 > CoordinateSystem.LocalWidth) {
                xStep /= 2;
                xSmallSteps /= 2;
                xDecimalDigits += 1;
            }
            for (float x = (float)ScaleStart(CoordinateSystem.LocalMinX, xStep) - xStep; x < CoordinateSystem.LocalMaxX; x += xStep) {
                if (x != 0) {
                    Line newLine = new Line();
                    newLine.X1 = CoordinateSystem.ConvertXToWorld(x);
                    newLine.Y1 = CoordinateSystem.WorldHeight;
                    newLine.X2 = CoordinateSystem.ConvertXToWorld(x);
                    newLine.Y2 = CoordinateSystem.WorldHeight - 5;
                    newLine.Stroke = scalesStroke;
                    newLine.StrokeThickness = 1;
                    Canvas.Children.Add(newLine);
                    // Graphics.DrawLine(pen, CoordinateSystem.ConvertXToWorld(x), CoordinateSystem.WorldHeight, CoordinateSystem.ConvertXToWorld(x), CoordinateSystem.WorldHeight - 5);
                    // float roundedX = (float)Math.Round(x, xDecimalDigits);
                    // SizeF xStrSize = Graphics.MeasureString(roundedX.ToString(), font);
                    // Graphics.DrawString(roundedX.ToString(), font, brush, CoordinateSystem.ConvertXToWorld(x) - (xStrSize.Width / 2), CoordinateSystem.WorldHeight - xStrSize.Height - 3);
                }
                for (float xSmall = x + (xStep / xSmallSteps); xSmall < x + xStep; xSmall += xStep / xSmallSteps) {
                    float xSmallAux = CoordinateSystem.ConvertXToWorld(xSmall);
                    Line newLine = new Line();
                    newLine.X1 = xSmallAux;
                    newLine.Y1 = CoordinateSystem.WorldHeight;
                    newLine.X2 = xSmallAux;
                    newLine.Y2 = CoordinateSystem.WorldHeight - 3;
                    newLine.Stroke = scalesStroke;
                    newLine.StrokeThickness = 1;
                    Canvas.Children.Add(newLine);
                }
            }
        }

        private void DrawYScale() {
            float yStep = (float)(Utils.AbsOuterPow10(CoordinateSystem.LocalHeight) / 10.0);
            int yDecimalDigits = 0;
            if (yStep < 1) {
                yDecimalDigits = (int)Math.Round(Math.Log10(1 / yStep));
            }
            float ySmallSteps = 10;
            if (yStep * 3 > CoordinateSystem.LocalHeight) {
                yStep /= 2;
                ySmallSteps /= 2;
                yDecimalDigits += 1;
            }
            for (float y = (float)ScaleStart(CoordinateSystem.LocalMinY, yStep) - yStep; y < CoordinateSystem.LocalMaxY; y += yStep) {
                if (y != 0) {
                    Line newLine = new Line();
                    newLine.X1 = CoordinateSystem.WorldWidth;
                    newLine.Y1 = CoordinateSystem.ConvertYToWorld(y);
                    newLine.X2 = CoordinateSystem.WorldWidth - 5;
                    newLine.Y2 = CoordinateSystem.ConvertYToWorld(y);
                    newLine.Stroke = scalesStroke;
                    newLine.StrokeThickness = 1;
                    Canvas.Children.Add(newLine);
                    // float roundedY = (float)Math.Round(y, yDecimalDigits);
                    // SizeF yStrSize = Graphics.MeasureString(roundedY.ToString(), font);
                    // Graphics.DrawString(roundedY.ToString(), font, brush, CoordinateSystem.WorldWidth - yStrSize.Width - 3, CoordinateSystem.ConvertYToWorld(y) - (yStrSize.Height / 2));
                }
                for (float ySmall = y + (yStep / ySmallSteps); ySmall < y + yStep; ySmall += yStep / ySmallSteps) {
                    float ySmallAux = CoordinateSystem.ConvertYToWorld(ySmall);
                    Line newLine = new Line();
                    newLine.X1 = CoordinateSystem.WorldWidth;
                    newLine.Y1 = ySmallAux;
                    newLine.X2 = CoordinateSystem.WorldWidth - 3;
                    newLine.Y2 = ySmallAux;
                    newLine.Stroke = scalesStroke;
                    newLine.StrokeThickness = 1;
                    Canvas.Children.Add(newLine);
                }
            }
        }

        private static double ScaleStart(double val, double step) {
            double r = val / step;
            double i = val >= 0 ? Math.Ceiling(r) : Math.Floor(r);
            return i * step;
        }
    }
}
