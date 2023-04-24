using ReactiveUI;

namespace GUI.ViewModels.SettingsViewModels;

public class SettingsGeneralViewModel : ReactiveObject, IRoutableViewModel
{
    public SettingsGeneralViewModel(IScreen screen)
    {
        HostScreen = screen;
    }
    
    
    public string? UrlPathSegment { get; } = "SettingsGeneralViewModel";
    public IScreen HostScreen { get; }
}