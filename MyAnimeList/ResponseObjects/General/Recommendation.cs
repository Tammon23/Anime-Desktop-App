using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.Anime;

namespace MyAnimeList.ResponseObjects.General;

[DataContract]
public class Recommendation
{
    public Recommendation(AnimeNode node, int numRecommendations)
    {
        Node = node;
        NumRecommendations = numRecommendations;
    }

    [DataMember] public AnimeNode Node { get; }

    [DataMember(Name = "num_recommendations")]
    public int NumRecommendations { get; }

    public override string ToString()
    {
        return $"Node: {Node}, "
               + $"Number of Recommendations: {NumRecommendations}"
            ;
    }
}