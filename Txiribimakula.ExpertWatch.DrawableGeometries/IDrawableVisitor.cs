using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.DrawableGeometries
{
    public interface IDrawableVisitor
    {
        ISegment GetTransformedSegment(ISegment segment);
        IArc GetTransformedArc(IArc arc);
        IPoint GetTransformedPoint(IPoint point);
    }
}
