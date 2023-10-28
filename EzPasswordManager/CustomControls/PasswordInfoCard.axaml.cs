using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using EzPasswordManager.DataTypes;
using EzPasswordManager.ViewModels;
using ReactiveUI;

namespace EzPasswordManager.CustomControls
{
    public class PasswordInfoCard : ContentControl
    {
        public PasswordInfoCard()
        {
            this.Content = new DataTypes.PasswordInfoStructure()
            {
                DisplayName = "Display",
                Email = "Email",
                Username = "User",
                Password = "Pass",
                Website = "Website"
            };
        }
    }
}