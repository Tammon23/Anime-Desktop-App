/*
Design by: https://github.com/donandren
Package: https://github.com/donandren/vlcsharpavalonia
*/

using Avalonia.Controls;
using LibVLCSharp.Shared;

namespace LibVLCSharp.Avalonia;

public static class AppBuilderExtensions
{
    public static T UseVLCSharp<T>(this AppBuilderBase<T> b, LibVLCAvaloniaRenderingOptions? renderingOptions = null, string? libVLCDirectoryPath = null)
        where T : AppBuilderBase<T>, new()
    {
        if (renderingOptions != null)
        {
            LibVLCAvaloniaOptions.RenderingOptions = renderingOptions.Value;
        }

        return b.AfterSetup(_ => Core.Initialize(libVLCDirectoryPath));
    }
}