using System.ComponentModel;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class DrawableArc : Arc, IDrawableArc {
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

            if (initialAngleQuadrant == finalAngleQuadrant) {
                if (InitialPoint.X < FinalPoint.X) {
                    minX = InitialPoint.X;
                    maxX = FinalPoint.X;
                } else {
                    minX = FinalPoint.X;
                    maxX = InitialPoint.X;
                }
                if (InitialPoint.Y < FinalPoint.Y) {
                    minY = InitialPoint.Y;
                    maxY = FinalPoint.Y;
                } else {
                    minY = FinalPoint.Y;
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
