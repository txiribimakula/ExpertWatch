using System;
using System.Globalization;
using System.Windows.Data;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertDebug.Converters
{
    class PointToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value != null) {
                IPoint point = (IPoint)value;

                return point.X.ToString("0.00", CultureInfo.InvariantCulture) + ", " + point.Y.ToString("0.00", CultureInfo.InvariantCulture);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
