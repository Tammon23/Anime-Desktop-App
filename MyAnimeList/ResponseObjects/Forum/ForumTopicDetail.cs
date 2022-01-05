using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.Anime;

namespace MyAnimeList.ResponseObjects.Forum
{
    [DataContract]
    public class ForumTopicDetail
    {
        public ForumTopicDetail(Data data, Paging paging)
        {
            this.Data = data;
            this.Paging = paging;
        }

        [DataMember]
        public Data Data { get; }

        [DataMember]
        public Paging Paging { get; }

        public override string ToString()
        {
            return $"Data: {Data}, "
                    + $"Paging: {Paging}"
                ;
        }
    }
}