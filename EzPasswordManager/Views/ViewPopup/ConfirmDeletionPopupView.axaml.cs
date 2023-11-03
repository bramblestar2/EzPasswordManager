using Avalonia.Controls;
using EzPasswordManager.ViewModels;

namespace EzPasswordManager.Views.ViewPopup
{
    public partial class ConfirmDeletionPopupView : UserControl
    {
        public PasswordsViewModel? viewModel;
        public ConfirmDeletionPopupView(PasswordsViewModel model)
        {
            InitializeComponent();

            viewModel = model;
        }

        public ConfirmDeletionPopupView()
        {
            InitializeComponent();
        }

        private void YesButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            viewModel?.DeleteCurrentAccount();
        }

        private void CancelButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (viewModel is not null)
            {
                viewModel.PasswordsPopupVisible = false;
            }
        }
    }
}
