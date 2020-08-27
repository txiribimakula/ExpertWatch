using Txiribimakula.ExpertWatch.Loading;

namespace Txiribimakula.ExpertWatch.Models
{
    public class InterpreterSelector : IInterpreterSelector
    {
        public IInterpreter Get(string type) {
            switch (type) {
                case "Txiribimakula.ExpertWatch.Geometries.Segment":
                    return new SegmentInterpreter();
                case "Txiribimakula.ExpertWatch.Geometries.Arc":
                    return new ArcInterpreter();
                case "Txiribimakula.ExpertWatch.Geometries.Point":
                    return new PointInterpreter();
                default:
                    return null;
            }
        }
    }
}
