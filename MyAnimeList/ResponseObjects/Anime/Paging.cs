using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class Paging
    {
        public Paging(string previous, string next)
        {
            Previous = previous;
            Next = next;
        }

        [DataMember]
        public string? Previous { get; }

        [DataMember]
        public string? Next { get; }
        
        /// <summary>
        /// Checks if a previous page URL is exists
        /// </summary>
        /// <returns>True if previous page URL exists, else false</returns>
        public bool HasPrevious()
        {
            return Previous != null;
        }
        
        /// <summary>
        /// Checks if a next page URL is exists
        /// </summary>
        /// <returns>True if next page URL exists, else false</returns>
        public bool HasNext()
        {
            return Next != null;
        }

        public override string ToString()
        {
            return $"Previous: {Previous}, "
                    + $"Next: {Next}"
                ;
        }
    }
}