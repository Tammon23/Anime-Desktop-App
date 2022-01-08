using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.General
{
    [DataContract]
    public class AlternativeTitles
    {
        public AlternativeTitles(List<string>? synonyms, string? en, string? ja)
        {
            this.Synonyms = synonyms;
            this.En = en;
            this.Ja = ja;
        }

        [DataMember]
        public IReadOnlyList<string>? Synonyms { get; }

        [DataMember]
        public string? En { get; }

        [DataMember]
        public string? Ja { get; }

        public override string ToString()
        {
            return $"Synonyms: {(Synonyms != null ? Synonyms : "N/A")}"
                + $"En: {(En != null ? En : "N/A")}"
                + $"Ja: {(Ja != null ? Ja : "N/A")}"
                ;
        }
    }

}