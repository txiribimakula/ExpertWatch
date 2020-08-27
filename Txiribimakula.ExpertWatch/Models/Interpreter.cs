using Txiribimakula.ExpertWatch.Drawing;
using Txiribimakula.ExpertWatch.Loading;

namespace Txiribimakula.ExpertWatch.Models
{
    public class Interpreter : IInterpreter
    {
        public IDrawable GetDrawable(ExpressionLoader expressionLoader) {
            switch (expressionLoader.Type) {
                case "Txiribimakula.ExpertWatch.Geometries.Segment":
                    return SegmentInterpreter.GetDrawable(expressionLoader);
                case "Txiribimakula.ExpertWatch.Geometries.Arc":
                    return ArcInterpreter.GetDrawable(expressionLoader);
                case "Txiribimakula.ExpertWatch.Geometries.Point":
                    return PointInterpreter.GetDrawable(expressionLoader);
                default:
                    return null;
            }
        }
    }
}
