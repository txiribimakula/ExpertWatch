using EnvDTE;
using Txiribimakula.ExpertWatch.Drawing;
using System.Threading.Tasks;
using System.Threading;

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

        public async Task<WatchItem> LoadAsync(string newInputName) {
            var source = new CancellationTokenSource();
            var token = source.Token;
            return await Task.Run(() => Load(newInputName, token), token);
        }

        private WatchItem Load(string newInputName, CancellationToken token) {
            WatchItem watchItem = new WatchItem();
            
            Expression expression = debugger.GetExpression(newInputName);

            if (expression != null && !string.IsNullOrEmpty(expression.Type)) {
                watchItem.Description = expression.Type;
                ExpressionLoader expressionLoader = new ExpressionLoader(expression);
                IDrawable drawable = null;
                if (Interpreter != null) {
                    drawable = Interpreter.GetDrawable(expressionLoader);
                }
                watchItem.Drawables.Add(drawable);
            } else {
                watchItem.Description = "Variable could not be found.";
            }
            return watchItem;
        }
    }
}