using EnvDTE;
using Txiribimakula.ExpertWatch.Drawing;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Txiribimakula.ExpertWatch.Loading
{
    public class Loader
    {
        private Debugger debugger;
        public IInterpreter Interpreter { get; set; }

        public Loader(Debugger debugger, IInterpreter interpreterSelector) {
            this.debugger = debugger;
            this.Interpreter = interpreterSelector;
        }

        public void Load(WatchItem item, BackgroundWorker backgroundWorker) {
            Expression expression = debugger.GetExpression(item.Name);

            if (expression != null && !string.IsNullOrEmpty(expression.Type)) {
                item.Description = expression.Type;
                ExpressionLoader expressionLoader = new ExpressionLoader(expression);
                if (Interpreter != null) {
                    Interpreter.GetDrawables(expressionLoader, backgroundWorker);
                }
            } else {
                item.Drawables.Error = "Variable could not be found.";
            }
        }
    }
}