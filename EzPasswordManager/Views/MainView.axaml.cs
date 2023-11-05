using Avalonia.Controls;
using Avalonia.Input;
using EzPasswordManager.CustomControls;

namespace EzPasswordManager.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    public void Entered(object? s, PointerEventArgs e)
    {
        if (s is SlideOutButton button)
        {
            button.IsEnabled = false;
        }
    }

    public void Exited(object? s, PointerEventArgs e)
    {
        if (s is SlideOutButton button)
        {
            button.IsEnabled = true;
        }
    }
}
