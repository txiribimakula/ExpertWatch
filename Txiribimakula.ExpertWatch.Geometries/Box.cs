namespace Txiribimakula.ExpertWatch.Geometries
{
    public class Box : IBox
    {
        private float minX;
        public float MinX {
            get => minX;
            set {
                if(value < minX) {
                    minX = value;
                }
            } 
        }
        private float minY;
        public float MinY { 
            get => minY;
            set {
                if(value < minY) {
                    minY = value;
                }
            }
        }
        private float maxX;
        public float MaxX {
            get => maxX;
            set {
                if (value > maxX) {
                    maxX = value;
                }
            }
        }
        private float maxY;
        public float MaxY {
            get => maxY;
            set {
                if (value > maxY) {
                    maxY = value;
                }
            }
        }
        public float HorizontalLength { get { return MaxX - MinX; } }
        public float VerticalLength { get { return MaxY - MinY; } }
        public bool IsValid {
            get {
                return HorizontalLength != 0 || VerticalLength != 0;
            }
        }

        public Box(float minX, float maxX, float minY, float maxY) {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

    }
}
