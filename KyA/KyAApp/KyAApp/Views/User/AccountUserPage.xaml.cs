using System;
using System.Collections.Generic;
using KyAApp.ViewModels.User;
using Xamarin.Forms;

namespace KyAApp.Views.User
{
    public partial class AccountUserPage : ContentPage
    {
        public AccountUserPage()
        {
            InitializeComponent();
            this.BindingContext = new AccountUserPageViewModel();
        }
    }
}
