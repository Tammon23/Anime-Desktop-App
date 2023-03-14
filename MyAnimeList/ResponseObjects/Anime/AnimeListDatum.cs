using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Anime;

[DataContract]
public class AnimeListDatum : Datum
{
    public AnimeListDatum(Node node)
        : base(node)
    {
    }

    public override string ToString()
    {
        return base.ToString();
    }
}