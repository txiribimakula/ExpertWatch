using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Geometries
{
    public class Segment : ISegment
    {
        public Segment(IPoint initialPoint, IPoint finalPoint) {
            InitialPoint = initialPoint;
            FinalPoint = finalPoint;
        }

        public IPoint InitialPoint { get; set; }
        public IPoint FinalPoint { get; set; }
        public IBox box { get; set; }
    }
}