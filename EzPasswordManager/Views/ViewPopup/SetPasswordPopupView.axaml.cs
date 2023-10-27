using Avalonia.Controls;
using EzPasswordManager.ViewModels;

namespace EzPasswordManager.Views.ViewPopup
{
    public partial class SetPasswordPopupView : UserControl
    {
        private PasswordsViewModel viewModel;

        public SetPasswordPopupView(PasswordsViewModel passwordsViewModel)
        {
            InitializeComponent();

            viewModel = passwordsViewModel;
        }

        public SetPasswordPopupView()
        {
            InitializeComponent();

            viewModel = new PasswordsViewModel();
        }

        private void CancelAddPasswordClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            viewModel.PasswordsPopupVisible = false;
            viewModel.AddPasswordInfo = new DataTypes.PasswordInfoStructure();
        }
    }
}
