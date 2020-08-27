using EnvDTE;

namespace Txiribimakula.ExpertWatch.Loading
{
    public class ExpressionLoader
    {
        private Expression expression;
        public ExpressionLoader(Expression expression) {
            this.expression = expression;
        }

        public ExpressionLoader GetMember(params string[] names) {
            Expression expression = this.expression;
            foreach (var name in names) {
                expression = expression.DataMembers.Item(name);
            }
            return new ExpressionLoader(expression);
        }

        public float GetValue(params string[] names) {
            Expression expression = this.expression;
            foreach (var name in names) {
                expression = expression.DataMembers.Item(name);
            }
            return float.Parse(expression.Value);
        }
        public string Type { get { return this.expression.Type; } }
    }
}
