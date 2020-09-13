using System;
using System.Globalization;
using System.Windows.Data;
using Txiribimakula.ExpertWatch.Drawing;
using Txiribimakula.ExpertWatch.Geometries.Contracts;

namespace Txiribimakula.ExpertWatch.Converters
{
    public class DrawablesToProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            DrawableCollection<IDrawable> drawables = (DrawableCollection<IDrawable>)value;

            return ((drawables.Count * 1.0) / (drawables.TotalCount * 1.0)) * 100.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
