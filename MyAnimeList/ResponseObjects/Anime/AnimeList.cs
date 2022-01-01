using System.Collections.Generic;
using System.Runtime.Serialization;

// https://json2csharp.com/json-to-csharp
namespace MyAnimeList.ResponseObjects.Anime
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
            return $"Medium: {Medium}, Large: {Large}";
        }
    }
    
    [DataContract]
    public class Node
    {
        public Node(int id, string title, MainPicture mainPicture)
        {
            Id = id;
            Title = title;
            MainPicture = mainPicture;
        }

        [DataMember]
        public int Id { get; }

        [DataMember]
        public string Title { get; }

        [DataMember(Name ="main_picture")]
        public MainPicture MainPicture { get; }
        
        public override string ToString()
        {
            return $"Id: {Id}, Title: {Title}, Picture Links: {MainPicture}";
        }
    }
    
    [DataContract]
    public class Datum
    {
        public Datum(Node node)
        {
            Node = node;
        }

        [DataMember]
        public Node Node { get; }

        public override string ToString()
        {
            return $"Node: {Node}";
        }
    }
    
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
            return $"Previous: {Previous}, Next: {Next}";
        }
    }
    
    [DataContract]
    public class AnimeList
    {
        public AnimeList(List<Datum> data, Paging paging)
        {
            Data = data;
            Paging = paging;
        }

        [DataMember]
        public IReadOnlyList<Datum> Data { get; }

        [DataMember]
        public Paging Paging { get; }
        

        public override string ToString()
        {
            return $"Data: {string.Join(" | ", Data)}, Paging: {Paging}";
        }
    }


}