using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EzPasswordManager.DataTypes
{
    public class PasswordInfoStructure : ReactiveObject
    {
        #region Constructors

        public PasswordInfoStructure(PasswordInfoStructure other)
        {
            this.DisplayName =  other.DisplayName;
            this.Email       =  other.Email      ;
            this.Username    =  other.Username   ;
            this.Password    =  other.Password   ;
            this.Website     =  other.Website    ;
            this.notes       =  other.Notes ;
        }

        public PasswordInfoStructure()
        {
        }

        #endregion

        #region Variables

        [JsonIgnore]
        private string displayName  = "";
        [JsonIgnore]
        private string email        = "";
        [JsonIgnore]
        private string username     = "";
        [JsonIgnore]
        private string password     = "";
        [JsonIgnore]
        private string website      = "";
        [JsonIgnore]
        private string notes      = "";

        #endregion

        #region Properties

        [JsonProperty("DisplayName",    DefaultValueHandling = DefaultValueHandling.Ignore),        DefaultValue("")]
        public string DisplayName
        {
            get => displayName;
            set => this.RaiseAndSetIfChanged(ref displayName, value);
        }

        [JsonProperty("Email",          DefaultValueHandling = DefaultValueHandling.Ignore),        DefaultValue("")]
        public string Email
        {
            get => email;
            set => this.RaiseAndSetIfChanged(ref email, value);
        }

        [JsonProperty("Username",       DefaultValueHandling = DefaultValueHandling.Ignore),        DefaultValue("")]
        public string Username 
        {
            get => username;
            set => this.RaiseAndSetIfChanged(ref username, value);
        }

        [JsonProperty("Password",       DefaultValueHandling = DefaultValueHandling.Ignore),        DefaultValue("")]
        public string Password
        {
            get => password;
            set => this.RaiseAndSetIfChanged(ref password, value);
        }

        [JsonProperty("Website",        DefaultValueHandling = DefaultValueHandling.Ignore),        DefaultValue("")]
        public string Website
        {
            get => website;
            set => this.RaiseAndSetIfChanged(ref website, value);
        }

        [JsonProperty("Notes", DefaultValueHandling = DefaultValueHandling.Ignore), DefaultValue("")]
        public string Notes
        {
            get => notes;
            set => this.RaiseAndSetIfChanged(ref notes, value);
        }

        #endregion
    }
}
