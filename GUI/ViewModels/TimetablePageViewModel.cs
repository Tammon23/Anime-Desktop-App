using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using GUI.Models;
using MyAnimeList.ResponseObjects.General;
using ReactiveUI;

namespace GUI.ViewModels;

public class TimetablePageViewModel : ReactiveObject, IRoutableViewModel
{
    
    public TimetablePageViewModel(IScreen screen)
    {
        HostScreen = screen;
        var startOfWeek = DateTime.Today.AddDays(
            (int) DayOfWeek.Monday - 
            (int) DateTime.Today.DayOfWeek);
        
        MondayDate = startOfWeek.ToString("dd MMM");
        TuesdayDate = startOfWeek.AddDays(1).ToString("dd MMM");
        WednesdayDate = startOfWeek.AddDays(2).ToString("dd MMM");
        ThursdayDate = startOfWeek.AddDays(3).ToString("dd MMM");
        FridayDate = startOfWeek.AddDays(4).ToString("dd MMM");
        SaturdayDate = startOfWeek.AddDays(5).ToString("dd MMM");
        SundayDate = startOfWeek.AddDays(6).ToString("dd MMM");

        this.WhenAnyValue(x => x.ShowAllAnime)
            .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
            .Subscribe(async x => await PopulatePage(x));
    }

    private async Task PopulatePage(bool showAllAnime = true)
    {
        MondayItems.Clear();
        TuesdayItems.Clear();
        WednesdayItems.Clear();
        ThursdayItems.Clear();
        FridayItems.Clear();
        SaturdayItems.Clear();
        SundayItems.Clear();
        UnknownItems.Clear();
        
        IEnumerable<AnimeTimetables> animeTimetables;

        if (showAllAnime)
        {
            animeTimetables = await AnimeTimetables.GetAllAnimeTimetables();
        }
        else
        {
            animeTimetables = await AnimeTimetables.GetUserAnimeTimetables();
        }

        animeTimetables = animeTimetables.Where(x => x.ShowData.Language == TimetableLanguage.SUB);
        
        foreach (var anime in animeTimetables)
        {

            var node = new TimetableNodePresentation(anime.ContentNode, HostScreen, anime.DayOfTheWeek, anime.Time, anime.TimeZoneShort)
            {
                UseLargePicture = true // eventually will be migrated to the settings plane
            };
            
            
            switch (anime.DayOfTheWeek)
            {
                case DayOfWeek.Monday:
                    MondayItems.Add(node);
                    break;

                case DayOfWeek.Tuesday:
                    TuesdayItems.Add(node);
                    break;
                    
                case DayOfWeek.Wednesday:
                    WednesdayItems.Add(node);
                    break;
                    
                case DayOfWeek.Thursday:
                    ThursdayItems.Add(node);
                    break;
                    
                case DayOfWeek.Friday:
                    FridayItems.Add(node);
                    break;
                    
                case DayOfWeek.Saturday:
                    SaturdayItems.Add(node);
                    break;
                    
                case DayOfWeek.Sunday:
                    SundayItems.Add(node);
                    break;
                
                case null:
                    UnknownItems.Add(node);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(anime.DayOfTheWeek), "Could not assign dayofweek to respective list");
            }
        }
    }

    // Reference to IScreen that owns the routable view model
    public IScreen HostScreen { get; }
    
    // Unique identifier for the routable view model
    public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public bool ShowAllAnime { get; set; } = true;
    
    public string MondayDate { get; }
    public string TuesdayDate { get; }
    public string WednesdayDate { get; }
    public string ThursdayDate { get; }
    public string FridayDate { get; }
    public string SaturdayDate { get; }
    public string SundayDate { get; }
    
    public ObservableCollection<Node> MondayItems { get; } = new();
    public ObservableCollection<Node> TuesdayItems { get; } = new();
    public ObservableCollection<Node> WednesdayItems { get; } = new();
    public ObservableCollection<Node> ThursdayItems { get; } = new();
    public ObservableCollection<Node> FridayItems { get; } = new();
    public ObservableCollection<Node> SaturdayItems { get; } = new();
    public ObservableCollection<Node> SundayItems { get; } = new();

    public ObservableCollection<Node> UnknownItems { get; } = new();


}