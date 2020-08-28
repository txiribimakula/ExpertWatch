using System;
using System.Globalization;
using System.Windows.Data;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch
{
    public class PointToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            IPoint point = (IPoint)value;

            return "M " + (point.X - 2).ToString(CultureInfo.InvariantCulture) + "," + point.Y.ToString(CultureInfo.InvariantCulture) +
                " A 2,2" +
                " 0 1 1 " +
                (point.X + 2).ToString(CultureInfo.InvariantCulture) + "," + point.Y.ToString(CultureInfo.InvariantCulture) +
                " A 2,2" +
                " 0 1 1 " +
                (point.X - 2).ToString(CultureInfo.InvariantCulture) + "," + point.Y.ToString(CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
