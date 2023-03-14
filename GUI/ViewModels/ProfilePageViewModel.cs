using System;
using ReactiveUI;

namespace GUI.ViewModels;

public class ProfilePageViewModel : ReactiveObject, IRoutableViewModel
{
    public ProfilePageViewModel(IScreen screen)
    {
        HostScreen = screen;
    }

    // Reference to IScreen that owns the routable view model
    public IScreen HostScreen { get; }

    // Unique identifier for the routable view model
    public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
}