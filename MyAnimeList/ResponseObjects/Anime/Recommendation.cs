using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class Recommendation
    {
        public Recommendation(Node node, int numRecommendations)
        {
            this.Node = node;
            this.NumRecommendations = numRecommendations;
        }

        [DataMember]
        public Node Node { get; }

        [DataMember(Name = "num_recommendations")]
        public int NumRecommendations { get; }
        
        public override string ToString()
        {
            return $"Node: {Node}, "
                   + $"Number of Recommendations: {NumRecommendations}"
                ;
        }
    }
}