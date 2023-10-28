using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
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

            viewModel.SavePasswords(_currentUsername, null);
        }

        private void LogoutClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(LogoutEvent, this));
        }
    }
}