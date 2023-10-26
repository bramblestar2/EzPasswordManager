using Avalonia.Controls;
using EzPasswordManager.ViewModels;

namespace EzPasswordManager.Views.ViewPopup
{
    public partial class AddPasswordPopupView : UserControl
    {
        private PasswordsViewModel viewModel;

        public AddPasswordPopupView(PasswordsViewModel passwordsViewModel)
        {
            InitializeComponent();

            viewModel = passwordsViewModel;
        }


        private void CancelAddPasswordClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            viewModel.AddPasswordsVisible = false;
            viewModel.AddPasswordInfo = new DataTypes.PasswordInfoStructure();
        }
    }
}
