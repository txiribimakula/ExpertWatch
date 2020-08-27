using System.ComponentModel;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.DrawableGeometries
{
    public class DrawableSegment : Segment, IDrawableSegment
    {
        public DrawableSegment(IPoint initialPoint, IPoint finalPoint) : base(initialPoint, finalPoint) {
            Color = Colors.Black;
        }
        public DrawableSegment(IPoint initialPoint, IPoint finalPoint, IColor color) : base(initialPoint, finalPoint) {
            Color = color;
        }

        public IColor Color { set; get; }

        private IGeometry transformedGeometry;
        public IGeometry TransformedGeometry {
            get { return transformedGeometry; }
            set { transformedGeometry = value; OnPropertyChanged(nameof(TransformedGeometry)); }
        }

        public void TransformGeometry(IDrawableVisitor visitor) {
            TransformedGeometry = visitor.GetTransformedSegment(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
