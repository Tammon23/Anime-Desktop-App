using Avalonia;
using Avalonia.AnimeViewer.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Avalonia.AnimeViewer.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel> // Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            /*
            this.WhenActivated(d => d(ViewModel.BuyMusicCommand.Subscribe(Close)));
        */
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}