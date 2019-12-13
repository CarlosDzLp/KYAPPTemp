using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;

namespace KyAApp.Models.User
{
    public class UserModel : INotifyPropertyChanged
    {
        [JsonIgnore]
        private string _user;
        [JsonIgnore]
        private string _password;
        [JsonProperty("IdOwner")]
        public Guid? IdOwner { get; set; }

        [JsonProperty("IdAdmin")]
        public Guid? IdAdmin { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("User")]
        public string User
        { get { return _user; } set { SetProperty(ref _user, value); } }

        [JsonProperty("Password")]
        public string Password { get { return _password; } set { SetProperty(ref _password, value); } }

        [JsonProperty("Icon")]
        public byte[] Icon { get; set; }

        [JsonProperty("IconString")]
        public string IconString { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty("DateModified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty("Status")]
        public bool? Status { get; set; }

        [JsonProperty("UserId"),PrimaryKey]
        public Guid? UserId { get; set; }

        [JsonProperty("StatusAssignUser")]
        public int StatusAssignUser { get; set; }

        [JsonIgnore]
        public string StatusAssign { get; set; }

        [JsonIgnore,Ignore]
        public Color ColorStatusAssign { get; set; }

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

    public class ListUserModel
    {
        [JsonProperty("Result")]
        public List<UserModel> Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
    public class User
    {
        [JsonProperty("Result")]
        public UserModel Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
    
}
