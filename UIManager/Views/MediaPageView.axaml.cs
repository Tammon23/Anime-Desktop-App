using System;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using UIManager.ViewModels;

namespace UIManager.Views;

public class MediaPageView : UserControl
{

    public MediaPageView()
    {
        InitializeComponent();

        DataContext = MediaPageViewModel.self;

        //it's open when we set text a bug in autocomplete?
        var autoComplete = this.Get<AutoCompleteBox>("mediaUrl");
        autoComplete.GetObservable(AutoCompleteBox.IsDropDownOpenProperty)
            .Skip(1).Take(1)
            .Subscribe(_ => autoComplete.IsDropDownOpen = false);

        switch (Application.Current?.ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktopLifetime:
                desktopLifetime.MainWindow.AddHandler(Window.WindowClosedEvent, OnMainWindowClosed);
                break;
            
            case ISingleViewApplicationLifetime singleViewLifetime:
                singleViewLifetime.MainView.AddHandler(Window.WindowClosedEvent, OnMainWindowClosed);
                break;
        }

    }

    void OnMainWindowClosed(Object sender, RoutedEventArgs e)
    {
        
        (DataContext as IDisposable)?.Dispose();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}