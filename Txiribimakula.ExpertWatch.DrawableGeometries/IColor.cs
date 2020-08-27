namespace Txiribimakula.ExpertWatch.DrawableGeometries
{
    public interface IColor
    {
        int Red { get; set; }
        int Green { get; set; }
        int Blue { get; set; }
        int Alpha { get; set; }

        public string Hex { get; }
    }
}