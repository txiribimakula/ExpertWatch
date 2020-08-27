namespace Txiribimakula.ExpertWatch.Geometries.Contracts
{
    public interface IArc: IGeometry
    {
        IPoint InitialPoint { get; set; }
        IPoint FinalPoint { get; set; }
        IPoint CenterPoint { get; set; }
        float InitialAngle { get; set; }
        float SweepAngle { get; set; }
        float Radius { get; set; }
        float Diameter { get; }
    }
}