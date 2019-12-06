using System;
using System.Collections.Generic;
using KyAApp.ViewModels.User;
using Xamarin.Forms;

namespace KyAApp.Views.User
{
    public partial class HomeUser : ContentPage
    {
        public HomeUser()
        {
            InitializeComponent();
            this.BindingContext = new HomeUserViewModel();
        }
    }
}
