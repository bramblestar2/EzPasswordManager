using Avalonia.Controls;
using Avalonia.Interactivity;
using EzPasswordManager.ViewModels;
using EzPasswordManager.Views.ViewPopup;

namespace EzPasswordManager.Views
{
    public partial class PasswordView : UserControl
    {
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

            viewModel.AddPasswordPopupView = new AddPasswordPopupView(viewModel);
        }

        protected override void OnUnloaded(RoutedEventArgs e)
        {
            base.OnUnloaded(e);

            viewModel.SavePasswords(_currentUsername, null);
        }

        private void AddPasswordClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            viewModel.AddPasswordInfo = new DataTypes.PasswordInfoStructure();
            viewModel.AddPasswordsVisible = true;
        }

        private void CancelAddPasswordClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            viewModel.AddPasswordsVisible = false;
            viewModel.AddPasswordInfo = new DataTypes.PasswordInfoStructure();
        }

        private void CreateAddPasswordClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            viewModel.AddPasswordsVisible = false;
            viewModel.AddPassword(viewModel.AddPasswordInfo);
            viewModel.AddPasswordInfo = new DataTypes.PasswordInfoStructure();
        }
    }
}