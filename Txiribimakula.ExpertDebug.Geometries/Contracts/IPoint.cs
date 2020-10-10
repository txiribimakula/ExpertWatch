namespace Txiribimakula.ExpertWatch.Geometries.Contracts
{
    public interface IPoint : IGeometry
    {
        float X { get; set; }
        float Y { get; set; }
    }
}