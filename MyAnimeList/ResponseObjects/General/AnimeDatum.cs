using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.Anime;

namespace MyAnimeList.ResponseObjects.General;

public class AnimeDatum : Datum
{
    public AnimeDatum(AnimeNode node) : base(node)
    {
        Node = node;
    }
    
    [DataMember] public new AnimeNode Node { get; }
    
    public override string ToString()
    {
        return $"Node: {Node}";
    }
}