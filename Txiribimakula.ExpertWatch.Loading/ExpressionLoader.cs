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

        public ExpressionLoader[] GetMembers() {
            Expressions expressions = expression.DataMembers;
            ExpressionLoader[] expressionsLoaders = new ExpressionLoader[expressions.Count];
            int i = 0;
            foreach (Expression expression in expressions) {
                expressionsLoaders[i] = new ExpressionLoader(expression);
                i++;
            }
            return expressionsLoaders;
        }
    }
} 
