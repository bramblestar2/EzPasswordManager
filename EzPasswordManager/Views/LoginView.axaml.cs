using Avalonia.Controls;
using EzPasswordManager.ViewModels;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EzPasswordManager.Views
{
    public partial class LoginView : UserControl
    {
        private LoginViewModel viewModel;

        public LoginView()
        {
            InitializeComponent();

            viewModel = new LoginViewModel();
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
