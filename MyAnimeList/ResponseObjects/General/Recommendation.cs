using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.General;

[DataContract]
public class Recommendation
{
    public Recommendation(Node node, int numRecommendations)
    {
        Node = node;
        NumRecommendations = numRecommendations;
    }

    [DataMember] public Node Node { get; }

    [DataMember(Name = "num_recommendations")]
    public int NumRecommendations { get; }

    public override string ToString()
    {
        return $"Node: {Node}, "
               + $"Number of Recommendations: {NumRecommendations}"
            ;
    }
}