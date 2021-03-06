using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using ReactiveUI;
using UIManager.Models;

namespace UIManager.ViewModels
{
    public class ContentCardViewModel: ViewModelBase
    {
        private Bitmap? _art;
        private readonly AnimeSearch _anime;

        public ContentCardViewModel(AnimeSearch anime)
        {
            _anime = anime;
        }

        public string AnimeTitle => _anime.SearchResult.Title;

        public Bitmap? Art
        {
            get => _art;
            private set => this.RaiseAndSetIfChanged(ref _art, value);
        }

        public async Task LoadCover()
        {
            await using var imageStream = await _anime.LoadAnimeArtLargeBitmapAsync();
            Art = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
        }

        public static async Task<IEnumerable<ContentCardViewModel>> LoadCached()
        {
            return (await AnimeSearch.LoadCachedAsync()).Select(x => new ContentCardViewModel(x));
        }

        public async Task SaveToDiskAsync()
        {
            await _anime.SaveAsync();

            if (Art != null)
            {
                await Task.Run(() =>
                {
                    using (var fs = _anime.SaveAnimeArtLargeBitmapStream())
                    {
                        Art.Save(fs);
                    }
                });
            }
        }
    }
}

