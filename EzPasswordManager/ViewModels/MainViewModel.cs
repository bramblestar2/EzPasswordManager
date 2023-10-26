using EzPasswordManager.Views;
using ReactiveUI;

namespace EzPasswordManager.ViewModels;

public class MainViewModel : ViewModelBase
{
    public PasswordView PasswordView { get; set; }

    private object? _currentView;

    public object? CurrentView
    {
        get => _currentView;
        set { this.RaiseAndSetIfChanged(ref _currentView, value); }
    }

    public MainViewModel() 
    {
        PasswordView = new PasswordView();

        CurrentView = PasswordView;
    }
}
