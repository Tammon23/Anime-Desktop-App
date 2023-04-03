using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Anime;

[DataContract]
public class AnimeListDatum : AnimeDatum
{
    public AnimeListDatum(AnimeNode node)
        : base(node)
    {
    }

    public override string ToString()
    {
        return base.ToString();
    }
}