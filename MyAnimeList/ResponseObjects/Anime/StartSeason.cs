using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class StartSeason
    {
        public StartSeason(int year, string season)
        {
            this.Year = year;
            this.Season = season;
        }

        [DataMember]
        public int Year { get; }

        [DataMember]
        public string Season { get; }
        
        public override string ToString()
        {
            return $"Year: {Year}, "
                   + $"Season: {Season}"
                ;
        }
    }
}