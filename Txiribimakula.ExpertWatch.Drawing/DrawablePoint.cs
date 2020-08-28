using System.ComponentModel;
using System.Globalization;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class DrawablePoint : Point, IDrawablePoint
    {
        public DrawablePoint(float x, float y) : base(x, y){
            Color = Colors.Black;
        }
        public DrawablePoint(float x, float y, IColor color) : base(x, y) {
            Color = color;
        }

        public IColor Color { get; set; }
        private IGeometry transformedGeometry;
        public IGeometry TransformedGeometry {
            get { return transformedGeometry; }
            set { transformedGeometry = value; OnPropertyChanged(nameof(TransformedGeometry)); }
        }

        public void TransformGeometry(IDrawableVisitor visitor) {
            IPoint transformedPoint = visitor.GetTransformedPoint(this);
            TransformedGeometry = visitor.GetTransformedPoint(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
