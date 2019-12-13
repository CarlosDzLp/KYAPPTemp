using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class MessageUserPage : ContentPage
    {
        public MessageUserPage(Models.User.UserModel obj)
        {
            InitializeComponent();
            txtname.Text = obj.Name;
            this.BindingContext = new MessageUserPageViewModel(obj);
        }
    }
}
