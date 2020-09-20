using System;
using System.Globalization;
using System.Windows.Data;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch
{
    public class ArcToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            IArc arc = (IArc)value;
            string path = "M" + arc.InitialPoint.X.ToString(CultureInfo.InvariantCulture) + "," + arc.InitialPoint.Y.ToString(CultureInfo.InvariantCulture) +
                " A" + arc.Radius.ToString(CultureInfo.InvariantCulture) + "," + arc.Radius.ToString(CultureInfo.InvariantCulture) +
                " " + arc.SweepAngle.ToString(CultureInfo.InvariantCulture) +
                " " + (arc.SweepAngle >= 180 ? "1" : "0") +
                " " + (arc.SweepAngle > 0 ? "0" : "1") +
                " " + arc.FinalPoint.X.ToString(CultureInfo.InvariantCulture) + "," + arc.FinalPoint.Y.ToString(CultureInfo.InvariantCulture);
            return path;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
