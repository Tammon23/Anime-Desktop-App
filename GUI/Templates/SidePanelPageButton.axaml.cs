using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace GUI.Templates;

public class SidePanelPageButton : RadioButton
{
    public static readonly StyledProperty<int> IconSizeProperty = AvaloniaProperty.Register<SidePanelPageButton, int>(
        nameof(IconSize), 24);

    public new static readonly StyledProperty<double> FontSizeProperty =
        AvaloniaProperty.Register<SidePanelPageButton, double>(
            nameof(FontSize), 20);

    public static readonly StyledProperty<Geometry> IconDataProperty =
        AvaloniaProperty.Register<SidePanelPageButton, Geometry>(
            nameof(IconData)/*,
            Geometry.Parse(
                "M12,12L14.33,16H9.68L12,12M12,8L6.21,18H17.8L12,8M12,2L1,21H23L12,2M12,6L19.53,19H4.47L12,6Z")*/);

    public static readonly StyledProperty<Thickness> IconMarginProperty =
        AvaloniaProperty.Register<SidePanelPageButton, Thickness>(
            nameof(IconMargin));

    public static readonly StyledProperty<bool> ShowTextProperty =
        AvaloniaProperty.Register<SidePanelPageButton, bool>(
            nameof(ShowText), true);
    
    public int IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    public new double FontSize
    {
        get => GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public Geometry IconData
    {
        get => GetValue(IconDataProperty);
        set => SetValue(IconDataProperty, value);
    }

    public Thickness IconMargin
    {
        get => GetValue(IconMarginProperty);
        set => SetValue(IconMarginProperty, value);
    }

    public bool ShowText
    {
        get => GetValue(ShowTextProperty);
        set => SetValue(ShowTextProperty, value);
    }
}