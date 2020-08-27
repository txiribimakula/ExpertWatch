using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Geometries
{
    public class Arc : IArc
    {
        public Arc(IPoint initialPoint, IPoint finalPoint, float radius) {
            InitialPoint = initialPoint;
            FinalPoint = finalPoint;
            Radius = radius;
        }

        public IPoint InitialPoint { get; set; }
        public IPoint FinalPoint { get; set; }
        public IPoint CenterPoint { get; set; }
        public float InitialAngle { get; set; }
        public float SweepAngle { get; set; }
        public float Radius { get; set; }
        public float Diameter => 2 * Radius;
        public IBox box { get; set; }
    }
}
