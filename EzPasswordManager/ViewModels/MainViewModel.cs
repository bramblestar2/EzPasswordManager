using EzPasswordManager.Views;
using ReactiveUI;
using System.Diagnostics;

namespace EzPasswordManager.ViewModels;

public class MainViewModel : ViewModelBase
{
    public PasswordView? PasswordView { get; set; }
    public LoginView? LoginView { get; set; }

    private object? _currentView;

    public object? CurrentView
    {
        get => _currentView;
        set { this.RaiseAndSetIfChanged(ref _currentView, value); }
    }

    public MainViewModel() 
    {
        PasswordView = null;
        LoginView = new LoginView();

        LoginView.Login += LoginView_Login;

        CurrentView = LoginView;
    }

    private void LoginView_Login(object? sender, CustomArgs.UserLoginArgs e)
    {
        PasswordView = new PasswordView(e.Username);
        PasswordView.Logout += PasswordView_Logout;
        CurrentView = PasswordView;
    }

    private void PasswordView_Logout(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        LoginView = new LoginView();
        LoginView.Login += LoginView_Login;
        CurrentView = LoginView;
        PasswordView = null;
    }
}
