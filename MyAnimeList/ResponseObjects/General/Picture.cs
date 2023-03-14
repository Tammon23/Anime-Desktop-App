using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.General;

[DataContract]
public class Picture
{
    public Picture(string medium, string large)
    {
        Medium = medium;
        Large = large;
    }

    [DataMember] public string Medium { get; }

    [DataMember] public string? Large { get; }

    public override string ToString()
    {
        return $"Medium: {Medium}, "
               + $"Large: {Large}"
            ;
    }
}