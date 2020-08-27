using EnvDTE;
using Txiribimakula.ExpertWatch.DrawableGeometries;
using System.Threading.Tasks;
using System.Threading;

namespace Txiribimakula.ExpertWatch.Loading
{
    public class Loader
    {
        private Debugger debugger;
        private IInterpreter interpreter;

        public Loader(Debugger debugger, IInterpreter interpreterSelector) {
            this.debugger = debugger;
            this.interpreter = interpreterSelector;
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
                if (interpreter != null) {
                    drawable = interpreter.GetDrawable(expressionLoader);
                }
                watchItem.Drawables.Add(drawable);
            } else {
                watchItem.Description = "Variable could not be found.";
            }
            return watchItem;
        }
    }
}