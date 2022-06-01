using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace UIManager.Models;

public class StatusBarModel : INotifyPropertyChanged
{
    private string _searchText;
    private string _title;
    public event PropertyChangedEventHandler? PropertyChanged;

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (value == _searchText) return;
            _searchText = value;
            OnPropertyChanged();
        }
    }
    public string Title
    {
        get => _title;
        set
        {
            if (value == _title) return;
            _title = value;
            OnPropertyChanged();
        }
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}