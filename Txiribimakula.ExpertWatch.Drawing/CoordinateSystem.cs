using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;
using Txiribimakula.ExpertWatch.Drawing.Contracts;

namespace Txiribimakula.ExpertWatch.Graphics
{
    public class CoordinateSystem : ICoordinateSystem
    {
        private float localToWorldFactor;
        private IBox box;

        private float scale;
        public float Scale {
            get { return scale; }
            set {
                scale = value;
                ReCalculate(WorldWidth, WorldHeight);
            }
        }

        public float LocalWidth { get; set; }
        public float LocalHeight { get; set; }
        public float WorldWidth { get; set; }
        public float WorldHeight { get; set; }
        public float LocalMinX { get; set; }
        public float LocalMaxX { get; set; }
        public float LocalMinY { get; set; }
        public float LocalMaxY { get; set; }

        private IPoint offset;
        public IPoint Offset {
            get { return offset; }
            set {
                offset.X += value.X;
                offset.Y += value.Y;
                ReCalculate(WorldWidth, WorldHeight);
            }
        }

        public CoordinateSystem(float worldWidth, float worldHeight, IBox box) {
            this.box = box;
            scale = 1.1f;
            offset = new Point(0, 0);

            ReCalculate(worldWidth, worldHeight);
        }

        public float ConvertLengthToLocal(float length) {
            return length / localToWorldFactor;
        }

        public float ConvertLengthToWorld(float length) {
            return length * localToWorldFactor;
        }

        public IPoint ConvertPointToWorld(IPoint point) {
            return new Point(ConvertXToWorld(point.X), ConvertYToWorld(point.Y));
        }

        public float ConvertXToWorld(float x) {
            return (x - LocalMinX) * localToWorldFactor;
        }

        public float ConvertYToWorld(float y) {
            return WorldHeight - (y - LocalMinY) * localToWorldFactor;
        }

        public float ConvertXToLocal(float x) {
            return (x / localToWorldFactor) + LocalMinX;
        }

        public float ConvertYToLocal(float y) {
            return LocalMinY - ((y - WorldHeight) / localToWorldFactor);
        }

        public void ReCalculate(float worldWidth, float worldHeight) {
            WorldHeight = worldHeight;
            WorldWidth = worldWidth;

            LocalHeight = box.MaxY - box.MinY;
            float scaledIncrementalY = ((LocalHeight * scale) - LocalHeight) / 2;
            LocalMinY = box.MinY - scaledIncrementalY;
            LocalMaxY = box.MaxY + scaledIncrementalY;


            LocalWidth = (WorldWidth * LocalHeight) / WorldHeight;
            float adjustedIncrementalX = (LocalWidth - (box.MaxX - box.MinX)) / 2;
            float scaledIncrementalX = ((LocalWidth * scale) - LocalWidth) / 2;
            LocalMinX = box.MinX - adjustedIncrementalX - scaledIncrementalX;
            LocalMaxX = box.MaxX + adjustedIncrementalX + scaledIncrementalX;


            LocalHeight *= scale;
            LocalWidth *= scale;
            localToWorldFactor = WorldHeight / LocalHeight; // it has to be tested if Y or X is the correct reference (the one that limits the geometries representation the most)
                                                            // optimise to not recalculate everything each time...?

            LocalMinY += ConvertLengthToLocal(Offset.Y);
            LocalMaxY += ConvertLengthToLocal(Offset.Y);
            LocalMinX -= ConvertLengthToLocal(Offset.X);
            LocalMaxX -= ConvertLengthToLocal(Offset.X);
        }
    }
}
