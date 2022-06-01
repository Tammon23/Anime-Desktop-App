using Avalonia.Controls;
using UIManager.Util;
using UIManager.ViewModels;

namespace UIManager.Models;

public class CustomTabItem
{
    public string Header { get; set; }
    public ViewModelBase Content { get; set; }
    public ISessionContext? SessionContext { get; set; }
}