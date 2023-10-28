using Avalonia.Interactivity;
using EzPasswordManager.CustomArgs;
using EzPasswordManager.Helpers;
using EzPasswordManager.Views;
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

        
        public LoginView loginView { get; set; }

        public LoginViewModel()
        {
            DefaultPasswordDirectory = Path.Combine(Directory.GetCurrentDirectory(), "EZPassMangr\\");

            //Login Button Command
            IObservable<bool> canLogin = this.WhenAnyValue(
                x => x.Username,
                x => x.Password,
                (username, password) => !string.IsNullOrWhiteSpace(username) &&
                                        !string.IsNullOrWhiteSpace(password) &&
                                        UserLoginExist(username, password) &&
                                        DoesUserFileExist(username, password)
                );

            LoginCommand = ReactiveCommand.Create(() =>
            {
                if (loginView is not null)
                    loginView.RaiseEvent(new UserLoginArgs(Username, Password, DefaultPasswordDirectory, LoginView.LoginEvent, loginView));
            }, canLogin);

            //Login Button Command
            IObservable<bool> canRegister = this.WhenAnyValue(
                x => x.Username,
                x => x.Password,
                (username, password) => !string.IsNullOrWhiteSpace(username) &&
                                        !string.IsNullOrWhiteSpace(password) &&
                                        !UserLoginExist(username, password) &&
                                        !DoesUserFileExist(username, password)
                );

            RegisterCommand = ReactiveCommand.Create(() =>
            {
                CreateLogin(Username, Password);
                if (loginView is not null)
                    loginView.RaiseEvent(new UserLoginArgs(Username, Password, DefaultPasswordDirectory, LoginView.LoginEvent, loginView));
            }, canRegister);
        }


        public void CreateLogin(string username, string password, string? directory = null)
        {
            CheckDirectory(ref directory);

            if (!Path.Exists(Path.Combine(directory, ".logins")))
                File.Create(Path.Combine(directory, ".logins")).Close();

            if (Path.Exists(Path.Combine(directory, ".logins")))
            {
                string hashedPassword = TextEncryption.EncryptSHA(password);
                bool existingLogin = UserLoginExist(username, hashedPassword);


                if (!existingLogin)
                {
                    File.AppendAllText(Path.Combine(directory, ".logins"), username + " " + hashedPassword + "\n");
                    File.Create(Path.Combine(directory, username)).Close();
                }
            }
        }


        public bool UserLoginExist(string username, string password, string? directory = null)
        {
            CheckDirectory(ref directory);

            if (!Path.Exists(Path.Combine(directory, ".logins")))
                File.Create(Path.Combine(directory, ".logins")).Close();

            using (var fs = File.OpenRead(Path.Combine(directory, ".logins")))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8, true))
                {
                    string hashedPassword = TextEncryption.EncryptSHA(password);
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line is not null)
                        {
                            string[] strings = line.Split(" ");
                            if (strings.Length > 1)
                            {
                                string user = strings[0],
                                       pass = strings[1];

                                if (username.Equals(user) && hashedPassword.Equals(pass))
                                    return true;
                            }
                        }
                    }
                }
            }

            return false;
        }


        public bool DoesUserFileExist(string username, string password, string? directory = null)
        {
            CheckDirectory(ref directory);

            foreach (var file in Directory.GetFiles(directory))
            {
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    if (username.Equals(fileName))
                        return true;
                }
                catch (Exception ex)
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
