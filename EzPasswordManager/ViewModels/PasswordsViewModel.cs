﻿using EzPasswordManager.CustomArgs;
using EzPasswordManager.DataTypes;
using EzPasswordManager.Helpers;
using EzPasswordManager.Views;
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
        private object? _passwordPopupView;
        public object? PasswordPopupView
        {
            get => _passwordPopupView;
            set => this.RaiseAndSetIfChanged(ref _passwordPopupView, value);
        }

        private ObservableCollection<PasswordInfoStructure> _passwords;
        public ObservableCollection<PasswordInfoStructure> Passwords
        {
            get => _passwords;
            set { this.RaiseAndSetIfChanged(ref _passwords, value); }
        }

        private int? _passwordsListSelectedIndex;
        public int? PasswordsListSelectedIndex
        {
            get => _passwordsListSelectedIndex;
            set => this.RaiseAndSetIfChanged(ref _passwordsListSelectedIndex, value);
        }

        private bool _passwordsPopupVisible = false;
        public bool PasswordsPopupVisible
        {
            get { return _passwordsPopupVisible; }
            set { this.RaiseAndSetIfChanged(ref _passwordsPopupVisible, value); }
        }

        private PasswordInfoStructure _addPasswordInfo = new PasswordInfoStructure();
        public PasswordInfoStructure AddPasswordInfo
        {
            get => _addPasswordInfo;
            set => this.RaiseAndSetIfChanged(ref _addPasswordInfo, value);
        }

        public PasswordView _passwordView { get; set; }

        public readonly string DefaultPasswordDirectory;

        private string? CurrentUsername;

        #region Commands

        //POPUP VIEW COMMANDS
        public ReactiveCommand<Unit, Unit> CreatePasswordCommand { get; }
        public ReactiveCommand<Unit, Unit> SetPasswordCommand { get; }

        //MAIN VIEW COMMANDS
        public ReactiveCommand<Unit, Unit> ShowPasswordPopupCommand { get; }
        public ReactiveCommand<Unit, Unit> RemovePasswordCommand { get; }
        public ReactiveCommand<Unit, Unit> EditPasswordCommand { get; }
        public ReactiveCommand<Unit, Unit> DeselectPasswordCommand { get; }

        public ReactiveCommand<Unit, Unit> DeleteAccountCommand { get; }

        #endregion

        public PasswordsViewModel()
        {
            Passwords = new ObservableCollection<PasswordInfoStructure>();

            DefaultPasswordDirectory = Path.Combine(Directory.GetCurrentDirectory(), "EZPassMangr\\");


            #region Setup Popup Commands

            //Add Password Button (POPUP)
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
                this.PasswordsPopupVisible = false;
                this.AddPassword(AddPasswordInfo);
                this.AddPasswordInfo = new DataTypes.PasswordInfoStructure();
            }, canCreatePassword);


            //Set Password Button (POPUP)
            IObservable<bool> canSetPassword = this.WhenAnyValue(
                x => x.AddPasswordInfo.DisplayName,
                x => x.AddPasswordInfo.Username,
                x => x.AddPasswordInfo.Password,
                (Displayname, Username, Password) =>
                     !string.IsNullOrWhiteSpace(Displayname) ||
                     !string.IsNullOrWhiteSpace(Username) ||
                     !string.IsNullOrWhiteSpace(Password)
                );

            SetPasswordCommand = ReactiveCommand.Create(() =>
            {
                if (PasswordsListSelectedIndex is not null)
                    this.Passwords[PasswordsListSelectedIndex.Value] = this.AddPasswordInfo;
                this.PasswordsPopupVisible = false;
                this.PasswordsListSelectedIndex = -1;
                this.AddPasswordInfo = new DataTypes.PasswordInfoStructure();
            }, canSetPassword);

            #endregion

            #region Setup Main Commands

            //Remove Password Button (MAIN)
            IObservable<bool> canRemovePassword = this.WhenAnyValue(
                x => x.PasswordsListSelectedIndex,
                x => x is not null && x >= 0
                );

            RemovePasswordCommand = ReactiveCommand.Create(() =>
            {
                if (PasswordsListSelectedIndex is not null)
                    this.RemovePassword(PasswordsListSelectedIndex.Value);

                this.PasswordsListSelectedIndex = -1;
            }, canRemovePassword);


            //Edit Password Button (MAIN)
            IObservable<bool> canEditPassword = this.WhenAnyValue(
                x => x.PasswordsListSelectedIndex,
                x => x is not null && x >= 0
                );

            EditPasswordCommand = ReactiveCommand.Create(() =>
            {
                if (PasswordsListSelectedIndex is not null)
                {
                    AddPasswordInfo = new PasswordInfoStructure(Passwords[PasswordsListSelectedIndex.Value]);
                    PasswordPopupView = new SetPasswordPopupView(this);
                    PasswordsPopupVisible = true;
                }
            }, canRemovePassword);

            //Show Password Popup Button
            ShowPasswordPopupCommand = ReactiveCommand.Create(() =>
            {
                this.PasswordsListSelectedIndex = -1;
                AddPasswordInfo = new DataTypes.PasswordInfoStructure();
                PasswordPopupView = new AddPasswordPopupView(this);
                PasswordsPopupVisible = true;
            });


            //Deselect Password Button (MAIN)
            IObservable<bool> canDeselectPassword = this.WhenAnyValue(
                x => x.PasswordsListSelectedIndex,
                x => x is not null && x >= 0
                );

            DeselectPasswordCommand = ReactiveCommand.Create(() =>
            {
                this.PasswordsListSelectedIndex = -1;
            }, canDeselectPassword);

            //Delete Account Button (MAIN)
            DeleteAccountCommand = ReactiveCommand.Create(() =>
            {
                PasswordPopupView = new ConfirmDeletionPopupView(this);
                PasswordsPopupVisible = true;
            });

            #endregion
        }


        public void DeleteCurrentAccount()
        {
            _passwordView.RaiseEvent(new UserLoginArgs(CurrentUsername, "", null, PasswordView.DeleteAccountEvent, _passwordView));
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
                {
                    var dir = Directory.CreateDirectory(directory);
                    dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.NotContentIndexed;
                }
            }
        }

        #region Keys
        private byte[] IV = { 210, 29, 113, 228, 186, 195, 253, 152, 9, 63, 188, 36, 236, 100, 59, 98 };
        private byte[] Key = { 11, 228, 250, 9, 33, 100, 137, 95, 173, 75, 170, 180, 233, 103, 227, 158,
                        225, 165, 215, 146, 170, 192, 142, 251, 64, 213, 222, 178, 148, 249, 36, 102 };
        #endregion

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

            CurrentUsername = username;

            List<PasswordInfoStructure>? items = null;
            using (StreamReader r = new StreamReader(Path.Combine(directory, username)))
            {
                string json = r.ReadToEnd();

                //Decryption
                try
                {
                    string decryptedText = TextEncryption.DecryptAES(json);

                    items = JsonConvert.DeserializeObject<List<PasswordInfoStructure>>(decryptedText);
                } catch (Exception ex)
                {
                    items = JsonConvert.DeserializeObject<List<PasswordInfoStructure>>(json);
                }
                //Decryption

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

        public async void SavePasswords(string username, string? directory = null)
        {
            CheckDirectory(ref directory);

            CreateAccount(username, directory);

            string json = JsonConvert.SerializeObject(Passwords, Formatting.Indented);

            try
            {
                string encryptedText = TextEncryption.EncryptAES(json);

                await File.WriteAllTextAsync(Path.Combine(directory, username), encryptedText);
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void SetPassword(PasswordInfoStructure passwordInfo, int index)
        {
            try
            {
                Passwords[index] = passwordInfo;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
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
