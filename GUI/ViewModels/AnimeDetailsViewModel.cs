using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using GUI.Models;
using MyAnimeList;
using MyAnimeList.FieldManager;
using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.General;
using ReactiveUI;

namespace GUI.ViewModels;

public class AnimeDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    private bool _animeInList;
    private bool _btnEpisodeCountDecreaseIsEnabled;
    private int _episodesSeen;
    
    private TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;
    private AnimeDetailsViewModel(IScreen screen)
    {
        HostScreen = screen;
        
        GoToLogInPage = ReactiveCommand.CreateFromObservable(
            () => HostScreen.Router.Navigate.Execute(new ProfilePageViewModel(HostScreen))
        );
        
        AddAnimeToList = ReactiveCommand.Create(() =>
        {
            AnimeInList = true;
        });
        
        IncreaseEpisodesSeen = ReactiveCommand.Create(() =>
        {
            EpisodesSeen += 1;

            if (EpisodesSeen != 0)
                BtnEpisodeCountDecreaseIsEnabled = true;

        });
        
        DecreaseEpisodesSeen = ReactiveCommand.Create(() =>
        {
            if (EpisodesSeen > 0)
                EpisodesSeen -= 1;

            if (EpisodesSeen == 0)
                BtnEpisodeCountDecreaseIsEnabled = false;
        });

        OpenAnimeInBrowser = ReactiveCommand.Create(() =>
        {
            Process.Start(new ProcessStartInfo($"https://myanimelist.net/anime/{Details?.Id}") { UseShellExecute = true });
        });
        
    }
    
    private IReadOnlyCollection<Node> AnimeRecommendations { get; init; }
    private IReadOnlyCollection<Node> AnimeRelated { get; init; }
    private IReadOnlyCollection<Node> MangaRelated { get; init; }
    
    public AnimeDetails? Details { get; private init; } // add ^ in view if does not work as is

    public string UrlPathSegment => "Details_Page";
    public IScreen HostScreen { get; }

    public string? Art => Details?.MainPicture.Large ?? Details?.MainPicture.Medium;
    



    public AnimeDetailsViewModel()
    {
        throw new NotImplementedException();
    }

    public static async Task<AnimeDetailsViewModel> CreateAsync(IScreen screen, int id)
    {
        var details = await GetDetails(id);
        List<Node> animeRecommendations = new();
        List<Node> relatedAnime = new();
        List<Node> relatedManga = new();
        
        if (details != null)
        {
            animeRecommendations.AddRange(details.Recommendations.Select(recAnime => new NodePresentation(recAnime.Node, screen)
            {
                UseLargePicture = true // eventually will be migrated to the settings plane
            }));
            
            relatedAnime.AddRange(details.RelatedAnime.Select(relAnime => new NodePresentation(relAnime.Node, screen)
            {
                UseLargePicture = true
            }));
            
            relatedManga.AddRange(details.RelatedManga.Select(relManga => new NodePresentation(relManga.Node, screen)
            {
                UseLargePicture = true
            }));
        }

        var selectedWatchStatusIndex = 0;// = null;
        var selectedRatingIndex = 0;// = null;
        var episodesSeen = 0;
        
        if (details?.MyListStatus != null)
        {
            var myStatus = details.MyListStatus;

            if (myStatus?.AnimeStatus == null)
                selectedWatchStatusIndex = -1;
            else
                selectedWatchStatusIndex = myStatus.AnimeStatus switch
                {
                    AnimeStatusEnum.Watching => 0,
                    AnimeStatusEnum.Completed => 1,
                    AnimeStatusEnum.OnHold => 2,
                    AnimeStatusEnum.Dropped => 3,
                    AnimeStatusEnum.PlanToWatch => 4,
                    _ => selectedWatchStatusIndex
                };


            if (myStatus?.Score is null or 0)
                selectedRatingIndex = -1;
            else
                selectedRatingIndex = 10 - myStatus.Score;

            episodesSeen = myStatus?.NumEpisodesWatched ?? 0;
        }
        
        var result = new AnimeDetailsViewModel(screen)
        {
            Details = details,
            AnimeRecommendations = animeRecommendations,
            AnimeRelated = relatedAnime,
            MangaRelated = relatedManga,
            AnimeInList = details?.MyListStatus != null,
            EpisodesSeen = episodesSeen,
            SelectedRatingIndex = selectedRatingIndex,
            SelectedWatchStatusIndex = selectedWatchStatusIndex
        };
        return result;
    }


    private static async Task<AnimeDetails?> GetDetails(int id)
    {
        var fieldsToSearchFor = new FieldSelector();
        fieldsToSearchFor.AddAllFields();
        
        var result = await Anime.GetAnimeDetails(id, fieldsToSearchFor);
        return result;
    }

    public bool IsLoggedIn => MALRequestClient.IsLoggedIn;
    
    public bool AnimeInList
    {
        get => _animeInList;
        set => this.RaiseAndSetIfChanged(ref _animeInList, value);
    }
    public string SelectedWatchStatusItem { get; set; }
    public int SelectedWatchStatusIndex { get; set; }
    
    public int SelectedRatingIndex { get; set; }


    public int EpisodesSeen
    {
        get => _episodesSeen; 
        set => this.RaiseAndSetIfChanged(ref _episodesSeen, value);
    }
    public bool BtnEpisodeCountDecreaseIsEnabled { 
        get => _btnEpisodeCountDecreaseIsEnabled;
        set => this.RaiseAndSetIfChanged(ref _btnEpisodeCountDecreaseIsEnabled, value);
    }

    public string TypeDisplayString => Details?.MediaType == null ? "Unknown" : Util.MediaTypeToString(Details.MediaType);

    public string StatusDisplayString => Details?.Status == null ? "Unknown" : Util.AiringStatusToString(Details.Status);
    public string AiredDisplayString
    {
        get
        {
            if (Details?.StartDate == null && Details?.EndDate == null)
                return "Unknown";

            string start, end;

            if (Details?.StartDate == null)
                start = "?";
            else
                start = Details?.StartDate?.ToString("MMM d, yyyy") ?? string.Empty;

            
            if (Details?.EndDate == null)
                end = "?";
            else
                end = Details?.EndDate?.ToString("MMM d, yyyy") ?? string.Empty;

            return $"{start} to {end}";
        }
    }

    public string PremieredDisplayString =>
        Details?.StartSeason == null || (Details?.StartSeason?.Season == null && Details?.StartSeason?.Year == null)
            ? "Unknown"
            : $"{(Details?.StartSeason?.Season == null ? "Unknown Season" : _textInfo.ToTitleCase(Details?.StartSeason.Season))} " +
              $"{(Details?.StartSeason?.Year == null ? "Unknown Year" : Details?.StartSeason.Year)}";

    public string BroadcastDisplayString => 
        Details?.Broadcast == null 
            ? "Unknown" 
            : $"{Details.Broadcast.DayOfTheWeek}s at {Details.Broadcast.StartTime}";

    public string DurationDisplayString
    {
        get
        {
            if (Details?.AverageEpisodeDuration is null or 0)
                return "Unknown";

            var timespan = TimeSpan.FromSeconds((double)Details.AverageEpisodeDuration);

            return 
                timespan.Hours > 0 
                    ? $"{timespan.Hours} {(timespan.Hours > 1 ? "hours" : "hour")}, {timespan.Minutes} {(timespan.Minutes > 1 ? "mins" : "min")}." 
                    : $"{timespan.Minutes} {(timespan.Minutes > 1 ? "mins" : "min")}.";
        }
    }

    public string RatingDisplayString => Details?.Rating is null
        ? "Unknown"
        : Util.RatingToString(Details?.Rating).ToUpper().Replace("_", "-");

    public string RatingDescriptionDisplayString => Details?.Rating is null
        ? "Unknown"
        : Util.GetEnumDescription(Details?.Rating!);

    public string SourceDisplayString
    {
        get
        {
            if (Details?.Source is null)
                return "Unknown";

            var result = Details.Source.ToString()!;
            if (Details.Source == AnimeSourceTypeEnum.Four_koma_manga)
                result = result.Replace("Four", "4");

            result = result.Replace("_", " ");

            return _textInfo.ToTitleCase(result);
        }
    }
    public string NsfwDisplayString => Details?.Nsfw is null ? "Unknown" : _textInfo.ToTitleCase(Util.NsfwToString(Details?.Nsfw));
    public string NsfwDescriptionDisplayString => Details?.Nsfw is null ? "Unknown" : Util.GetEnumDescription(Details?.Nsfw!);
    
    
    public ReactiveCommand<Unit, IRoutableViewModel> GoToLogInPage { get; }
    public ReactiveCommand<Unit, Unit> AddAnimeToList { get; }
    public ReactiveCommand<Unit, Unit> IncreaseEpisodesSeen { get; }
    public ReactiveCommand<Unit, Unit> DecreaseEpisodesSeen { get; }
    public ReactiveCommand<Unit, Unit> OpenAnimeInBrowser { get; }
}