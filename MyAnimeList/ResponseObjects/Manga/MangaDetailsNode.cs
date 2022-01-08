using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Manga
{
    [DataContract]
    public class MangaDetailsNode : Node
    {
        public MangaDetailsNode(int id, string firstName, string lastName, string title, Picture mainPicture, string name)
            : base(id, title, mainPicture)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Name = name;
        }

        [DataMember(Name="first_name")]
        public string FirstName { get; }

        [DataMember(Name="last_name")]
        public string LastName { get; }
        
        [DataMember]
        public string Name { get; }

        public override string ToString()
        {
            return base.ToString()
                    + $" ,First name: {FirstName}"
                    + $" ,Last Name: {LastName}"
                    + $" ,Name: {Name}"
                ;
        }
    }
}