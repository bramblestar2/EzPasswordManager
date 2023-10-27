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
        public PasswordInfoStructure(PasswordInfoStructure other)
        {
            this.DisplayName =  other.DisplayName;
            this.Email       =  other.Email      ;
            this.Username    =  other.Username   ;
            this.Password    =  other.Password   ;
            this.Website     =  other.Website    ;
        }

        public PasswordInfoStructure()
        {
            this.DisplayName =  null;
            this.Email       =  null;
            this.Username    =  null;
            this.Password    =  null;
            this.Website     =  null;
        }

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
