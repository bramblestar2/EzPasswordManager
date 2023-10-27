using EzPasswordManager.Helpers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

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

        public readonly string DefaultPasswordDirectory;


        #region Commands

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }
        public ReactiveCommand<Unit, Unit> RegisterCommand { get; }

        #endregion

        public LoginViewModel()
        {
            DefaultPasswordDirectory = Path.Combine(Directory.GetCurrentDirectory(), "EZPassMangr\\");

            //Login Button Command
            IObservable<bool> canLogin = this.WhenAnyValue(
                x => x.Username,
                x => x.Password,
                (username, password) => !string.IsNullOrWhiteSpace(username) &&
                                        !string.IsNullOrWhiteSpace(password) &&
                                        DoesUserFileExist(username, password)
                );

            LoginCommand = ReactiveCommand.Create(() =>
            {
            }, canLogin);

            //Login Button Command
            IObservable<bool> canRegister = this.WhenAnyValue(
                x => x.Username,
                x => x.Password,
                (username, password) => !string.IsNullOrWhiteSpace(username) &&
                                        !string.IsNullOrWhiteSpace(password)
                );

            RegisterCommand = ReactiveCommand.Create(() =>
            {
            }, canRegister);
        }


        public bool DoesUserFileExist(string username, string password, string? directory = null)
        {
            CheckDirectory(ref directory);
            
            foreach (var file in Directory.GetFiles(directory))
            {
                try
                {
                    
                    string[] fileName = Path.GetFileNameWithoutExtension(file).Split(' ');
                    if (fileName.Length == 2)
                    {
                        string fileUsername, filePassword;
                        fileUsername = fileName[0];
                        filePassword = fileName[1];

                        if (username.Equals(fileUsername) && password.Equals(filePassword))
                            return true;
                    }
                } catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return false;
        }


        public void CheckDirectory(ref string? directory)
        {
            if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
            {
                directory = DefaultPasswordDirectory;
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
            }
        }
    }
}
