using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SQLite;

namespace KyAApp.Models.Owner
{
    public class OwnerModel : INotifyPropertyChanged
    {
        [JsonIgnore]
        private string _user;
        [JsonIgnore]
        private string _password;

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("IdOwner"),PrimaryKey]
        public Guid? IdOwner { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty("Icon")]
        public byte[] Icon { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("DateModified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty("User")]
        public string User { get { return _user; } set { SetProperty(ref _user, value); } }

        [JsonProperty("Status")]
        public bool? Status { get; set; }

        [JsonProperty("Password")]
        public string Password { get { return _password; } set { SetProperty(ref _password, value); } }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("IdAdmin")]
        public Guid? IdAdmin { get; set; }

        [JsonProperty("IconString")]
        public string IconString { get; set; }

        #region NotifyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(field, value)) { return false; }

            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
    public class ListOwnerModel
    {
        [JsonProperty("Result")]
        public List<OwnerModel> Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }

    public class Owner
    {
        [JsonProperty("Result")]
        public OwnerModel Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
