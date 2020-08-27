using EnvDTE;
using System.Collections.Generic;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Loading
{
    public interface ILoader
    {
        List<IGeometry> Load(Expression expression);
    }
}
