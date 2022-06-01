using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace UIManager;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktopLifetime:
                desktopLifetime.MainWindow = new MainWindow();
                break;
            case ISingleViewApplicationLifetime singleViewLifetime:
                singleViewLifetime.MainView = new MainView();
                break;
        }

        base.OnFrameworkInitializationCompleted();
            
        /*if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindow()
            };
            desktop.Exit += OnExit;

        }*/
    }
}