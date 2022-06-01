using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UIManager.Views;

public partial class ContentCardView : UserControl
{
    public ContentCardView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}