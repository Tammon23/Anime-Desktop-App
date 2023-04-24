using ReactiveUI;

namespace GUI.ViewModels.SettingsViewModels;

public class SettingsOtherViewModel : ReactiveObject, IRoutableViewModel
{
    public SettingsOtherViewModel(IScreen screen)
    {
        HostScreen = screen;
    }

    public string? UrlPathSegment { get; } = nameof(SettingsOtherViewModel);
    public IScreen HostScreen { get; }
}