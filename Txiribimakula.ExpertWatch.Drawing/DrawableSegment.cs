using System.ComponentModel;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class DrawableSegment : Segment, IDrawableSegment
    {
        public DrawableSegment(IPoint initialPoint, IPoint finalPoint) : base(initialPoint, finalPoint) {
            Color = Colors.Black;
            SetBox();
        }
        public DrawableSegment(IPoint initialPoint, IPoint finalPoint, IColor color) : base(initialPoint, finalPoint) {
            Color = color;
            SetBox();
        }

        public IColor Color { set; get; }
        public IBox Box { get; set; }
        private IGeometry transformedGeometry;
        public IGeometry TransformedGeometry {
            get { return transformedGeometry; }
            set { transformedGeometry = value; OnPropertyChanged(nameof(TransformedGeometry)); }
        }

        public void TransformGeometry(IDrawableVisitor visitor) {
            TransformedGeometry = visitor.GetTransformedSegment(this);
        }

        // TODO: create this as extension method of IBox (SetSegmentBox)
        private void SetBox() {
            float minX, maxX, minY, maxY;
            minX = InitialPoint.X < FinalPoint.X ? InitialPoint.X : FinalPoint.X;
            maxX = InitialPoint.X > FinalPoint.X ? InitialPoint.X : FinalPoint.X;
            minY = InitialPoint.Y < FinalPoint.Y ? InitialPoint.Y : FinalPoint.Y;
            maxY = InitialPoint.Y > FinalPoint.Y ? InitialPoint.Y : FinalPoint.Y;

            Box = new Box(minX, maxX, minY, maxY);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
