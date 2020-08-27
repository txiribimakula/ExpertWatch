namespace Txiribimakula.ExpertWatch.Geometries.Contracts
{
    public interface ISegment : IGeometry
    {
        IPoint InitialPoint { get; set; }
        IPoint FinalPoint { get; set; }
    }
}