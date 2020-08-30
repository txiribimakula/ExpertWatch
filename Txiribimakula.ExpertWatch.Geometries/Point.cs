using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Geometries
{
    public class Point : IPoint
    {
        public float X { get; set; }
        public float Y { get; set; }
        public IBox Box { get; set; }

        public Point(float x, float y) {
            X = x;
            Y = y;
        }
    }
}
