using System.ComponentModel;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public interface IDrawable : INotifyPropertyChanged
    {
        IColor Color { set; get; }
        IGeometry TransformedGeometry { set; get; }
        void TransformGeometry(IDrawableVisitor visitor);
    }
}