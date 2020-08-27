using EnvDTE;
using System;
using System.Collections.Generic;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Loading.CustomLoaders
{
    public class TRegistroGeoLtLoader : ILoader
    {
        public List<IGeometry> Load(Expression expression) {
            List<IGeometry> geometries = new List<IGeometry>();

            Expression curve2D = expression.DataMembers.Item("TCurve2D");
            IGeometry geometry = TCurve2DLoader.Get(curve2D);

            geometries.Add(geometry);

            return geometries;
        }

        public static IGeometry Get(Expression expression) {
            throw new NotImplementedException();
        }
    }
}

