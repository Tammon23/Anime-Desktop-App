using System;
using System.Reactive;
using System.Reactive.Linq;
using GUI.Interfaces;
using ReactiveUI;

namespace GUI.ViewModels;

public class MainWindowViewModel : ViewModelBase, IAdvancedScreen
{
    private string _searchBoxText = "";

    public MainWindowViewModel()
    {
        Router.Navigate.Execute(new HomePageViewModel(this));
        
        // to use the search bar in multiple view models we create an observable that 
        // watches when the value bound to the text property is changed this being an IObservable<string>
        // we can then pass this observable where needed and bound it using the "^" in the axaml
        SearchBoxTextObservable = this
            .WhenAnyValue(vm => vm.SearchBoxText)
            .Select(value => value);

        ExecuteSearch = ReactiveCommand.Create(() => { });

        GoHomePage = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new HomePageViewModel(this))
        );

        GoSearchPage = ReactiveCommand.CreateFromObservable(
            () =>
            {
                var model = SearchPageViewModel.Instance;
                model.HostScreen = this;
                model.ButtonIsExecuting = ExecuteSearch.IsExecuting;
                model.AnimeSearchText = SearchBoxTextObservable;
                return Router.Navigate.Execute(SearchPageViewModel.Instance);
            });

        GoRecommendationsPage = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new RecommendationsPageViewModel(this))
        );

        GoSeasonalAnimePage = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new SeasonalAnimePageViewModel(this))
        );

        GoTimetablePage = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new TimetablePageViewModel(this))
        );

        GoWatchPage = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new WatchPageViewModel(this))
        );

        GoProfilePage = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new ProfilePageViewModel(this))
        );

        GoSettingsPage = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new SettingsPageViewModel(this))
        );
    }

    public string SearchBoxText
    {
        get => _searchBoxText;
        set => this.RaiseAndSetIfChanged(ref _searchBoxText, value);
    }

    // The commands that navigates to a specified page
    public ReactiveCommand<Unit, IRoutableViewModel> GoHomePage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoSearchPage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoRecommendationsPage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoSeasonalAnimePage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoTimetablePage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoWatchPage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoProfilePage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoSettingsPage { get; }

    // The command that navigates a user back.
    public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

    public IObservable<string> SearchBoxTextObservable { get; }

    // The command that handles searching
    public ReactiveCommand<Unit, Unit> ExecuteSearch { get; }


    // The Router associated with this Screen.
    // Required by the IScreen interface
    public RoutingState Router { get; } = new();
}