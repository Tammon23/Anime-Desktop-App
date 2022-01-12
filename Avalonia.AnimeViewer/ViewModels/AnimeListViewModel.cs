using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyAnimeList.ResponseObjects.Anime;

namespace Avalonia.AnimeViewer.ViewModels
{
    public class AnimeListViewModel : ViewModelBase
    {
        public AnimeListViewModel(IEnumerable<Datum> items)
        {
            Items = new ObservableCollection<Datum>(items);
        }
        
        public ObservableCollection<Datum> Items { get; }
    }
}