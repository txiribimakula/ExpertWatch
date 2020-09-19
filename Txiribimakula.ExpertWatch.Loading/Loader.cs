using EnvDTE;
using Txiribimakula.ExpertWatch.Drawing;
using System.Threading.Tasks;

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

        public async Task LoadAsync(WatchItem item) {
            await Task.Run(() => Load(item), item.TokenSource.Token);
        }

        private void Load(WatchItem item) {
            Expression expression = debugger.GetExpression(item.Name);

            if (expression != null && !string.IsNullOrEmpty(expression.Type)) {
                item.Description = expression.Type;
                ExpressionLoader expressionLoader = new ExpressionLoader(expression);
                DrawableCollection<IDrawable> drawables = null;
                if (Interpreter != null) {
                    drawables = Interpreter.GetDrawables(expressionLoader, item.TokenSource.Token);
                }
                item.Drawables = drawables;
            } else {
                item.Drawables.Error = "Variable could not be found.";
            }
        }
    }
}