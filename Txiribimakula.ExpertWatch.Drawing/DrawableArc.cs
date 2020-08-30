using System.ComponentModel;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class DrawableArc : Arc, IDrawableArc {
        public DrawableArc(IPoint centerPoint, float initialAngle, float sweepAngle, float radius)
            : base(centerPoint, initialAngle, sweepAngle, radius) {
            Color = Colors.Black;
        }

        public DrawableArc(IPoint centerPoint, float initialAngle, float sweepAngle, float radius, IColor color)
            : base(centerPoint, initialAngle, sweepAngle, radius) {
            Color = color;
        }

        public IColor Color { get; set; }

        private IGeometry transformedGeometry;
        public IGeometry TransformedGeometry {
            get { return transformedGeometry; }
            set { transformedGeometry = value; OnPropertyChanged(nameof(TransformedGeometry)); }
        }

        public void TransformGeometry(IDrawableVisitor visitor) {
            TransformedGeometry = visitor.GetTransformedArc(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
