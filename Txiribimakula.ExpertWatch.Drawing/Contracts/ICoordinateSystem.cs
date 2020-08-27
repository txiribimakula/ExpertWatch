
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing.Contracts
{
    public interface ICoordinateSystem
    {
        float Scale { get; set; }
        IPoint Offset { get; set; }

        float LocalWidth { get; set; }
        float LocalHeight { get; set; }
        float WorldWidth { get; set; }
        float WorldHeight { get; set; }
        float LocalMinX { get; set; }
        float LocalMaxX { get; set; }
        float LocalMinY { get; set; }
        float LocalMaxY { get; set; }

        IPoint ConvertPointToWorld(IPoint point);
        float ConvertXToWorld(float x);
        float ConvertYToWorld(float y);
        float ConvertLengthToWorld(float length);
        float ConvertXToLocal(float x);
        float ConvertYToLocal(float y);
        float ConvertLengthToLocal(float length);
    }
}
