﻿using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using EzPasswordManager.Helpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EzPasswordManager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        base.OnClosing(e);
    }

    private void Border_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        this.BeginMoveDrag(e);
    }

    private void CloseClicked(object? sender, RoutedEventArgs e)
    {
        if (View.Content is PasswordView view)
            view.SaveInfo();

        this.Close();
    }

    private void MinimizeClicked(object? sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }
}
