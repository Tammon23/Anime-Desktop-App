using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace GUI.Views.SettingsViews;

public partial class SettingsOtherView : ReactiveUserControl<SettingsOtherView>
{
    public SettingsOtherView()
    {
        this.WhenActivated(_ => { });
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}