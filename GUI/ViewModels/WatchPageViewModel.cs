using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Input;
using Avalonia.Threading;
using LibVLCSharp.Shared;
using ReactiveUI;

namespace GUI.ViewModels;

public class WatchPageViewModel : ReactiveObject, IRoutableViewModel, IDisposable
{
    private static readonly LibVLC _libVlc = new();
    private static MediaPlayer? _videoPlayer;
    private CompositeDisposable _subscriptions;

    private long _videoSeekTime = 5000;
    public WatchPageViewModel(IScreen screen)
    {
        HostScreen = screen;

        _videoPlayer ??= new MediaPlayer(_libVlc);
        
        bool operationActive = false;
        var refresh = new Subject<Unit>();
        
        MediaUrl = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";


        //disable events while some operations active, as sometimes causing deadlocks
        IObservable<Unit> Wrap(IObservable<Unit> obs)
            => obs.Where(_ => !operationActive).Merge(refresh).ObserveOn(AvaloniaScheduler.Instance);

        IObservable<Unit> VLCEvent(string name)
            => Observable.FromEventPattern(VideoPlayer, name).Select(_ => Unit.Default);


        void Op(Action action)
        {
            operationActive = true;
            action();
            operationActive = false;
            refresh.OnNext(Unit.Default);
        }

        
        var positionChanged = VLCEvent(nameof(VideoPlayer.PositionChanged));
        var playingChanged = VLCEvent(nameof(VideoPlayer.Playing));
        var stoppedChanged = VLCEvent(nameof(VideoPlayer.Stopped));
        var timeChanged = VLCEvent(nameof(VideoPlayer.TimeChanged));
        var lengthChanged = VLCEvent(nameof(VideoPlayer.LengthChanged));
        var muteChanged = VLCEvent(nameof(VideoPlayer.Muted))
            .Merge(VLCEvent(nameof(VideoPlayer.Unmuted)));
        var endReachedChanged = VLCEvent(nameof(VideoPlayer.EndReached));
        var pausedChanged = VLCEvent(nameof(VideoPlayer.Paused));
        var volumeChanged = VLCEvent(nameof(VideoPlayer.VolumeChanged));
        var stateChanged = Observable.Merge(playingChanged, stoppedChanged, endReachedChanged, pausedChanged);
        var hasMediaObservable = this.WhenAnyValue(v => v.MediaUrl, v => !string.IsNullOrEmpty(v));
        var fullState = Observable.Merge(
            stateChanged,
            VLCEvent(nameof(MediaPlayer.NothingSpecial)),
            VLCEvent(nameof(MediaPlayer.Buffering)),
            VLCEvent(nameof(MediaPlayer.EncounteredError))
        );
        
        
        _subscriptions = new CompositeDisposable
        {
            Wrap(positionChanged).DistinctUntilChanged(_ => MediaPosition).Subscribe(_ => this.RaisePropertyChanged(nameof(MediaPosition))),
            Wrap(timeChanged).DistinctUntilChanged(_ => CurrentTime).Subscribe(_ => this.RaisePropertyChanged(nameof(CurrentTime))),
            Wrap(lengthChanged).DistinctUntilChanged(_ => Duration).Subscribe(_ => this.RaisePropertyChanged(nameof(Duration))),
            Wrap(muteChanged).DistinctUntilChanged(_ => IsMuted).Subscribe(_ => this.RaisePropertyChanged(nameof(IsMuted))),
            Wrap(fullState).DistinctUntilChanged(_ => State).Subscribe(_ => this.RaisePropertyChanged(nameof(State))),
            Wrap(volumeChanged).DistinctUntilChanged(_ => Volume).Subscribe(_ => this.RaisePropertyChanged(nameof(Volume))),
            Wrap(fullState).DistinctUntilChanged(_ => Information).Subscribe(_ => this.RaisePropertyChanged(nameof(Information)))
        };
        
        
        bool active() => _subscriptions == null ? false : VideoPlayer.IsPlaying || VideoPlayer.CanPause;
        stateChanged = Wrap(stateChanged);

        PlayPauseCommand = ReactiveCommand.Create(() =>
        {
            if (_videoPlayer.IsPlaying)
            {
                _videoPlayer.Pause();
                stateChanged.Select(_ => active());
            }
            else
            {
                Op(() =>
                {
                    VideoPlayer?.Play();
                });
            }
        }, hasMediaObservable);
        
        
        StopCommand = ReactiveCommand.Create(
            () => Op(() => VideoPlayer?.Stop()),
            stateChanged.Select(_ => active()));
        
        
        /*
        SetMedia = ReactiveCommand.Create(() =>
        {
            var media = new Media(_libVlc, MediaUrl, FromType.FromLocation);
            _videoPlayer.Media = media;
            /*MediaLength = _videoPlayer.Length;#1#
        });
        */

        BackwardCommand = ReactiveCommand.Create(
            () => _videoPlayer.Time -= _videoSeekTime,
            stateChanged.Select(_ => active()));


        ForwardCommand = ReactiveCommand.Create(
            () => _videoPlayer.Time += _videoSeekTime,
            stateChanged.Select(_ => active()));

        var media = new Media(_libVlc, MediaUrl, FromType.FromLocation);
        _videoPlayer.Media = media;

    }

    // Reference to IScreen that owns the routable view model
    public IScreen HostScreen { get; }

    public long MediaLength { get; set; }
    public string MediaUrl { get; set; }

    public float MediaPosition
    {
        get
        {
            if (VideoPlayer != null)
                return VideoPlayer.Position * 100.0f;
            return 0;
        }
        set 
        {
            if (VideoPlayer != null && VideoPlayer.Position != value / 100.0f)
            {
                VideoPlayer.Position = value / 100.0f;
            }
        }
    }
    
    public bool IsMuted
    {
        get => VideoPlayer is {Mute: true};
        set
        {
            if (VideoPlayer != null)
            {
                VideoPlayer.Mute = value;
            }
        }
    }
    

    public TimeSpan CurrentTime => TimeSpan.FromMilliseconds(VideoPlayer is {Time: > -1} ? VideoPlayer.Time : 0);
    public TimeSpan Duration => TimeSpan.FromMilliseconds(VideoPlayer is {Length: > -1} ? VideoPlayer.Length : 0);
    public VLCState State => VideoPlayer!.State;

    // Unique identifier for the routable view model
    public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public MediaPlayer? VideoPlayer
    {
        get => _videoPlayer;
        set => this.RaiseAndSetIfChanged(ref _videoPlayer, value);
    }
    
    public int Volume
    {
        get => VideoPlayer.Volume;
        set => VideoPlayer.Volume = value;
    }

    public string Information => VideoPlayer != null ? $"FPS: {VideoPlayer.Fps}" : "FPS: Unknown";
    
    private IObservable<Unit> positionChanged;
    
    public ICommand PlayPauseCommand { get; }
    public ICommand StopCommand { get; }
    public ICommand BackwardCommand { get; }
    public ICommand ForwardCommand { get; }
    public void Dispose()
    {
        VideoPlayer?.Dispose();
    }
}