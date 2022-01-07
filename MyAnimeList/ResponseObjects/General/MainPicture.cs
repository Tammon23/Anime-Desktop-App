using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.General
{
    [DataContract]
    public class MainPicture
    {
        public MainPicture(string medium, string large)
        {
            Medium = medium;
            Large = large;
        }

        [DataMember]
        public string Medium { get; }

        [DataMember]
        public string Large { get; }
        
        /// <summary>
        /// Checks if a medium image URL is exists
        /// </summary>
        /// <returns>True if medium image URL exists, else false</returns>
        public bool HasMediumPicture()
        {
            return Medium != null;
        }
        
        /// <summary>
        /// Checks if a large image URL is exists
        /// </summary>
        /// <returns>True if large image URL exists, else false</returns>
        public bool HasLargePicture()
        {
            return Large != null;
        }

        public override string ToString()
        {
            return $"Medium: {Medium}, "
                    + $"Large: {Large}"
                ;
        }
    }
}