using System;
using ReactiveUI;

namespace GUI.ViewModels;

public class HomePageViewModel : ReactiveObject, IRoutableViewModel

{
    public HomePageViewModel(IScreen screen)
    {
        HostScreen = screen;
    }

    public HomePageViewModel()
    {
        throw new NotImplementedException();
    }

    // Reference to IScreen that owns the routable view model
    public IScreen HostScreen { get; }

    // Unique identifier for the routable view model
    public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
}