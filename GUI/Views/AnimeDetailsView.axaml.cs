using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using GUI.ViewModels;
using ReactiveUI;

namespace GUI.Views;

public class AnimeDetailsView : ReactiveUserControl<AnimeDetailsViewModel>
{

    private Button _left;
    private Button _right;
    private Carousel _carousel;
    public AnimeDetailsView()
    {
        InitializeComponent();

        _carousel = this.Get<Carousel>("Carousel");
        _left = this.Get<Button>("Left");
        _right = this.Get<Button>("Right");
        
        _left.Click += (_, _) => _carousel.Previous();
        _right.Click += (_, _) => _carousel.Next();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(_ => { });
        AvaloniaXamlLoader.Load(this);
    }
}