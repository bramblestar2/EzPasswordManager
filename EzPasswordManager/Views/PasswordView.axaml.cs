using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using EzPasswordManager.CustomArgs;
using EzPasswordManager.ViewModels;
using System;

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

            _currentUsername = username;

            viewModel.LoadPasswords(username, directory);
        }

        protected override void OnUnloaded(RoutedEventArgs e)
        {
            base.OnUnloaded(e);
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
    }
}