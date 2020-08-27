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
            set { transformedGeometry = value; }
        }

        private string path;
        public string Path {
            get { return path; }
            set { path = value; OnPropertyChanged(nameof(Path)); }
        }

        public void TransformGeometry(IDrawableVisitor visitor) {
            IPoint transformedPoint = visitor.GetTransformedPoint(this);
            Path = "M " + (transformedPoint.X - 2).ToString(CultureInfo.InvariantCulture) + "," + transformedPoint.Y.ToString(CultureInfo.InvariantCulture) + 
                " A 2,2" +
                " 0 1 1 " +
                (transformedPoint.X + 2).ToString(CultureInfo.InvariantCulture) + "," + transformedPoint.Y.ToString(CultureInfo.InvariantCulture) +
                " A 2,2" +
                " 0 1 1 " +
                (transformedPoint.X - 2).ToString(CultureInfo.InvariantCulture) + "," + transformedPoint.Y.ToString(CultureInfo.InvariantCulture);
            TransformedGeometry = transformedPoint;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
