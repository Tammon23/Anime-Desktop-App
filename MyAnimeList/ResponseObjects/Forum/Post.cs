using System;
using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum
{
    [DataContract]
    public class Post
    {
        public Post(string id, int number, DateTime createdAt, CreatedBy createdBy, string body, string signature)
        {
            this.Id = id;
            this.Number = number;
            this.CreatedAt = createdAt;
            this.CreatedBy = createdBy;
            this.Body = body;
            this.Signature = signature;
        }

        [DataMember]
        public string Id { get; }

        [DataMember]
        public int Number { get; }

        [DataMember(Name="created_at")]
        public DateTime CreatedAt { get; }

        [DataMember(Name="created_by")]
        public CreatedBy CreatedBy { get; }

        [DataMember]
        public string Body { get; }

        [DataMember(Name="signature")]
        public string Signature { get; }

        public override string ToString()
        {
            return $"Id: {Id}, "
                   + $"Number: {Number}, "
                   + $"Created At: {CreatedAt}, "
                   + $"Created By: {CreatedBy}, "
                   + $"Body: {Body}, "
                   + $"Signature: {Signature}"
                ;
        }
    }
}