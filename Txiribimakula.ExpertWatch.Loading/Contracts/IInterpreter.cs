using System.ComponentModel;

namespace Txiribimakula.ExpertWatch.Loading
{
    public interface IInterpreter
    {
        void GetDrawables(ExpressionLoader expression, BackgroundWorker backgroundWorker);
    }
}
