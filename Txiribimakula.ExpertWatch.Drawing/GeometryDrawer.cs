using System.Collections.ObjectModel;
using Txiribimakula.ExpertWatch.DrawableGeometries;
using Txiribimakula.ExpertWatch.Graphics.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class GeometryDrawer
    {
        public ICoordinateSystem CoordinateSystem { get; set; }
        public DrawableVisitor DrawableVisitor { get; set; }

        public GeometryDrawer(ICoordinateSystem coordinateSystem) {
            CoordinateSystem = coordinateSystem;
            DrawableVisitor = new DrawableVisitor(CoordinateSystem);
        }

        public void TransformGeometries(ObservableCollection<IDrawable> drawables) {
            foreach (var drawable in drawables) {
                drawable.TransformGeometry(DrawableVisitor);
            }
        }
    }
}
