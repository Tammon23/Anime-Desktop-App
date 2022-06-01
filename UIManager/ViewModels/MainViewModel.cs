using System.Collections;
using UIManager.Models;

namespace UIManager.ViewModels;

public class MainViewModel : ViewModelBase
{
    private static IList _items = new ArrayList();
    /*
    private static ViewModelBase _statusBarView = new StatusBarViewModel();
    */

    public MainViewModel()
    {
        /*
        _statusBarView = new StatusBarViewModel();
        */
        _items.Add(new CustomTabItem {Header = "My Anime List",       Content = new AnimeListPageViewModel()});
        _items.Add(new CustomTabItem {Header = "Seasonal Anime List", Content = new SeasonalAnimePageViewModel()});
        _items.Add(new CustomTabItem {Header = "Watch",               Content = MediaPageViewModel.self});
        /*
        _items.Add(new CustomTabItem {Header = "Anime List Page",     Content = new AnimeListPageViewModel()});
    */
    }

    /*
    public ViewModelBase StatusBarView => _statusBarView;
    */

    public IList Items
    {
        get => _items;
    }
}