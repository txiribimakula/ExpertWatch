using Txiribimakula.ExpertWatch.Drawing;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;
using Txiribimakula.ExpertWatch.Loading;

namespace Txiribimakula.ExpertWatch.Models
{
    public static class ArcInterpreter
    {
        public static IDrawable GetDrawable(ExpressionLoader expression) {
            ExpressionLoader centerPointLoader = expression.GetMember("CenterPoint");

            IPoint centerPoint = new Point(centerPointLoader.GetValue("X"), centerPointLoader.GetValue("Y"));
            float radius = expression.GetValue("Radius");
            float initialAngle = expression.GetValue("InitialAngle");
            float sweepAngle = expression.GetValue("SweepAngle");

            return new DrawableArc(centerPoint, initialAngle, sweepAngle, radius);
        }
    }
}
