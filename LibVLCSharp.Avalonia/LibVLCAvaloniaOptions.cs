/*
Design by: https://github.com/donandren
Package: https://github.com/donandren/vlcsharpavalonia
*/

namespace LibVLCSharp.Avalonia;

public enum LibVLCAvaloniaRenderingOptions
{
    /// <summary>
    /// uses native handle passed to vlc, best possible performance
    /// </summary>
    VlcNative,

    /// <summary>
    /// uses default Avalonia rendering with image
    /// </summary>
    Avalonia,

    /// <summary>
    /// uses default Avalonia rendering with custom drawing operation, expected better performance with Avalonia
    /// </summary>
    AvaloniaCustomDrawingOperation
}

internal class LibVLCAvaloniaOptions
{
    public static LibVLCAvaloniaRenderingOptions RenderingOptions { get; set; } = LibVLCAvaloniaRenderingOptions.VlcNative;
}