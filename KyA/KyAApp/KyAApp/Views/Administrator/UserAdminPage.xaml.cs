using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class UserAdminPage : ContentPage
    {
        public UserAdminPage()
        {
            InitializeComponent();
            this.BindingContext = new UserAdminPageViewModel();
        }
    }
}
