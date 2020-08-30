using System.Collections.ObjectModel;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class GeometryDrawer
    {
        public DrawableVisitor DrawableVisitor { get; set; }

        public GeometryDrawer(DrawableVisitor visitor) {
            DrawableVisitor = visitor;
        }

        public void TransformGeometries(DrawableCollection<IDrawable> drawables) {
            foreach (var drawable in drawables) {
                drawable.TransformGeometry(DrawableVisitor);
            }
        }
    }
}
