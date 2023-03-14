using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Visuals.Media.Imaging;

namespace GUI.Models;

/// <summary>
/// Source: https://gist.github.com/maxkatz6/bc6e25db4323ede3b9a631cd7511cdb5#file-asyncimage-cs
/// </summary>
public class AsyncImage : Image
{
    private static readonly HttpClient httpClient = new HttpClient();
 
    public static readonly StyledProperty<string?> AsyncSourceProperty =
        AvaloniaProperty.Register<AsyncImage, string?>(nameof(AsyncSource));
 
    public static readonly StyledProperty<string?> FallbackSourceProperty =
        AvaloniaProperty.Register<AsyncImage, string?>(nameof(FallbackSource));
 
    public static readonly StyledProperty<bool> IsImageLoadedProperty =
        AvaloniaProperty.Register<AsyncImage, bool>(nameof(IsImageLoaded));
 
    public static readonly DirectProperty<AsyncImage, BitmapInterpolationMode> InterpolationModeProperty =
        AvaloniaProperty.RegisterDirect<AsyncImage, BitmapInterpolationMode>(nameof(InterpolationMode), i => i._interpolationMode, (i, v) => i._interpolationMode = v);
 
    private BitmapInterpolationMode _interpolationMode = BitmapInterpolationMode.Default;
    private string? _currentSource;
 
    public string? AsyncSource
    {
        get => GetValue(AsyncSourceProperty);
        set => SetValue(AsyncSourceProperty, value);
    }
 
    public string? FallbackSource
    {
        get => GetValue(FallbackSourceProperty);
        set => SetValue(FallbackSourceProperty, value);
    }
 
    public bool IsImageLoaded
    {
        get => GetValue(IsImageLoadedProperty);
        set => SetValue(IsImageLoadedProperty, value);
    }
 
    public BitmapInterpolationMode InterpolationMode
    {
        get => _interpolationMode;
        set => SetAndRaise(InterpolationModeProperty, ref _interpolationMode, value);
    }
 
    protected override async void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
    {
        base.OnPropertyChanged(change);
 
        if (change.Property == SourceProperty)
        {
            IsImageLoaded = change.NewValue.HasValue && change.NewValue.Value is not null;
        }
        else if (change.Property == AsyncSourceProperty
            || change.Property == FallbackSourceProperty
            || change.Property == IsEnabledProperty
            || change.Property == InterpolationModeProperty)
        {
            Source = null;
 
            if (!IsEnabled)
            {
                return;
            }
 
            var height = Height;
 
            var sourcePath = AsyncSource ?? FallbackSource;
            if (_currentSource == sourcePath)
            {
                return;
            }
 
            if (sourcePath == null)
            {
                Source = null;
                return;
            }
 
            if (await GetImageFromSource(sourcePath, height) is { } normalSource)
            {
                Source = normalSource;
                _currentSource = sourcePath;
            }
            else if (FallbackSource != null && FallbackSource != sourcePath
                && await GetImageFromSource(FallbackSource, height) is { } fallbackSource)
            {
                Source = fallbackSource;
                _currentSource = FallbackSource;
            }
        }
    }
 
    private async Task<IImage?> GetImageFromSource(string sourcePath, double rescaleToHeight = double.NaN)
    {
        return await Task.Run(async () =>
        {
            Stream? imageStream = null;
 
            try
            {
                if (sourcePath.StartsWith("http"))
                {
                    var response = await httpClient.GetAsync(sourcePath, HttpCompletionOption.ResponseContentRead);
                    imageStream = await response.Content.ReadAsStreamAsync();
                }
                else if (sourcePath.StartsWith("avares"))
                {
                    var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                    imageStream = assets.Open(new Uri(sourcePath));
                }
                else
                {
                    imageStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
                }
 
                if (double.IsNaN(rescaleToHeight))
                {
                    return new Bitmap(imageStream);
                }
                else
                {
                    return Bitmap.DecodeToHeight(imageStream, (int)rescaleToHeight, InterpolationMode);
                }
            }
            catch (Exception ex)
            {
                /*Log.Logger.Warning(ex, "Image reading or resizing failed.");*/
                return null;
            }
            finally
            {
                imageStream?.Dispose();
            }
        });
    }
}