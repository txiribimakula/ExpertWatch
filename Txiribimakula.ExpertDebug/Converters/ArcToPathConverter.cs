using System;
using System.Globalization;
using System.Windows.Data;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch
{
    public class ArcToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            IArc arc = (IArc)value;

            if (arc.SweepAngle == 360) {
                IArc arc1 = new Arc(arc.CenterPoint, 0, 180, arc.Radius);
                IArc arc2 = new Arc(arc.CenterPoint, 180, 180, arc.Radius);
                return ConvertArcToPath(arc1) + " " + ConvertArcToPath(arc2);
            } else {
                return ConvertArcToPath(arc);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }

        private string ConvertArcToPath(IArc arc) {
            return "M" + arc.InitialPoint.X.ToString(CultureInfo.InvariantCulture) + "," + arc.InitialPoint.Y.ToString(CultureInfo.InvariantCulture) +
                " A" + arc.Radius.ToString(CultureInfo.InvariantCulture) + "," + arc.Radius.ToString(CultureInfo.InvariantCulture) +
                " " + arc.SweepAngle.ToString(CultureInfo.InvariantCulture) +
                " " + (arc.SweepAngle >= 180 ? "1" : "0") +
                " " + (arc.SweepAngle > 0 ? "0" : "1") +
                " " + arc.FinalPoint.X.ToString(CultureInfo.InvariantCulture) + "," + arc.FinalPoint.Y.ToString(CultureInfo.InvariantCulture);
        }
    }
}
