using System.ComponentModel;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class Axis : IDrawable
    {
        public Axis(IBox box) {
            Color = Colors.Black;
            Box = box;
        }

        public IColor Color { get; set; }
        public IGeometry TransformedGeometry { get; set; }
        public IBox Box { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public bool IsHorizontal => Box.HorizontalLength > Box.VerticalLength;

        public void TransformGeometry(IDrawableVisitor visitor) {
            IPoint coordinatesOriginWorld = visitor.GetTransformedPoint(new Point(0, 0));
            if(IsHorizontal) {
                TransformedGeometry = new Segment(new Point(Box.MinX, coordinatesOriginWorld.Y), new Point(Box.MaxX, coordinatesOriginWorld.Y));
            } else {
                TransformedGeometry = new Segment(new Point(coordinatesOriginWorld.X, Box.MinY), new Point(coordinatesOriginWorld.X, Box.MaxY));
            }
        }
    }
}
