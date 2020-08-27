using System.Windows.Controls;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.DrawableGeometries;

namespace Txiribimakula.ExpertWatch
{
    public partial class ExpertWatchWindow : UserControl
    {
        //Drawing.Engine DrawingEngine { get; set; }

        //Loader Loader { get; set; }

        //ObservableCollection<ExpertItem> ExpertItems { get; set; }

        //public Debugger Debugger { get; set; }


        //private bool isMiddleMouseDown;
        //private IPoint lastClickPoint;

        public ExpertWatchWindow() {
            Point point = new Point(0,0);
            IDrawable drawable = new DrawableSegment(point, point);
            InitializeComponent();

            //DTE2 DTE2 = ExpertWatchCommand.Instance.ServiceProvider.GetService(typeof(DTE)) as DTE2;
            //Debugger = DTE2.Debugger;
            //IInterpreterSelector interpreterSelector = new InterpreterSelector();
            //Loader = new Loader(Debugger, interpreterSelector);

            //ExpertItems = new ObservableCollection<ExpertItem>();
            //ExpertItems.CollectionChanged += ExpertItems_CollectionChanged;
            //dataGrid.ItemsSource = ExpertItems;
        }

        //private void ExpertItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
        //    if (e.NewItems != null) {
        //        foreach (ExpertItem item in e.NewItems) {
        //            item.PropertyChanged += ExpertItem_PropertyChanged;
        //        }
        //    }

        //    if (e.OldItems != null) {
        //        foreach (ExpertItem item in e.OldItems) {
        //            item.PropertyChanged -= ExpertItem_PropertyChanged;
        //        }
        //    }
        //}

        //private void ExpertItem_PropertyChanged(ExpertItem sender, ExpertItemArgs e) {
        //    ExpertItem expertItem = Loader.Load(e.OldValue, sender.Name);


        //    if(expertItem != null) {
        //        // TODO: Manage with ExpertItems being a Custom Dictionary.
        //        for (int i = 0; i < ExpertItems.Count; i++) {
        //            if (ExpertItems[i].Name == sender.Name) {
        //                ExpertItems[i].Drawable = expertItem.Drawable;
        //                ExpertItems[i].Description = expertItem.Description;
        //            }
        //        }
        //        // In the custom Dictionary method to obtain all drawables.
        //        List<IDrawable<IGeometryDrawing>> drawables = new List<IDrawable<IGeometryDrawing>>();
        //        foreach (var item in ExpertItems) {
        //            drawables.Add(item.Drawable);
        //        }
        //        /////
        //        DrawingEngine.DrawGeometries(drawables);
        //    }
        //}

        //private void Canvas_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e) {
        //    // TODO: control singletons where needed
        //    //ICoordinateSystem coordinateSystem = new CoordinateSystem((float)geometryCanvas.ActualWidth, (float)geometryCanvas.ActualHeight, new Box(-10, 10, -10, 10));

        //    //CursorEngine cursorDrawer = new CursorEngine(auxiliarCanvas);
        //    //AxesDrawer axesDrawer = new AxesDrawer(axesCanvas);
        //    //ScalesDrawer scalesDrawer = new ScalesDrawer(scalesCanvas);
        //    //GeometryDrawer geometryDrawer = new GeometryDrawer(geometryCanvas, coordinateSystem);

        //    //DrawingEngine = new Drawing.Engine(coordinateSystem, cursorDrawer, axesDrawer, scalesDrawer, geometryDrawer);

        //    //DrawingEngine.DrawAxes(); // TODO: control the redraw on the coordinatesystem's setter
        //    //DrawingEngine.DrawScales(); // TODO: control the redraw on the coordinatesystem's setter


        //    //DrawingEngine.DrawGeometries(Loader.Drawables.Values.ToList<IDrawable<IGeometryDrawing>>());
        //}

        //private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e) {
        //    System.Windows.Point canvasClickPoint = e.GetPosition(auxiliarCanvas);
        //    IPoint clickPoint = new Point((float)canvasClickPoint.X, (float)canvasClickPoint.Y);
        //    DrawingEngine.DrawCursor(clickPoint);
        //    if (isMiddleMouseDown) {
        //        DrawingEngine.Pan(lastClickPoint, clickPoint);
        //        lastClickPoint = clickPoint;
        //    }
        //}

        //private void Canvas_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e) {
        //    System.Windows.Point worldPoint = e.GetPosition(auxiliarCanvas);

        //    DrawingEngine.Zoom(e.Delta, new Point((float)worldPoint.X, (float)worldPoint.Y));
        //}

        //private void Canvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
        //    DrawingEngine.ClearCursor();
        //}

        //private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        //    if (e.MiddleButton == System.Windows.Input.MouseButtonState.Pressed) {
        //        isMiddleMouseDown = true;
        //        System.Windows.Point point = e.GetPosition(auxiliarCanvas);
        //        lastClickPoint = new Point((float)point.X, (float)point.Y);
        //    }
        //    auxiliarCanvas.CaptureMouse();
        //}

        //private void Canvas_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        //    isMiddleMouseDown = false;
        //    auxiliarCanvas.ReleaseMouseCapture();
        //}
    }
}
