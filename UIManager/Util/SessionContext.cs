using System.ComponentModel;

namespace UIManager.Util;

public class SessionContext : ISessionContext
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public string StatusBarSearchText { get; set; }
}