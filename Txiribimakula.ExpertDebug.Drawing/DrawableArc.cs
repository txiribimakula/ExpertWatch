using System.Collections.Generic;
using System.ComponentModel;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class DrawableArc : Arc, IDrawableArc {
        public DrawableArc(IArc arc) : base(arc.CenterPoint, arc.InitialAngle, arc.SweepAngle, arc.Radius) {
            Color = Colors.Black;
            SetBox();
        }
        public DrawableArc(IPoint centerPoint, float initialAngle, float sweepAngle, float radius)
            : base(centerPoint, initialAngle, sweepAngle, radius) {
            Color = Colors.Black;
            SetBox();
        }
        public DrawableArc(IPoint centerPoint, float initialAngle, float sweepAngle, float radius, IColor color)
            : base(centerPoint, initialAngle, sweepAngle, radius) {
            Color = color;
            SetBox();
        }

        public IColor Color { get; set; }
        public IBox Box { get; set; }

        private IGeometry transformedGeometry;
        public IGeometry TransformedGeometry {
            get { return transformedGeometry; }
            set { transformedGeometry = value; OnPropertyChanged(nameof(TransformedGeometry)); }
        }

        private void SetBox() {
            float minX = 0.0f, maxX = 0.0f, minY = 0.0f, maxY = 0.0f;

            var initialAngleQuadrant = GetQuadrant(InitialAngle);
            var finalAngleQuadrant = GetQuadrant(InitialAngle + SweepAngle);

            List<string> crossedSemiAxes = new List<string>();

            int currentQuadrant = initialAngleQuadrant;
            while (currentQuadrant != finalAngleQuadrant) {
                if (SweepAngle > 0) {
                    currentQuadrant++;
                    if (currentQuadrant == 4) {
                        currentQuadrant = 0;
                    }
                    switch (currentQuadrant) {
                        case 0:
                            crossedSemiAxes.Add("xPositive");
                            break;
                        case 1:
                            crossedSemiAxes.Add("yPositive");
                            break;
                        case 2:
                            crossedSemiAxes.Add("xNegative");
                            break;
                        case 3:
                            crossedSemiAxes.Add("yNegative");
                            break;
                    }
                } else {
                    currentQuadrant--;
                    if (currentQuadrant == -1) {
                        currentQuadrant = 3;
                    }
                    switch (currentQuadrant) {
                        case 0:
                            crossedSemiAxes.Add("yPositive");
                            break;
                        case 1:
                            crossedSemiAxes.Add("xNegative");
                            break;
                        case 2:
                            crossedSemiAxes.Add("yNegative");
                            break;
                        case 3:
                            crossedSemiAxes.Add("xPositive");
                            break;
                    }
                }
            }
            if (crossedSemiAxes.Contains("xNegative")) {
                minX = CenterPoint.X - Radius;
            } else {
                if (InitialPoint.X < FinalPoint.X) {
                    minX = InitialPoint.X;
                } else {
                    minX = FinalPoint.X;
                }
            }
            if (crossedSemiAxes.Contains("xPositive")) {
                maxX = CenterPoint.X + Radius;
            } else {
                if (InitialPoint.X < FinalPoint.X) {
                    maxX = FinalPoint.X;
                } else {
                    maxX = InitialPoint.X;
                }
            }
            if (crossedSemiAxes.Contains("yNegative")) {
                minY = CenterPoint.Y - Radius;
            } else {
                if (InitialPoint.Y < FinalPoint.Y) {
                    minY = InitialPoint.Y;
                } else {
                    minY = FinalPoint.Y;
                }
            }
            if (crossedSemiAxes.Contains("yPositive")) {
                maxY = CenterPoint.Y + Radius;
            } else {
                if (InitialPoint.Y < FinalPoint.Y) {
                    maxY = FinalPoint.Y;
                } else {
                    maxY = InitialPoint.Y;
                }
            }

            Box = new Box(minX, maxX, minY, maxY);
        }
        private int GetQuadrant(float angle) {
            if (angle < 0) {
                int rounds = (int)(angle / 360.0f) + 1;
                angle += (-angle) + 360 * rounds;
            }

            float trueAngle = angle % (360.0f);

            if (trueAngle >= 0.0 && trueAngle < 90.0f)
                return 0;
            if (trueAngle >= 90.0f && trueAngle < 180.0f)
                return 1;
            if (trueAngle >= 180.0f && trueAngle < 270.0f)
                return 2;
            if (trueAngle >= 270.0f && trueAngle < 360.0f)
                return 3;

            return -1;
        }

        public void TransformGeometry(IDrawableVisitor visitor) {
            TransformedGeometry = visitor.GetTransformedArc(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
