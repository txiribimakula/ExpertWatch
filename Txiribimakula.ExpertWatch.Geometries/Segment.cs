using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Geometries
{
    public class Segment : ISegment
    {
        public Segment(IPoint initialPoint, IPoint finalPoint) {
            InitialPoint = initialPoint;
            FinalPoint = finalPoint;

            SetBox();
        }

        public IPoint InitialPoint { get; set; }
        public IPoint FinalPoint { get; set; }
        public IBox Box { get; set; }

        // TODO: create this as extension method of IBox
        private void SetBox() {
            float minX, maxX, minY, maxY;
            minX = InitialPoint.X < FinalPoint.X ? InitialPoint.X : FinalPoint.X;
            maxX = InitialPoint.X > FinalPoint.X ? InitialPoint.X : FinalPoint.X;
            minY = InitialPoint.Y < FinalPoint.Y ? InitialPoint.Y : FinalPoint.Y;
            maxY = InitialPoint.Y > FinalPoint.Y ? InitialPoint.Y : FinalPoint.Y;

            Box = new Box(minX, maxX, minY, maxY);
        }
    }
}