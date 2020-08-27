using System.Collections.ObjectModel;
using System.ComponentModel;
using Txiribimakula.ExpertWatch.DrawableGeometries;

namespace Txiribimakula.ExpertWatch.Loading
{
    public class WatchItem : INotifyPropertyChanged
    {
        public WatchItem() {
            Drawables = new ObservableCollection<IDrawable>();
        }

        private string name;
        public string Name {
            get { return name; }
            set { name = value; OnNameChanged(); }
        }

        private string description;
        public string Description {
            get { return description; }
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }

        private ObservableCollection<IDrawable> drawables;
        public ObservableCollection<IDrawable> Drawables {
            get { return drawables; }
            set { drawables = value; OnPropertyChanged(nameof(Drawables)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event WatchItemEventHandler NameChanged;
        private void OnNameChanged() {
            NameChanged?.Invoke(this);
        }
        public delegate void WatchItemEventHandler(WatchItem sender);
    }
}
