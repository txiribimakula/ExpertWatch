using System.Threading;
using Txiribimakula.ExpertWatch.Drawing;

namespace Txiribimakula.ExpertWatch.Loading
{
    public interface IInterpreter
    {
        DrawableCollection<IDrawable> GetDrawables(ExpressionLoader expression, CancellationToken token);
    }
}
