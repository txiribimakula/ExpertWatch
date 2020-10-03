using System;
using System.Globalization;
using System.Windows.Data;
using Txiribimakula.ExpertWatch;
using Txiribimakula.ExpertWatch.Drawing;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertDebug.Converters
{
    public class AxesToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            (Axis, Axis) axes = ((Axis, Axis))value;

            if(axes.Item1 != null && axes.Item2 != null) {
                return SegmentToPathConverter.ConvertSegmentToPath((ISegment)axes.Item1.TransformedGeometry) + " " +
                    SegmentToPathConverter.ConvertSegmentToPath((ISegment)axes.Item2.TransformedGeometry);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
