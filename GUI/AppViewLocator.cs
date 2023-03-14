using System;
using ReactiveUI;

namespace GUI;

public class AppViewLocator : IViewLocator
{
    public IViewFor? ResolveView<T>(T viewModel, string? contract = null)
    {
        var name = viewModel!.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);
        if (type == null)
            throw new ArgumentOutOfRangeException(nameof(viewModel));


        var instance = Activator.CreateInstance(type)!;

        var prop = type.GetProperty("DataContext")!;
        prop.SetValue(instance, viewModel);

        return instance as IViewFor;
    }
}