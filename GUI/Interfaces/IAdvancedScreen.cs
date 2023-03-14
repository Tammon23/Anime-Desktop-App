using System;
using System.Reactive;
using ReactiveUI;

namespace GUI.Interfaces;

public interface IAdvancedScreen : IScreen // IScreen contains the routing state
{
    // An observable property for the search text box used to track the text in the textbox 
    public IObservable<string> SearchBoxTextObservable { get; }

    // The reactive command connected to the search box submit button
    public ReactiveCommand<Unit, Unit> ExecuteSearch { get; }
}