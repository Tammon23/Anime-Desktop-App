/*
Design by: https://github.com/donandren
Package: https://github.com/donandren/vlcsharpavalonia
Modified by: https://github.com/Tammon23 
*/

using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Threading;
using Avalonia.Visuals.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace LibVLCSharp.Avalonia;

public class VLCImageRenderer : Image
{
    public VLCImageRenderer()
    {
        this.GetObservable(SourceProperty).Subscribe(v =>
        {
            _subscriptions?.Dispose();
            _subscriptions = null;

            if (v is not VlcSharpWriteableBitmap {Updated: { }} vb) return;
            
            var m = vb.Updated.Select(_ => vb.PixelSize)
                .DistinctUntilChanged()
                .ObserveOn(AvaloniaScheduler.Instance)
                .Subscribe(_ => InvalidateMeasure());

            var f = vb.Updated.Subscribe(_ => _stats.DeliveredFrame());

            _subscriptions = new CompositeDisposable(m, f);
        });
    }

    public static readonly AvaloniaProperty<VlcVideoSourceProvider> SourceProviderProperty =
        AvaloniaProperty.RegisterDirect<VLCImageRenderer, VlcVideoSourceProvider>(nameof(SourceProvider), v => v.SourceProvider, (x, v) => x.SourceProvider = v);

    private VlcVideoSourceProvider _sourceProvider;

    public VlcVideoSourceProvider SourceProvider
    {
        get => _sourceProvider;
        set
        {
            if (SetAndRaise(SourceProviderProperty, ref _sourceProvider, value))
            {
                Source = _sourceProvider?.VideoSource;
            }
        }
    }

    public static readonly DirectProperty<VLCImageRenderer, bool> DisplayRenderStatsProperty =
        VideoView.DisplayRenderStatsProperty.AddOwner<VLCImageRenderer>(v => v.DisplayRenderStats, (s, v) => s.DisplayRenderStats = v);

    private bool _displayRenderStats;

    public bool DisplayRenderStats
    {
        get => _displayRenderStats;
        set
        {
            if (SetAndRaise(DisplayRenderStatsProperty, ref _displayRenderStats, value))
            {
                _stats.Enabled = DisplayRenderStats;
                _stats.Reset();
            }
        }
    }

    public static readonly DirectProperty<VLCImageRenderer, bool> UseCustomDrawingOperationProperty =
        VideoView.DisplayRenderStatsProperty.AddOwner<VLCImageRenderer>(v => v.UseCustomDrawingOperation, (s, v) => s.UseCustomDrawingOperation = v);

    private bool _useCustomDrawingOperation = true;

    public bool UseCustomDrawingOperation
    {
        get => _useCustomDrawingOperation;
        set => SetAndRaise(UseCustomDrawingOperationProperty, ref _useCustomDrawingOperation, value);
    }

    public void ResetStats()
    {
        _stats.Reset();
    }

    private IDisposable? _subscriptions;

    private readonly struct CustomOp : ICustomDrawOperation
    {
        private readonly IBitmap _source;
        private readonly Rect _sourceRect;
        private readonly Rect _destRect;
        private readonly BitmapInterpolationMode _interpolationMode;
        private readonly RenderingStats _stats;

        public Rect Bounds => _destRect;

        public CustomOp(IBitmap source, Rect sourceRect, Rect destRect, BitmapInterpolationMode interpolationMode, RenderingStats stats)
        {
            _source = source;
            _sourceRect = sourceRect;
            _destRect = destRect;
            _interpolationMode = interpolationMode;
            _stats = stats;
        }

        public void Dispose()
        {
            if (_source is not VlcSharpWriteableBitmap)
                _source.Dispose();
        }

        public bool Equals(ICustomDrawOperation? other) => false;

        public bool HitTest(Point p) => false;

        public void Render(IDrawingContextImpl context)
        {
            try
            {
                using (_stats.RenderFrame())
                {
                    if (_source is VlcSharpWriteableBitmap vb)
                        vb.Render(context, 1, _sourceRect, _destRect, _interpolationMode);
                    else
                        context.DrawBitmap(_source.PlatformImpl, 1, _sourceRect, _destRect, _interpolationMode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error render:{e}");
            }

            _stats.Render(context, $"{_sourceRect.Width}x{_sourceRect.Height} -> {_destRect.Width.ToString("0.00")}x{_destRect.Height.ToString("0.00")}");
        }
    }

    public override void Render(DrawingContext context)
    {
        if (UseCustomDrawingOperation)
        {
            if (Source is not IBitmap source) return;
            
            var viewPort = new Rect(Bounds.Size);
            var sourceSize = source.PixelSize.ToSize(1);
            var scale = Stretch.CalculateScaling(Bounds.Size, sourceSize);
            var scaledSize = sourceSize * scale;
            var destRect = viewPort
                .CenterRect(new Rect(scaledSize))
                .Intersect(viewPort);
            var sourceRect = new Rect(sourceSize)
                .CenterRect(new Rect(destRect.Size / scale));

            var interpolationMode = RenderOptions.GetBitmapInterpolationMode(this);

            context.Custom(new CustomOp(source, sourceRect, destRect, interpolationMode, _stats));
        }
        else
        {
            using (_stats.RenderFrame())
            {
                base.Render(context);
            }

            var size = (Source as IBitmap)?.PixelSize ?? default(PixelSize);
            _stats.Render(context, $"{size.Width}x{size.Height}");
        }
    }

    private readonly RenderingStats _stats = new RenderingStats();

    private class RenderingStats
    {
        public int StatsFrameCount = 60;
        public int UpdateStatsPerFrames = 10;
        public bool Enabled;

        private readonly Queue<double> _frameTimes = new ();
        private readonly Queue<double> _frameDurations = new ();
        private Stopwatch _start;
        private int _rendered;
        private int _delivered;
        private double _last;

        public RenderingStats()
        {
            Reset();
        }

        public void Reset()
        {
            _frameTimes.Clear();
            _frameDurations.Clear();
            _frameTimes.Enqueue(0);
            _frameDurations.Enqueue(0);
            _start = Stopwatch.StartNew();
            _rendered = 0;
            _delivered = 0;
            _last = 0;
            _text = null;
        }

        public void DeliveredFrame() => _delivered++;

        public IDisposable RenderFrame()
        {
            if (_delivered <= _rendered)
            {
                //assume resize e.g. some forced invalidations not related to frames
                return Disposable.Empty;
            }

            var w = Stopwatch.StartNew();
            _last = _start.ElapsedMilliseconds;
            _frameTimes.Enqueue(_last);
            _rendered++;

            return Disposable.Create(() =>
            {
                w.Stop();
                _frameDurations.Enqueue(w.ElapsedMilliseconds);
                if (_frameDurations.Count <= StatsFrameCount) return;
                
                _frameTimes.Dequeue();
                _frameDurations.Dequeue();
            });
        }

        public override string ToString()
        {
            var dur = (_last - _frameTimes.Peek()) / _frameTimes.Count;
            string F(double d) => d.ToString("0.00");
            return $"total rendered:{_rendered}, delivered: {_delivered}, dropped: {_delivered - _rendered}\n" +
                   $"stats for last {_frameTimes.Count} frames, rendered fps: {F(1000.0 / dur)}, time for render: {F(_frameDurations.Average())} ms";
        }

        public void Render(DrawingContext context, string info, double x = 10, double y = 10)
        {
            Render(context.PlatformImpl, info, x, y);
        }

        private FormattedText? _text;

        public void Render(IDrawingContextImpl context, string info, double x = 10, double y = 10)
        {
            if (!Enabled) return;
            
            _text = _text != null && _rendered % UpdateStatsPerFrames != 0 ?
                _text :
                new FormattedText { Text = $"{info}\n{ToString()}", FontSize = 12, Typeface = Typeface.Default };
            context.DrawRectangle(Brushes.Black, null, _text.Bounds.Translate(new Vector(x, y)));
            context.DrawText(Brushes.White, new Point(x, y), _text.PlatformImpl);
        }
    }
}