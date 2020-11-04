using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
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
        private int currentProgress = 0;

        private List<IDrawable> drawables;

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            Tuple<ExpressionLoader, BackgroundWorker> tuple = (Tuple<ExpressionLoader, BackgroundWorker>)e.Argument;

            drawables = new List<IDrawable>();

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += (timerSender, args) => {
                if (drawables.Count > 0) {
                    int currentDrawablesCount = drawables.Count;
                    tuple.Item2.ReportProgress(currentProgress, drawables.GetRange(0, currentDrawablesCount));
                    drawables.RemoveRange(0, currentDrawablesCount);
                }
            };
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
            DoGetDrawables(tuple.Item1, tuple.Item2);
            aTimer.Enabled = false;
            int drawablesCount = drawables.Count;
            tuple.Item2.ReportProgress(currentProgress, drawables.GetRange(0, drawablesCount));
            drawables.RemoveRange(0, drawablesCount);
        }

        private void DoGetDrawables(ExpressionLoader expressionLoader, BackgroundWorker backgroundWorker) {
            Blueprint interpreter;
            interpreters.TryGetValue(expressionLoader.Type, out interpreter);
            if (interpreter != null) {
                if (interpreter.Root.Key == "list") {
                    ExpressionLoader[] expressionLoaders = expressionLoader.GetMembers();
                    float totalCount = expressionLoaders.Length - 1;
                    for (int i = 0; i < totalCount; i++) {
                        Blueprint nextInterpreter;
                        interpreters.TryGetValue(expressionLoaders[i].Type, out nextInterpreter);
                        currentProgress = Convert.ToInt32(((i + 1) / totalCount) * 100);
                        DoGetDrawables(expressionLoaders[i], backgroundWorker);
                        if (backgroundWorker.CancellationPending) {
                            return;
                        }
                    }
                } else if (interpreter.Root.Key == "blueprint") {
                    DoGetDrawables(expressionLoader.GetMember(interpreter.Root.Name), backgroundWorker);
                } else if (interpreter.Root.Key == "switch") {
                    string caseValue = expressionLoader.GetStringValue(interpreter.Root.Name);
                    BlueprintNode caseNode = interpreter.Root.Members.FirstOrDefault(node => node.Value == caseValue);
                    DoGetDrawables(expressionLoader.GetMember(caseNode.Name), backgroundWorker);
                } else if (interpreter.Root.Key == "linked-list") {
                    ExpressionLoader nextExpressionLoader = expressionLoader;
                    while (nextExpressionLoader.GetStringValue() != interpreter.Root.Value) {
                        DoGetDrawables(nextExpressionLoader.GetMember(interpreter.Root.Members[0].Name), backgroundWorker);
                        nextExpressionLoader = nextExpressionLoader.GetMember(interpreter.Root.Name);
                        if (backgroundWorker.CancellationPending) {
                            return;
                        }
                    }
                    backgroundWorker.ReportProgress(100, null);
                } else {
                    IDrawable drawable = GetDrawable(expressionLoader, interpreter);
                    if(drawable != null) {
                        drawables.Add(drawable);
                    }
                }
            } else {
                throw new LoadingException("No interpreter found");
            }
        }

        private IDrawable GetDrawable(ExpressionLoader expressionLoader, Blueprint interpreter) {
            if (interpreter.Root.Key == "segment") {
                ISegment segment = GetSegment(expressionLoader, interpreter.Root);
                return new DrawableSegment(segment);
            } else if (interpreter.Root.Key == "arc") {
                IArc arc = GetArc(expressionLoader, interpreter.Root);
                return new DrawableArc(arc);
            } else if (interpreter.Root.Key == "point") {
                IPoint point = GetPoint(expressionLoader, interpreter.Root);
                return new DrawablePoint(point);
            } else {
                return null;
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
                        initialAngle = currentExpressionLoader.GetFloatValue(node.Name);
                        break;
                    case "sweepAngle":
                        sweepAngle = currentExpressionLoader.GetFloatValue(node.Name);
                        break;
                    case "radius":
                        radius = currentExpressionLoader.GetFloatValue(node.Name);
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
                        x = currentExpressionLoader.GetFloatValue(node.Name);
                        break;
                    case "y":
                        y = currentExpressionLoader.GetFloatValue(node.Name);
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
