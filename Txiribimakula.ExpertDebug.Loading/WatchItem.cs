using System.ComponentModel;
using Txiribimakula.ExpertWatch.Drawing;

namespace Txiribimakula.ExpertWatch.Loading
{
    public class WatchItem : INotifyPropertyChanged
    {
        public WatchItem() {
            Drawables = new DrawableCollection();
            isLoading = true;
            isVisible = true;
        }

        private bool isVisible;
        public bool IsVisible {
            get { return isVisible; }
            set { isVisible = value; OnPropertyChanged(nameof(IsVisible)); }
        }

        private bool isLoading;
        public bool IsLoading {
            get { return isLoading; }
            set { 
                isLoading = value;
                if(isLoading) {
                    IsLoadingActivated?.Invoke(this);
                } else {
                    IsLoadingCancelled?.Invoke(this);
                }
            }
        }
        public event WatchItemEventHandler IsLoadingActivated;
        public event WatchItemEventHandler IsLoadingCancelled;

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

        private DrawableCollection drawables;
        public DrawableCollection Drawables {
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
