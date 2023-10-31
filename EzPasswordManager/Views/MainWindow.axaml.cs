using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using EzPasswordManager.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;
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

        //const string connectionUri = "mongodb+srv://jatcat151:<password>@cluster0.pk6ceqb.mongodb.net/?retryWrites=true&w=majority";
        //var settings = MongoClientSettings.FromConnectionString(connectionUri);
        //// Set the ServerApi field of the settings object to Stable API version 1
        //settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        //// Create a new client and connect to the server
        //var client = new MongoClient(settings);
        //// Send a ping to confirm a successful connection
        //try
        //{
        //    var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
        //    Debug.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
        //}
        //catch (Exception ex)
        //{
        //    Debug.WriteLine(ex);
        //}
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
