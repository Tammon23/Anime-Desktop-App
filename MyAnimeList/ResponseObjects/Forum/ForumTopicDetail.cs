using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Forum;

[DataContract]
public class ForumTopicDetail
{
    public ForumTopicDetail(List<ForumTopicDatum> data, Paging paging)
    {
        Data = data;
        Paging = paging;
    }

    [DataMember] public IReadOnlyList<ForumTopicDatum> Data { get; }

    [DataMember] public Paging Paging { get; }

    public override string ToString()
    {
        return $"Data: {(Data != null ? string.Join(" | ", Data) : "N/A")}, "
               + $"Paging: {Paging}"
            ;
    }
}