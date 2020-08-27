using Txiribimakula.ExpertWatch.DrawableGeometries;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;
using Txiribimakula.ExpertWatch.Loading;

namespace Txiribimakula.ExpertWatch.Models
{
    public class ArcInterpreter : IInterpreter
    {
        public IDrawable GetDrawable(ExpressionLoader expression) {
            ExpressionLoader initialPointLoader = expression.GetMember("InitialPoint");
            ExpressionLoader finalPointLoader = expression.GetMember("FinalPoint");

            IPoint initialPoint = new Point(initialPointLoader.GetValue("X"), initialPointLoader.GetValue("Y"));
            IPoint finalPoint = new Point(finalPointLoader.GetValue("X"), finalPointLoader.GetValue("Y"));
            float radius = expression.GetValue("Radius");

            return new DrawableArc(initialPoint, finalPoint, radius);
        }
    }
}
