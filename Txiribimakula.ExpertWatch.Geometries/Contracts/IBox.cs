namespace Txiribimakula.ExpertWatch.Geometries
{
    public interface IBox
    {
        float MinX { get; set; }
        float MinY { get; set; }
        float MaxX { get; set; }
        float MaxY { get; set; }
        float HorizontalLength { get; }
        float VerticalLength { get; }
        bool IsValid { get; }

        void Expand(IBox box);
    }
}
