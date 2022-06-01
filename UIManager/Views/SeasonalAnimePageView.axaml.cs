using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UIManager.Views;

public class SeasonalAnimePageView : UserControl
{
    public SeasonalAnimePageView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}