using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.User;

[DataContract]
public class UserInformation : User
{
    public UserInformation(int id, string name, string picture, string? gender, string? birthday, string? location, DateTime joinedAt, AnimeStatistics animeStatistics, string? timeZone, bool isSupporter)
        : base(id, name)
    {
        Picture = picture;
        Gender = gender;
        Birthday = birthday;
        Location = location;
        JoinedAt = joinedAt;
        AnimeStatistics = animeStatistics;
        TimeZone = timeZone;
        IsSupporter = isSupporter;
    }

    [DataMember] public string Picture { get; }
    [DataMember] public string? Gender { get; }
    [DataMember] public string? Birthday { get; }
    [DataMember] public string? Location { get; }
    [DataMember(Name = "joined_at")] public DateTime JoinedAt { get; }
    [DataMember(Name = "anime_statistics")] public AnimeStatistics AnimeStatistics { get; }
    
    [DataMember(Name = "time_zone")] public string? TimeZone { get; }
    [DataMember(Name = "is_supporter")] public bool? IsSupporter { get; }
    public override string ToString()
    {
        return base.ToString()
               + $" Picture: {Picture} "
               + $"Gender: {Gender} "
               + $"Birthday: {Birthday} "
               + $"Location: {Location}, "
               + $"Joined At: {JoinedAt}, "
               + $"Anime Statistics: {AnimeStatistics} "
               + $"TimeZone: {TimeZone} "
               + $"Is Supporter: {IsSupporter} "
            ;
    }
}