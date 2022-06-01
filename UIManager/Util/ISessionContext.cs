using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UIManager.Util;

public interface ISessionContext : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    string? StatusBarSearchText { get; set; }

    /*public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }*/
    // add tab id here to??
}