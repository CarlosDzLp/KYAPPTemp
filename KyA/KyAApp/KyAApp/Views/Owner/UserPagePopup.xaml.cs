using System;
using System.Collections.Generic;
using System.IO;
using KyAApp.Helpers;
using Xamarin.Forms;

namespace KyAApp.Views.Owner
{
    public partial class UserPagePopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public UserPagePopup(Models.User.UserModel use)
        {
            InitializeComponent();
            img.Source = (string.IsNullOrEmpty(use.IconString)) ? "user" : "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + use.IconString;
            name.Text = use.Name;
            address.Text = use.Address;
            phone.Text = use.Phone;
            user.Text = Encrypt.DeCrypt(use.User);
            datecreated.Text = use.DateCreated.ToString();
        }
    }
}
