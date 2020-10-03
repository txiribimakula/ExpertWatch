using EnvDTE;
using System.Collections.ObjectModel;
using System.Windows;
using Txiribimakula.ExpertWatch.Drawing;
using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;
using Txiribimakula.ExpertWatch.Graphics;
using Txiribimakula.ExpertWatch.Drawing.Contracts;
using Txiribimakula.ExpertWatch.Loading;
using Txiribimakula.ExpertWatch.Models;
using System.ComponentModel;
using Txiribimakula.ExpertWatch.Loading.Exceptions;
using System;

namespace Txiribimakula.ExpertWatch.ViewModels
{
    public class ViewModel : INotifyPropertyChanged {
        public ViewModel(Debugger debugger) {
            WatchItems = new ObservableCollection<WatchItem>();
            WatchItems.CollectionChanged += OnWatchItemsCollectionChanged;

            IInterpreter interpreter = new Interpreter();
            loader = new Loader(debugger, interpreter);
        }

        public void OnToolsOptionsBlueprintsChanged(string blueprints) {
            loader.Interpreter = new Interpreter(blueprints);
        }

        public void OnEnterBreakMode(dbgEventReason reason, ref dbgExecutionAction executionAction) {
            foreach (var watchItem in WatchItems) {
                OnWatchItemLoading(watchItem);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private Loader loader;
        private GeometryDrawer geoDrawer;
        public ObservableCollection<WatchItem> WatchItems { get; set; }

        public Axis AxisX { get; set; }
        public Axis AxisY { get; set; }

        private IPoint currentCursorPoint;
        public IPoint CurrentCursorPoint {
            get { return currentCursorPoint; }
            set { currentCursorPoint = value; OnPropertyChanged("CurrentCursorPoint"); }
        }

        private bool isMiddleMouseDown;
        private IPoint lastClickPoint;

        public RelayCommand AutoFitCommand { get; set; }

        public void OnLoaded(object sender, RoutedEventArgs e) {
            FrameworkElement frameworkElement = (FrameworkElement)sender;

            ICoordinateSystem coordinateSystem = new CoordinateSystem((float)frameworkElement.ActualWidth, (float)frameworkElement.ActualHeight, new Box(-10, 10, -10, 10));

            AxisX = new Axis(new Box(0, (float)frameworkElement.ActualWidth, 0, 0));
            AxisY = new Axis(new Box(0, 0, 0, (float)frameworkElement.ActualHeight));

            DrawableVisitor visitor = new DrawableVisitor(coordinateSystem);
            geoDrawer = new GeometryDrawer(visitor);

            geoDrawer.TransformGeometry(AxisX);
            geoDrawer.TransformGeometry(AxisY);
            OnPropertyChanged(nameof(AxisX));
            OnPropertyChanged(nameof(AxisY));

            AutoFitCommand = new RelayCommand(parameter => AutoFit((float)frameworkElement.ActualWidth / (float)frameworkElement.ActualHeight));
        }

        private void AutoFit(float windowRatio) {
            IBox box = null;
            foreach (var watchItem in WatchItems) {
                if (box == null && watchItem.IsVisible) {
                    box = (IBox)watchItem.Drawables.Box.Clone();
                } else if (watchItem.IsVisible) {
                    box.Expand(watchItem.Drawables.Box);
                }
            }

            float drawablesRatio = box.HorizontalLength / box.VerticalLength;
            if (drawablesRatio > windowRatio) {
                float verticalIncrement = (box.VerticalLength * (drawablesRatio / windowRatio)) - box.VerticalLength;
                box.MaxY += verticalIncrement / 2;
                box.MinY -= verticalIncrement / 2;
            }

            ICoordinateSystem coordinateSystem = new CoordinateSystem(geoDrawer.DrawableVisitor.CoordinateSystem.WorldWidth, geoDrawer.DrawableVisitor.CoordinateSystem.WorldHeight, box);
            geoDrawer.DrawableVisitor.CoordinateSystem = coordinateSystem;

            foreach (var watchItem in WatchItems) {
                geoDrawer.TransformGeometries(watchItem.Drawables);
            }
            geoDrawer.TransformGeometry(AxisX);
            geoDrawer.TransformGeometry(AxisY);
            OnPropertyChanged(nameof(AxisX));
            OnPropertyChanged(nameof(AxisY));
        }

        public void OnSizeChanged(object sender, SizeChangedEventArgs args) {
            geoDrawer.DrawableVisitor.CoordinateSystem.ReCalculate((float)args.NewSize.Width, (float)args.NewSize.Height);
            foreach (var watchItem in WatchItems) {
                geoDrawer.TransformGeometries(watchItem.Drawables);
            }
            geoDrawer.TransformGeometry(AxisX);
            geoDrawer.TransformGeometry(AxisY);
            OnPropertyChanged(nameof(AxisX));
            OnPropertyChanged(nameof(AxisY));
        }

        public void OnMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            IInputElement senderElement = (IInputElement)sender;
            if (e.MiddleButton == System.Windows.Input.MouseButtonState.Pressed) {
                isMiddleMouseDown = true;
                System.Windows.Point point = e.GetPosition(senderElement);
                lastClickPoint = new Geometries.Point((float)point.X, (float)point.Y);
            }
            senderElement.CaptureMouse();
        }

        public void OnMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            IInputElement senderElement = (IInputElement)sender;
            isMiddleMouseDown = false;
            senderElement.ReleaseMouseCapture();
        }

        public void OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            CurrentCursorPoint = null;
        }

        public void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs e) {
            IInputElement senderElement = (IInputElement)sender;
            System.Windows.Point canvasClickPoint = e.GetPosition(senderElement);
            IPoint currentCursorPoint = new Geometries.Point((float)canvasClickPoint.X, (float)canvasClickPoint.Y);
            CurrentCursorPoint = geoDrawer.DrawableVisitor.CoordinateSystem.ConvertPointToLocal(currentCursorPoint);
            if (isMiddleMouseDown) {
                float incrementalX = currentCursorPoint.X - lastClickPoint.X;
                float incrementalY = currentCursorPoint.Y - lastClickPoint.Y;
                geoDrawer.DrawableVisitor.CoordinateSystem.Offset = new Geometries.Point(incrementalX, incrementalY);
                foreach (var watchItem in WatchItems) {
                    geoDrawer.TransformGeometries(watchItem.Drawables);
                }
                geoDrawer.TransformGeometry(AxisX);
                geoDrawer.TransformGeometry(AxisY);
                OnPropertyChanged(nameof(AxisX));
                OnPropertyChanged(nameof(AxisY));
                lastClickPoint = currentCursorPoint;
            }
        }

        private void OnWatchItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (e.NewItems != null) {
                foreach (WatchItem item in e.NewItems) {
                    if(item != null) {
                        item.NameChanged += OnWatchItemNameChanged;
                        item.IsLoadingActivated += OnWatchItemLoading;
                    }
                }
            }
            if (e.OldItems != null) {
                foreach (WatchItem item in e.OldItems) {
                    if (item != null) {
                        item.NameChanged -= OnWatchItemNameChanged;
                        item.IsLoadingActivated -= OnWatchItemLoading;
                    }
                }
            }
        }

        public void OnMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e) {
            IInputElement senderElement = (IInputElement)sender;
            System.Windows.Point cursorWorldPoint = e.GetPosition(senderElement);
            IPoint localPoint = new Geometries.Point(geoDrawer.DrawableVisitor.CoordinateSystem.ConvertXToLocal((float)cursorWorldPoint.X), geoDrawer.DrawableVisitor.CoordinateSystem.ConvertYToLocal((float)cursorWorldPoint.Y));
            if (e.Delta < 0) {
                geoDrawer.DrawableVisitor.CoordinateSystem.Scale *= 1.1f;
            } else {
                geoDrawer.DrawableVisitor.CoordinateSystem.Scale /= 1.1f;
            }
            float newWorldPointX = geoDrawer.DrawableVisitor.CoordinateSystem.ConvertXToWorld(localPoint.X);
            float newWorldPointY = geoDrawer.DrawableVisitor.CoordinateSystem.ConvertYToWorld(localPoint.Y);

            geoDrawer.DrawableVisitor.CoordinateSystem.Offset = new Geometries.Point((float)cursorWorldPoint.X - newWorldPointX, (float)cursorWorldPoint.Y - newWorldPointY);

            foreach (var watchItem in WatchItems) {
                geoDrawer.TransformGeometries(watchItem.Drawables);
            }
            geoDrawer.TransformGeometry(AxisX);
            geoDrawer.TransformGeometry(AxisY);
            OnPropertyChanged(nameof(AxisX));
            OnPropertyChanged(nameof(AxisY));
        }

        private void OnWatchItemNameChanged(WatchItem sender) {
            OnWatchItemLoading(sender);
        }

        private void OnWatchItemLoading(WatchItem watchItem) {
            if (watchItem.IsLoading) {
                watchItem.Drawables.ResetAndNotify();
                BackgroundWorker backgroundWorker = new BackgroundWorker();
                backgroundWorker.WorkerSupportsCancellation = true;
                watchItem.IsLoadingCancelled += (sender) => {
                    backgroundWorker.CancelAsync();
                };
                backgroundWorker.ProgressChanged += (sender, arguments) => {
                    IDrawable drawable = (IDrawable)arguments.UserState;
                    geoDrawer.TransformGeometry(drawable);
                    watchItem.Drawables.AddAndNotify(drawable);
                    watchItem.Drawables.Progress = arguments.ProgressPercentage;
                };
                backgroundWorker.RunWorkerCompleted += (sender, arguments) => {
                };
                try {
                    loader.Load(watchItem, backgroundWorker);
                } catch(LoadingException ex) {
                    watchItem.Drawables.Error = ex.Message;
                }
            }
        }
    }
}
