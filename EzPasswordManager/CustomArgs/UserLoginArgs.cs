using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzPasswordManager.CustomArgs
{
    public class UserLoginArgs : RoutedEventArgs
    {
        public readonly string Username;
        public readonly string Password;
        public readonly string Directory;

        public UserLoginArgs(string username, string password, string directory, RoutedEvent? routedEvent, object? sender) : base(routedEvent, sender)
        { 
            Username = username;
            Password = password;
            Directory = directory;
        }
    }
}
