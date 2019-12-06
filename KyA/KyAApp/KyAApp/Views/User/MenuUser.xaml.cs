using System;
using System.Collections.Generic;
using KyAApp.ViewModels.User;
using Xamarin.Forms;

namespace KyAApp.Views.User
{
    public partial class MenuUser : ContentPage
    {
        public MenuUser()
        {
            InitializeComponent();
            this.BindingContext = new MenuUserViewModel();
        }
    }
}
