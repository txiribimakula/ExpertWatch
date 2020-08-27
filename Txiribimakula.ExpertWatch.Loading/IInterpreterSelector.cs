namespace Txiribimakula.ExpertWatch.Loading
{
    public interface IInterpreterSelector
    {
        IInterpreter Get(string type);
    }
}
