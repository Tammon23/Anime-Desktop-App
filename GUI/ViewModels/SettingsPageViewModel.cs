using System;
using System.Reactive;
using GUI.ViewModels.SettingsViewModels;
using ReactiveUI;

namespace GUI.ViewModels;

public class SettingsPageViewModel : ReactiveObject, IRoutableViewModel, IScreen
{
    public SettingsPageViewModel(IScreen screen)
    {
        HostScreen = screen;
        Router.Navigate.Execute(new SettingsGeneralViewModel(this));
            
        GoSettingsGeneralPage = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new SettingsGeneralViewModel(this))
            );

        GoSettingsOtherPage = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new SettingsOtherViewModel(this))
            );
    }

    // Reference to IScreen that owns the routable view model
    public IScreen HostScreen { get; }

    // Unique identifier for the routable view model
    public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public ReactiveCommand<Unit, IRoutableViewModel> GoSettingsGeneralPage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoSettingsOtherPage { get; }
    public RoutingState Router { get; } = new();
}