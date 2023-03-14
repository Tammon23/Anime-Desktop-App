using System;
using System.Reactive.Linq;
using Avalonia.Controls.Mixins;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using GUI.ViewModels;
using ReactiveUI;

namespace GUI.Views;

public class SearchPageView : ReactiveUserControl<SearchPageViewModel>
{
    public SearchPageView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposables =>
        {
            var vm = DataContext as SearchPageViewModel;

            // first we grab the values we want to observe
            // next we convert multiple values into 1 values (the selector)
            // next we filter out any false values
            // subscribe on a function we would like to call
            // then dispose (destroy, when the view is destroyed)
            ViewModel
                .WhenAnyObservable(x => x!.ButtonIsExecuting, x => x!.AnimeSearchText,
                    (isExecuting, searchText) => (isValid: isExecuting && !string.IsNullOrWhiteSpace(searchText),
                        searchText))
                .Where(value => value.isValid)
                .Subscribe(t => vm?.DoSearch(t.searchText))
                .DisposeWith(disposables);
        });
        AvaloniaXamlLoader.Load(this);
    }
}