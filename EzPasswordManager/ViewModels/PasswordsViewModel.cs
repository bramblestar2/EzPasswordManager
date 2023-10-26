using EzPasswordManager.DataTypes;
using EzPasswordManager.Views.ViewPopup;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reactive;

namespace EzPasswordManager.ViewModels
{
    public class PasswordsViewModel : ViewModelBase
    {
        private AddPasswordPopupView _addPasswordPopupView;
        public AddPasswordPopupView AddPasswordPopupView
        {
            get => _addPasswordPopupView;
            set => this.RaiseAndSetIfChanged(ref _addPasswordPopupView, value);
        }

        private ObservableCollection<PasswordInfoStructure> _passwords;
        public ObservableCollection<PasswordInfoStructure> Passwords
        {
            get => _passwords;
            set { this.RaiseAndSetIfChanged(ref _passwords, value); }
        }

        private bool _addPasswordsVisible = false;
        public bool AddPasswordsVisible
        {
            get { return _addPasswordsVisible; }
            set { this.RaiseAndSetIfChanged(ref _addPasswordsVisible, value); }
        }

        private PasswordInfoStructure _addPasswordInfo = new PasswordInfoStructure();
        public PasswordInfoStructure AddPasswordInfo
        {
            get => _addPasswordInfo;
            set => this.RaiseAndSetIfChanged(ref _addPasswordInfo, value);
        }


        public readonly string DefaultPasswordDirectory;


        #region Commands

        public ReactiveCommand<Unit, Unit> CreatePasswordCommand { get; }

        #endregion


        public PasswordsViewModel()
        {
            Passwords = new ObservableCollection<PasswordInfoStructure>();

            DefaultPasswordDirectory = Path.Combine(Directory.GetCurrentDirectory(), "EZPassMangr\\");

            IObservable<bool> canCreatePassword = this.WhenAnyValue(
                x => x.AddPasswordInfo.DisplayName,
                x => x.AddPasswordInfo.Username,
                x => x.AddPasswordInfo.Password,
                (Displayname, Username, Password) => 
                     !string.IsNullOrWhiteSpace(Displayname) ||
                     !string.IsNullOrWhiteSpace(Username) ||
                     !string.IsNullOrWhiteSpace(Password)
                );

            CreatePasswordCommand = ReactiveCommand.Create(() =>
            {
                this.AddPasswordsVisible = false;
                this.AddPassword(AddPasswordInfo);
                this.AddPasswordInfo = new DataTypes.PasswordInfoStructure();
            }, canCreatePassword);
        }

        public void CreateAccount(string username, string directory)
        {
            if (!Path.Exists(Path.Combine(directory, username)))
            {
                File.Create(Path.Combine(directory, username)).Close();
            }
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

        /// <summary>
        /// Loads passwords from username file in provided directory
        /// </summary>
        /// <param name="username"></param>
        /// <param name="directory">If there is no string or if directory is invalid, the default password directory will be used</param>
        /// <returns>The amount of passwords loaded</returns>
        /// <exception cref="NotImplementedException"></exception>
        public int LoadPasswords(string username, string? directory = null)
        {
            CheckDirectory(ref directory);

            CreateAccount(username, directory);

            List<PasswordInfoStructure>? items = null;
            using (StreamReader r = new StreamReader(Path.Combine(directory, username)))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<PasswordInfoStructure>>(json);
            }

            if (items is null)
                return 0;
            else
            {
                Passwords.Clear();

                Passwords = new ObservableCollection<PasswordInfoStructure>(items);

                return items.Count;
            }
        }

        public void SavePasswords(string username, string? directory = null)
        {
            CheckDirectory(ref directory);

            CreateAccount(username, directory);

            string json = JsonConvert.SerializeObject(Passwords, Formatting.Indented);

            File.WriteAllText(Path.Combine(directory, username), json);
        }


        public void AddPassword(PasswordInfoStructure passwordInfo)
        {
            Passwords.Add(passwordInfo);
        }

        public void RemovePassword(PasswordInfoStructure passwordInfo)
        {
            Passwords.Remove(passwordInfo);
        }

        public void RemovePassword(int index)
        {
            Passwords.RemoveAt(index);
        }
    }
}
