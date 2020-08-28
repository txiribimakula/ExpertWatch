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

            return "M" + arc.InitialPoint.X.ToString(CultureInfo.InvariantCulture) + "," + arc.InitialPoint.Y.ToString(CultureInfo.InvariantCulture) +
                " A" + arc.Radius.ToString(CultureInfo.InvariantCulture) + "," + arc.Radius.ToString(CultureInfo.InvariantCulture) +
                " 0 0 0 " + arc.FinalPoint.X.ToString(CultureInfo.InvariantCulture) + "," + arc.FinalPoint.Y.ToString(CultureInfo.InvariantCulture);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
