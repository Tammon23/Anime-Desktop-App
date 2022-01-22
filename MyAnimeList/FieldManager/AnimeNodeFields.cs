using System.Text;
using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.FieldManager;

public class AnimeNodeFields
{
    public int? Id;
    public string? Title;
    public string? Synopsis;
    public double? Mean;
    public int? Rank;
    public int? Popularity;
    public int? NumListUsers;
    public int? NumScoringUsers;
    public NsfwEnum? Nsfw;
    public DateTime? CreatedAt;
    public DateTime? UpdatedAt;
    public string? MediaType;
    public string? Status;
    public int? NumEpisodes;
    public StartSeason? StartSeason;
    public Broadcast? Broadcast;
    public string? Source;
    public int? AverageEpisodeDuration;
    public RatingEnum? Rating;

    public string getFields()
    {
        StringBuilder sb = new StringBuilder();
        if (Id != null) sb.Append($"id={Id}");
        if (Synopsis != null) sb.Append($"synopsis={Synopsis}");
        if (Mean != null) sb.Append($"mean={Mean}");
        if (Rank != null) sb.Append($"rank={Rank}");
        if (Popularity != null) sb.Append($"popularity={Popularity}");
        if (NumListUsers != null) sb.Append($"num_list_users={NumListUsers}");
        if (NumScoringUsers != null) sb.Append($"num_scoring_users={NumScoringUsers}");
        if (Nsfw != null) sb.Append($"nsfw={Nsfw}");
        if (CreatedAt != null) sb.Append($"created_at={CreatedAt}");
        if (UpdatedAt != null) sb.Append($"updated_at={UpdatedAt}");
        if (MediaType != null) sb.Append($"media_type={MediaType}");
        if (Status != null) sb.Append($"status={Status}");
        if (NumEpisodes != null) sb.Append($"num_episodes={NumEpisodes}");
        if (StartSeason != null) sb.Append($"start_season={StartSeason}");
        if (Broadcast != null) sb.Append($"broadcast={Broadcast}");
        if (AverageEpisodeDuration != null) sb.Append($"id={AverageEpisodeDuration}");
        if (Source != null) sb.Append($"source={Source}");
        if (Rating != null) sb.Append($"rating={Rating}");

        return sb.ToString();
    }
}