using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.General;

[DataContract]
public class Root<T>
{
    public Root(List<T> data, Paging paging)
    {
        Data = data;
        Paging = paging;
    }

    [DataMember] public IReadOnlyList<T> Data { get; }

    [DataMember] public Paging Paging { get; }

    public override string ToString()
    {
        return $"Data: {(Data != null ? string.Join(" | ", Data) : "N/A")}, "
               + $"Paging: {Paging}"
            ;
    }
}