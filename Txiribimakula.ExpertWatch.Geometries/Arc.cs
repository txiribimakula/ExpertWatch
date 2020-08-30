using System;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Geometries
{
    public class Arc : IArc
    {
        public Arc(IPoint centerPoint, float initialAngle, float sweepAngle, float radius) {
            CenterPoint = centerPoint;
            InitialAngle = initialAngle;
            SweepAngle = sweepAngle;
            Radius = radius;

            InitialPoint = new Point(
                centerPoint.X + (float)Math.Cos((initialAngle / 180) * Math.PI) * radius,
                centerPoint.Y - (float)Math.Sin((initialAngle / 180) * Math.PI) * radius);

            FinalPoint = new Point(
               centerPoint.X + (float)Math.Cos(((initialAngle + sweepAngle) / 180) * Math.PI) * radius,
               centerPoint.Y - (float)Math.Sin(((initialAngle + sweepAngle) / 180) * Math.PI) * radius);

            SetBox();
        }

        public IPoint InitialPoint { get; set; }
        public IPoint FinalPoint { get; set; }
        public IPoint CenterPoint { get; set; }
        public float InitialAngle { get; set; }
        public float SweepAngle { get; set; }
        public float Radius { get; set; }
        public float Diameter => 2 * Radius;
        public IBox Box { get; set; }

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
    }
}
