using System;
using System.ComponentModel;
using System.Globalization;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class DrawableArc : Arc, IDrawableArc {
        public DrawableArc(IPoint initialPoint, IPoint finalPoint, float radius)
            : base(initialPoint, finalPoint, radius) {
            Color = Colors.Black;
        }

        public DrawableArc(IPoint initialPoint, IPoint finalPoint, float radius, IColor color)
            : base(initialPoint, finalPoint, radius) {
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
