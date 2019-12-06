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
            var result = ImageSource.FromStream(
                () => new MemoryStream(use.Icon));
            img.Source = (result==null)?"user":result;
            name.Text = use.Name;
            address.Text = use.Address;
            phone.Text = use.Phone;
            user.Text = Encrypt.DeCrypt(use.User);
            datecreated.Text = use.DateCreated.ToString();
        }
    }
}
