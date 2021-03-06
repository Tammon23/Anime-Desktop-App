using System.ComponentModel;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using Avalonia.Dialogs;
using ReactiveUI;
using UIManager.Util;

namespace UIManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private IManagedNotificationManager _notificationManager;
    private ViewModelBase MainViewView;
    public MainWindowViewModel(IManagedNotificationManager notificationManager)
    {
        _notificationManager = notificationManager;
        
        ShowCustomManagedNotificationCommand = ReactiveCommand.Create(() =>
        {
            NotificationManager.Show(new NotificationViewModel(NotificationManager) { Title = "Hey There!", Message = "Did you know that Avalonia now supports Custom In-Window Notifications?" });
        });

        ShowManagedNotificationCommand = ReactiveCommand.Create(() =>
        {
            NotificationManager.Show(new Avalonia.Controls.Notifications.Notification("Welcome", "Avalonia now supports Notifications.", NotificationType.Information));
        });

        ShowNativeNotificationCommand = ReactiveCommand.Create(() =>
        {
            NotificationManager.Show(new Avalonia.Controls.Notifications.Notification("Error", "Native Notifications are not quite ready. Coming soon.", NotificationType.Error));
        });

        AboutCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var dialog = new AboutAvaloniaDialog();

            var mainWindow = (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

            await dialog.ShowDialog(mainWindow);
        });

        ExitCommand = ReactiveCommand.Create(() =>
        {
            (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).Shutdown();
        });
    }

    public IManagedNotificationManager NotificationManager
    {
        get { return _notificationManager; }
        set { this.RaiseAndSetIfChanged(ref _notificationManager, value); }
    }

    public ReactiveCommand<Unit, Unit> ShowCustomManagedNotificationCommand { get; }

    public ReactiveCommand<Unit, Unit> ShowManagedNotificationCommand { get; }

    public ReactiveCommand<Unit, Unit> ShowNativeNotificationCommand { get; }

    public ReactiveCommand<Unit, Unit> AboutCommand { get; }

    public ReactiveCommand<Unit, Unit> ExitCommand { get; }
    
}