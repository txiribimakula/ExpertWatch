using Newtonsoft.Json;
using System.Collections.Generic;
using Txiribimakula.ExpertWatch.Drawing;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;
using Txiribimakula.ExpertWatch.Loading;
using Txiribimakula.ExpertWatch.Models.Interpreters;

namespace Txiribimakula.ExpertWatch.Models
{
    public class Interpreter : IInterpreter
    {
        public Interpreter() {
            interpreters = new Dictionary<string, InterpreterNode>();
        }

        public Interpreter(string jsonText) {
            interpreters = new Dictionary<string, InterpreterNode>();
            InterpreterNode[] nodes = JsonConvert.DeserializeObject<InterpreterNode[]>(jsonText);
            foreach (var node in nodes) {
                interpreters.Add(node.Key, node);
            }
        }

        private Dictionary<string, InterpreterNode> interpreters;

        public IDrawable GetDrawable(ExpressionLoader expressionLoader) {
            InterpreterNode interpreterNode;
            interpreters.TryGetValue(expressionLoader.Type, out interpreterNode);

            interpreterNode = interpreterNode.Members[0]; // create root object with only one member?

            if (interpreterNode != null) {
                ExpressionLoader currentExpressionLoader = expressionLoader;
                if (!string.IsNullOrWhiteSpace(interpreterNode.Name)) {
                    currentExpressionLoader = currentExpressionLoader.GetMember(interpreterNode.Name);
                }
                IDrawable drawable = null;
                if(interpreterNode.Key == "segment") {
                    ISegment segment = GetSegment(currentExpressionLoader, interpreterNode);
                    drawable = new DrawableSegment(segment);
                } else if (interpreterNode.Key == "arc") {
                    IArc arc = GetArc(currentExpressionLoader, interpreterNode);
                    drawable = new DrawableArc(arc);
                } else if (interpreterNode.Key == "point") {
                    IPoint point = GetPoint(currentExpressionLoader, interpreterNode);
                    drawable = new DrawablePoint(point);
                }
                return drawable;
            } else {
                return null;
            }

        }

        private IArc GetArc(ExpressionLoader expressionLoader, InterpreterNode interpreterNode) {
            ExpressionLoader currentExpressionLoader = expressionLoader;
            if (!string.IsNullOrWhiteSpace(interpreterNode.Name)) {
                currentExpressionLoader = currentExpressionLoader.GetMember(interpreterNode.Name);
            }
            IPoint centerPoint = null;
            float? initialAngle = null;
            float? sweepAngle = null;
            float? radius = null;
            foreach (var node in interpreterNode.Members) {
                switch (node.Key) {
                    case "centerPoint":
                        centerPoint = GetPoint(currentExpressionLoader, node);
                        break;
                    case "initialAngle":
                        initialAngle = currentExpressionLoader.GetValue(node.Name);
                        break;
                    case "sweepAngle":
                        sweepAngle = currentExpressionLoader.GetValue(node.Name);
                        break;
                    case "radius":
                        radius = currentExpressionLoader.GetValue(node.Name);
                        break;
                    case "":
                    case null:
                        return GetArc(currentExpressionLoader, node);
                }
            }
            if (centerPoint != null && initialAngle != null && sweepAngle != null && radius != null) {
                return new Arc(centerPoint, initialAngle.GetValueOrDefault(), sweepAngle.GetValueOrDefault(), radius.GetValueOrDefault());
            } else {
                return null;
            }
        }

        private ISegment GetSegment(ExpressionLoader expressionLoader, InterpreterNode interpreterNode) {
            ExpressionLoader currentExpressionLoader = expressionLoader;
            if (!string.IsNullOrWhiteSpace(interpreterNode.Name)) {
                currentExpressionLoader = currentExpressionLoader.GetMember(interpreterNode.Name);
            }
            IPoint initialPoint = null;
            IPoint finalPoint = null;
            foreach (var node in interpreterNode.Members) {
                switch (node.Key) {
                    case "initialPoint":
                        initialPoint = GetPoint(currentExpressionLoader, node);
                        break;
                    case "finalPoint":
                        finalPoint = GetPoint(currentExpressionLoader, node);
                        break;
                    case "":
                    case null:
                        return GetSegment(currentExpressionLoader, node);
                }
            }
            if(initialPoint != null && finalPoint != null) {
                return new Segment(initialPoint, finalPoint);
            } else {
                return null;
            }
        }

        private IPoint GetPoint(ExpressionLoader expressionLaoder, InterpreterNode interpreterNode) {
            ExpressionLoader currentExpressionLoader = expressionLaoder;
            if (!string.IsNullOrWhiteSpace(interpreterNode.Name)) {
                currentExpressionLoader = currentExpressionLoader.GetMember(interpreterNode.Name);
            }
            float? x = null, y = null;
            foreach (var node in interpreterNode.Members) {
                switch (node.Key) {
                    case "x":
                        x = currentExpressionLoader.GetValue(node.Name);
                        break;
                    case "y":
                        y = currentExpressionLoader.GetValue(node.Name);
                        break;
                    case "":
                    case null:
                        return GetPoint(currentExpressionLoader, node);
                }
            }
            if(x != null && y != null) {
                return new Point(x.GetValueOrDefault(), y.GetValueOrDefault());
            } else {
                return null;
            }
        }
    }
}
