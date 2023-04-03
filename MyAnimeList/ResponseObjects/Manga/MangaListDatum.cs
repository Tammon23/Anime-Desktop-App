using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Manga;

public class MangaListDatum : MangaDatum
{
    public MangaListDatum(MangaNode node)
        : base(node)
    {
    }
}