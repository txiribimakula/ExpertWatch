using System.Collections.Generic;
using Txiribimakula.ExpertWatch.DrawableGeometries;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;
using Txiribimakula.ExpertWatch.Graphics.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class Engine
    {
        ICoordinateSystem CoordinateSystem { get; set; }
        CursorDrawer CursorDrawer { get; set; }
        AxesDrawer AxesDrawer { get; set; }
        ScalesDrawer ScalesDrawer { get; set; }
        GeometryDrawer GeometryDrawer { get; set; }

        public Engine(ICoordinateSystem coordinateSystem, CursorDrawer cursorDrawer, AxesDrawer axesDrawer, ScalesDrawer scalesDrawer, GeometryDrawer geometryDrawer) {
            CoordinateSystem = coordinateSystem;
            CursorDrawer = cursorDrawer;
            AxesDrawer = axesDrawer;
            ScalesDrawer = scalesDrawer;
            GeometryDrawer = geometryDrawer;
        }


        private void ReDraw() {
            DrawAxes();
            DrawScales();
            //DrawGeometries(GeometryDrawer.Drawables);
        }

        public void DrawCursor(IPoint point) {
            CursorDrawer.Draw(point);
        }
        public void ClearCursor() {
            CursorDrawer.Clear();
        }

        public void DrawAxes() {
            AxesDrawer.CoordinateSystem = CoordinateSystem;
            AxesDrawer.Draw();
        }

        public void DrawScales() {
            ScalesDrawer.CoordinateSystem = CoordinateSystem;
            ScalesDrawer.DrawScales();
        }

        public void Zoom(int delta, IPoint worldPoint) {
            Point localPoint = new Point(CoordinateSystem.ConvertXToLocal(worldPoint.X), CoordinateSystem.ConvertYToLocal(worldPoint.Y));
            if (delta < 0) {
                CoordinateSystem.Scale *= 1.1f;
            } else {
                CoordinateSystem.Scale /= 1.1f;
            }
            float newWorldPointX = CoordinateSystem.ConvertXToWorld(localPoint.X);
            float newWorldPointY = CoordinateSystem.ConvertYToWorld(localPoint.Y);

            CoordinateSystem.Offset = new Point((float)worldPoint.X - newWorldPointX, (float)worldPoint.Y - newWorldPointY);

            ReDraw();
        }

        public void Pan(IPoint fromPoint, IPoint toPoint) {
            float incrementalX = toPoint.X - fromPoint.X;
            float incrementalY = toPoint.Y - fromPoint.Y;
            CoordinateSystem.Offset = new Point(incrementalX, incrementalY);

            ReDraw();
        }

        public void DrawGeometries(List<IDrawable> drawables) {
            //GeometryDrawer.Drawables = drawables;
            //GeometryDrawer.CoordinateSystem = CoordinateSystem;
            //GeometryDrawer.Redraw();
        }
    }
}
