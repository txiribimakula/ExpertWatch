using EnvDTE;
using System.Runtime.InteropServices;
using Txiribimakula.ExpertWatch.Loading.Exceptions;

namespace Txiribimakula.ExpertWatch.Loading
{
    public class ExpressionLoader
    {
        public ExpressionLoader(Expression expression) {
            this.expression = expression;
            Type = expression.Type;
            Name = expression.Name;
        }

        private Expression expression;
        public string Type { get; set; }
        public string Name { get; set; }

        public ExpressionLoader GetMember(params string[] names) {
            Expression expression = this.expression;
            foreach (var name in names) {
                try {
                    expression = expression.DataMembers.Item(name);
                } catch(COMException ex) {
                    throw new MemberNotFoundException(expression.Type, name);
                }
            }
            return new ExpressionLoader(expression);
        }

        public float GetFloatValue(params string[] names) {
            Expression expression = this.expression;
            foreach (var name in names) {
                expression = expression.DataMembers.Item(name);
            }
            return float.Parse(expression.Value, System.Globalization.NumberStyles.Float, System.Threading.Thread.CurrentThread.CurrentUICulture);
        }

        public string GetStringValue(params string[] names) {
            Expression expression = this.expression;
            foreach (var name in names) {
                expression = expression.DataMembers.Item(name);
            }
            return expression.Value;
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
