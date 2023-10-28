using Avalonia.Controls;
using Avalonia.Interactivity;
using EzPasswordManager.CustomArgs;
using EzPasswordManager.ViewModels;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EzPasswordManager.Views
{
    public partial class LoginView : UserControl
    {
        private LoginViewModel viewModel;

        #region Events

        public static readonly RoutedEvent<UserLoginArgs> LoginEvent =
            RoutedEvent.Register<LoginView, UserLoginArgs>(nameof(Login), RoutingStrategies.Bubble);

        public event EventHandler<UserLoginArgs> Login
        {
            add => AddHandler(LoginEvent, value);
            remove => RemoveHandler(LoginEvent, value);
        }

        #endregion

        public LoginView()
        {
            InitializeComponent();

            viewModel = new LoginViewModel();
            viewModel.loginView = this;
            this.DataContext = viewModel;
        }

        private void UsernameTextRestriction(object? sender, Avalonia.Controls.TextChangedEventArgs e)
        {
            if (viewModel.Username is not null)
            {
                Regex regex = new Regex(@"[^a-zA-Z0-9]+");
                if (regex.Matches(viewModel.Username).Count > 0)
                {
                    viewModel.Username = regex.Replace(viewModel.Username, "");
                }
            }
        }


        private void PasswordTextRestriction(object? sender, Avalonia.Controls.TextChangedEventArgs e)
        {
            if (viewModel.Password is not null)
            {
                Regex regex = new Regex(@"[^\!\@\#\$\%\&\*\(\)\-_\=\+\w]+");
                if (regex.Matches(viewModel.Password).Count > 0)
                {
                    viewModel.Password = regex.Replace(viewModel.Password, "");
                }
            }
        }
    }
}
