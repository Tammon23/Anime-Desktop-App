using System.Diagnostics;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace GUI.Templates;

public class SidePanelPageButton : RadioButton/*TemplatedControl*/
{

    public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<SidePanelPageButton, string>(
        nameof(Text), "Button Text");

    public static readonly StyledProperty<int> IconSizeProperty = AvaloniaProperty.Register<SidePanelPageButton, int>(
        nameof(IconSize), 24);

    public new static readonly StyledProperty<double> FontSizeProperty =
        AvaloniaProperty.Register<SidePanelPageButton, double>(
            nameof(FontSize), 20);

    public static readonly StyledProperty<Geometry> IconDataProperty =
        AvaloniaProperty.Register<SidePanelPageButton, Geometry>(
            nameof(IconData),
            Geometry.Parse(
                "M12,12L14.33,16H9.68L12,12M12,8L6.21,18H17.8L12,8M12,2L1,21H23L12,2M12,6L19.53,19H4.47L12,6Z"));

    public static readonly StyledProperty<int> BorderWidthProperty =
        AvaloniaProperty.Register<SidePanelPageButton, int>(
            nameof(BorderWidth), 48);

    /*
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<SidePanelPageButton, ICommand>(
            nameof(Command));*/

    /*public static readonly StyledProperty<string> GroupNameProperty = AvaloniaProperty.Register<SidePanelPageButton, string>(
        "GroupName");*/

    /*public static readonly StyledProperty<bool> IsCheckedProperty = AvaloniaProperty.Register<SidePanelPageButton, bool>(
        "IsChecked");*/

    /*public bool IsChecked
    {
        get => GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }*/
    /*public string GroupName
    {
        get => GetValue(GroupNameProperty);
        set => SetValue(GroupNameProperty, value);
    }*/

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

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

    public int BorderWidth
    {
        get => GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    /*public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }*/
}