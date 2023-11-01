using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using EzPasswordManager.CustomControls;
using System.Diagnostics;

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
