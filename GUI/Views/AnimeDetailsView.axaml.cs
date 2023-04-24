using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using GUI.ViewModels;
using ReactiveUI;

namespace GUI.Views;

public class AnimeDetailsView : ReactiveUserControl<AnimeDetailsViewModel>
{

    private Button _back;
    private Button _forward;
    private Carousel _carousel;
    public AnimeDetailsView()
    {
        InitializeComponent();

        _carousel = this.Get<Carousel>("CarouselAnimeImages");
        _back = this.Get<Button>("ButtonBackCarouselAnimeImages");
        _forward = this.Get<Button>("ButtonForwardCarouselAnimeImages");
        
        _back.Click += (_, _) => _carousel.Previous();
        _forward.Click += (_, _) => _carousel.Next();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(_ => { });
        AvaloniaXamlLoader.Load(this);
    }

    private void Spinner_OnSpin(object? sender, SpinEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}