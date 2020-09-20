using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Txiribimakula.ExpertWatch.Drawing;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;
using Txiribimakula.ExpertWatch.Loading.Exceptions;

namespace Txiribimakula.ExpertWatch.Loading
{
    public class Interpreter : IInterpreter {
        public Interpreter() {
            interpreters = new Dictionary<string, Blueprint>();
        }

        public Interpreter(string jsonText) {
            interpreters = new Dictionary<string, Blueprint>();
            Blueprint[] blueprints = JsonConvert.DeserializeObject<Blueprint[]>(jsonText);
            foreach (var blueprint in blueprints) {
                foreach (var key in blueprint.Keys) {
                    interpreters.Add(key, blueprint);
                }
            }
        }

        private Dictionary<string, Blueprint> interpreters;

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            Tuple<ExpressionLoader, BackgroundWorker> tuple = (Tuple<ExpressionLoader, BackgroundWorker>)e.Argument;
            DoGetDrawables(tuple.Item1, tuple.Item2);
        }

        private void DoGetDrawables(ExpressionLoader expressionLoader, BackgroundWorker backgroundWorker) {
            Blueprint interpreter;
            interpreters.TryGetValue(expressionLoader.Type, out interpreter);
            if (interpreter != null) {
                ExpressionLoader currentExpressionLoader = expressionLoader;
                if (interpreter.Root.Key == "segment") {
                    ISegment segment = GetSegment(currentExpressionLoader, interpreter.Root);
                    DrawableSegment drawableSegment = new DrawableSegment(segment);
                    backgroundWorker.ReportProgress(1, drawableSegment);
                } else if (interpreter.Root.Key == "arc") {
                    IArc arc = GetArc(currentExpressionLoader, interpreter.Root);
                    DrawableArc drawableArc = new DrawableArc(arc);
                    backgroundWorker.ReportProgress(1, drawableArc);
                } else if (interpreter.Root.Key == "point") {
                    IPoint point = GetPoint(currentExpressionLoader, interpreter.Root);
                    DrawablePoint drawablePoint = new DrawablePoint(point);
                    backgroundWorker.ReportProgress(1, drawablePoint);
                } else if (interpreter.Root.Key == "list") {
                    ExpressionLoader[] expressionLoaders = currentExpressionLoader.GetMembers();
                    for (int i = 0; i < expressionLoaders.Length - 1; i++) {
                        DoGetDrawables(currentExpressionLoader.GetMember("[" + i + "]"), backgroundWorker);
                        if (backgroundWorker.CancellationPending) {
                            return;
                        }
                    }
                }
            } else {
                throw new LoadingException("No interpreter found");
            }
        }

        

        public void GetDrawables(ExpressionLoader expressionLoader, BackgroundWorker backgroundWorker) {
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.RunWorkerAsync(new Tuple<ExpressionLoader, BackgroundWorker>(expressionLoader, backgroundWorker));
        }

        private IArc GetArc(ExpressionLoader expressionLoader, BlueprintNode interpreterNode) {
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

        private ISegment GetSegment(ExpressionLoader expressionLoader, BlueprintNode interpreterNode) {
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

        private IPoint GetPoint(ExpressionLoader expressionLaoder, BlueprintNode interpreterNode) {
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
