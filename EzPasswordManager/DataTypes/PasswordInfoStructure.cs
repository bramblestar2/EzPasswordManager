using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzPasswordManager.DataTypes
{
    public class PasswordInfoStructure : ReactiveObject
    {
        [JsonIgnore]
        private string? displayName;
        [JsonIgnore]
        private string? email;
        [JsonIgnore]
        private string? username;
        [JsonIgnore]
        private string? password;
        [JsonIgnore]
        private string? website;

        [JsonProperty("DisplayName")]
        public string? DisplayName
        {
            get => displayName;
            set => this.RaiseAndSetIfChanged(ref displayName, value);
        }
        [JsonProperty("Email")]
        public string? Email
        {
            get => email;
            set => this.RaiseAndSetIfChanged(ref email, value);
        }
        [JsonProperty("Username")]
        public string? Username 
        {
            get => username;
            set => this.RaiseAndSetIfChanged(ref username, value);
        }
        [JsonProperty("Password")]
        public string? Password
        {
            get => password;
            set => this.RaiseAndSetIfChanged(ref password, value);
        }
        [JsonProperty("Website")]
        public string? Website
        {
            get => website;
            set => this.RaiseAndSetIfChanged(ref website, value);
        }
    }
}
