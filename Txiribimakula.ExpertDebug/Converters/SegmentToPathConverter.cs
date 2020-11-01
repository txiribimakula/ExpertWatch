using System;
using System.Globalization;
using System.Windows.Data;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch
{
    public class SegmentToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value != null) {
                ISegment segment = (ISegment)value;
                return ConvertSegmentToPath(segment);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }

        public static string ConvertSegmentToPath(ISegment segment) {
            return "M " + segment.InitialPoint.X.ToString(CultureInfo.InvariantCulture) + "," + segment.InitialPoint.Y.ToString(CultureInfo.InvariantCulture) +
                " L" + segment.FinalPoint.X.ToString(CultureInfo.InvariantCulture) + "," + segment.FinalPoint.Y.ToString(CultureInfo.InvariantCulture);
        }
    }
}
