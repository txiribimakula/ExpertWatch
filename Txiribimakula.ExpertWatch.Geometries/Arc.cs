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
        }

        public IPoint InitialPoint { get; set; }
        public IPoint FinalPoint { get; set; }
        public IPoint CenterPoint { get; set; }
        public float InitialAngle { get; set; }
        public float SweepAngle { get; set; }
        public float Radius { get; set; }
        public float Diameter => 2 * Radius;
    }
}
