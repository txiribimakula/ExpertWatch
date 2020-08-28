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

        private string path;
        public string Path {
            get { return path; }
            set { path = value; OnPropertyChanged(nameof(Path)); }
        }

        public void TransformGeometry(IDrawableVisitor visitor) {
            IArc transformedArc = visitor.GetTransformedArc(this);
            Path = "M" + transformedArc.InitialPoint.X.ToString(CultureInfo.InvariantCulture) + "," + transformedArc.InitialPoint.Y.ToString(CultureInfo.InvariantCulture) + " A" + transformedArc.Radius.ToString(CultureInfo.InvariantCulture) + "," + transformedArc.Radius.ToString(CultureInfo.InvariantCulture) + " 0 0 0 " + transformedArc.FinalPoint.X.ToString(CultureInfo.InvariantCulture) + "," + transformedArc.FinalPoint.Y.ToString(CultureInfo.InvariantCulture);
            TransformedGeometry = transformedArc;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
