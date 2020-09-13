using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Txiribimakula.ExpertWatch.Geometries;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class DrawableCollection<IDrawable> : Collection<IDrawable>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public DrawableCollection(IBox box): base() {
            Box = box;
        }

        public IBox Box { get; set; }
        public int TotalCount { get; set; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
