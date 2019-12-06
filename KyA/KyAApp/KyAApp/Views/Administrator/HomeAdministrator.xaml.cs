using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class HomeAdministrator : ContentPage
    {
        public HomeAdministrator()
        {
            InitializeComponent();
            this.BindingContext = new HomeAdministratorViewModel();
        }
    }
}
