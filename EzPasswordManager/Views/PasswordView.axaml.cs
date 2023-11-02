using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Dialogs;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using EzPasswordManager.CustomArgs;
using EzPasswordManager.DataTypes;
using EzPasswordManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EzPasswordManager.Views
{
    public partial class PasswordView : UserControl
    {
        #region Events

        public static readonly RoutedEvent<RoutedEventArgs> LogoutEvent =
            RoutedEvent.Register<LoginView, RoutedEventArgs>(nameof(Logout), RoutingStrategies.Bubble);

        public event EventHandler<RoutedEventArgs> Logout
        {
            add => AddHandler(LogoutEvent, value);
            remove => RemoveHandler(LogoutEvent, value);
        }


        public static readonly RoutedEvent<UserLoginArgs> DeleteAccountEvent =
            RoutedEvent.Register<LoginView, UserLoginArgs>(nameof(DeleteAccount), RoutingStrategies.Bubble);

        public event EventHandler<UserLoginArgs> DeleteAccount
        {
            add => AddHandler(DeleteAccountEvent, value);
            remove => RemoveHandler(DeleteAccountEvent, value);
        }

        #endregion

        private PasswordsViewModel viewModel;
        private string _currentUsername;
        public bool SavePasswords = true;

        public PasswordView(string username)
        {
            Init(username);
        }

        public PasswordView()
        {
            Init("username");
        }

        private void Init(string username, string? directory = null)
        {
            InitializeComponent();

            viewModel = new PasswordsViewModel();
            this.DataContext = viewModel;

            viewModel._passwordView = this;

            _currentUsername = username;

            viewModel.LoadPasswords(username, directory);
        }

        protected override void OnUnloaded(RoutedEventArgs e)
        {
            base.OnUnloaded(e);

            if (SavePasswords)
                this.SaveInfo();
        }

        public void SaveInfo()
        {
            viewModel.SavePasswords(_currentUsername);
        }

        private void LogoutClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(LogoutEvent, this));
        }

        private void DeleteAccountClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.RaiseEvent(new UserLoginArgs(_currentUsername, "", null, DeleteAccountEvent, this));
        }

        private async void ExportPasswordsClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            TopLevel? topLevel = TopLevel.GetTopLevel(this);

            if (topLevel is not null)
            {
                var file = await topLevel.StorageProvider.SaveFilePickerAsync(new Avalonia.Platform.Storage.FilePickerSaveOptions
                {
                    Title = "Save CSV File",
                    ShowOverwritePrompt = true,
                    DefaultExtension = "csv",
                    SuggestedFileName = _currentUsername + "_passwords",
                    FileTypeChoices = new[]
                    {
                        new FilePickerFileType("csv")
                        {
                            Patterns = new[] { "*.csv" },
                            MimeTypes = new[] { "csv/*" }
                        }
                    }
                });

                if (file is not null)
                {
                    this.SaveInfo();

                    string absolutePath = Uri.UnescapeDataString(file.Path.AbsolutePath);
                    DirectoryInfo? directory = Directory.GetParent(absolutePath);

                    File.Create(absolutePath).Close();
                    PasswordInfoStructure[] passwords = viewModel.Passwords.ToArray();

                    List<Task> tasks = new List<Task>();

                    using (FileStream fs = File.Create(absolutePath))
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        string layout = "";

                        layout += "\"Display Name\",";
                        layout += "\"Username\",";
                        layout += "\"Password\",";
                        layout += "\"Email\",";
                        layout += "\"Website\"";

                        tasks.Add(sw.WriteLineAsync(layout));

                        for (int i = 0; i < passwords.Length; i++)
                        {
                            string output = "";

                            output += "\"" + passwords[i].DisplayName + "\",";
                            output += "\"" + passwords[i].Username + "\",";
                            output += "\"" + passwords[i].Password + "\",";
                            output += "\"" + passwords[i].Email + "\",";
                            output += "\"" + passwords[i].Website + "\"";

                            tasks.Add(sw.WriteLineAsync(output));
                        }
                    }

                    await Task.WhenAll(tasks);
                }
            }
        }
    }
}