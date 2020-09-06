using EnvDTE;

namespace Txiribimakula.ExpertWatch.Loading
{
    public class ExpressionLoader
    {
        public ExpressionLoader(Expression expression) {
            this.expression = expression;
            Type = expression.Type;
        }

        private Expression expression;
        public string Type { get; set; }

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

    }
} 
