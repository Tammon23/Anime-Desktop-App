using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.ReactiveUI;
using GUI.ViewModels.SettingsViewModels;
using ReactiveUI;

namespace GUI.Views.SettingsViews;

public class SettingsGeneralView : ReactiveUserControl<SettingsGeneralViewModel>
{
    public SettingsGeneralView()
    {
        this.WhenActivated(_ => { });
        InitializeComponent();

        var oceanDark = new StyleInclude(new Uri("resm:Themes?assembly=GUI"))
        {
            Source = new Uri("avares://GUI/Themes/OceanDark.axaml")
        };
        var darkSimple = new StyleInclude(new Uri("resm:Themes?assembly=GUI"))
        {
            Source = new Uri("avares://GUI/Themes/DarkSimple.axaml")
        };

            
        var themes = this.Find<ComboBox>("Themes");
        themes.SelectionChanged += (_, _) =>
        {
            Application.Current.Styles[0] = themes.SelectedIndex switch
            {
                0 => oceanDark,
                1 => darkSimple,
                _ => Styles[0]
            };
        };
        Styles.Add(oceanDark);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}