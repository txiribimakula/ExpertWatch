using Txiribimakula.ExpertWatch.Drawing;
using Txiribimakula.ExpertWatch.Loading;

namespace Txiribimakula.ExpertWatch.Models
{
    public static class PointInterpreter
    {
        public static IDrawable GetDrawable(ExpressionLoader expression) {
            float x = expression.GetValue("X");
            float y = expression.GetValue("Y");

            return new DrawablePoint(x, y);
        }
    }
}
