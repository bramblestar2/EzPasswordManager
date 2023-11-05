using EzPasswordManager.CustomArgs;
using EzPasswordManager.Helpers;
using EzPasswordManager.Views;
using ReactiveUI;
using System;
using System.Reactive;

namespace EzPasswordManager.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string? _username;
        public string? Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        #region Commands

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }
        public ReactiveCommand<Unit, Unit> RegisterCommand { get; }

        #endregion

        
        public LoginView loginView { get; set; }

        public LoginViewModel()
        {
            //Login Button Command
            IObservable<bool> canLogin = this.WhenAnyValue(
                x => x.Username,
                x => x.Password,
                (username, password) => !string.IsNullOrWhiteSpace(username) &&
                                        !string.IsNullOrWhiteSpace(password) &&
                                        AccountManager.UserLoginExist(username, password) &&
                                        AccountManager.DoesUserFileExist(username, password)
                );

            LoginCommand = ReactiveCommand.Create(() =>
            {
                if (loginView is not null)
                    loginView.RaiseEvent(new UserLoginArgs(Username, Password, AccountManager.DefaultPasswordDirectory, LoginView.LoginEvent, loginView));
            }, canLogin);

            //Login Button Command
            IObservable<bool> canRegister = this.WhenAnyValue(
                x => x.Username,
                x => x.Password,
                (username, password) => !string.IsNullOrWhiteSpace(username) &&
                                        !string.IsNullOrWhiteSpace(password) &&
                                        !AccountManager.UserLoginExist(username, password) &&
                                        !AccountManager.DoesUserFileExist(username, password)
                );

            RegisterCommand = ReactiveCommand.Create(() =>
            {
                AccountManager.CreateLogin(Username, Password);
                if (loginView is not null)
                    loginView.RaiseEvent(new UserLoginArgs(Username, Password, AccountManager.DefaultPasswordDirectory, LoginView.LoginEvent, loginView));
            }, canRegister);
        }
    }
}
