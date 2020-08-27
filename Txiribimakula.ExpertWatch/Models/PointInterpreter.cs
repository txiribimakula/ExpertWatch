using Txiribimakula.ExpertWatch.DrawableGeometries;
using Txiribimakula.ExpertWatch.Loading;

namespace Txiribimakula.ExpertWatch.Models
{
    public class PointInterpreter : IInterpreter
    {
        public IDrawable GetDrawable(ExpressionLoader expression) {
            float x = expression.GetValue("X");
            float y = expression.GetValue("Y");

            return new DrawablePoint(x, y);
        }
    }
}
