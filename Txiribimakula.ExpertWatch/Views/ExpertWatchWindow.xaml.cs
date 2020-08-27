using System.Windows.Controls;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.DrawableGeometries;

namespace Txiribimakula.ExpertWatch
{
    public partial class ExpertWatchWindow : UserControl
    {
        public ExpertWatchWindow() {
            Point point = new Point(0,0);
            IDrawable drawable = new DrawableSegment(point, point);
            InitializeComponent();
        }
    }
}
