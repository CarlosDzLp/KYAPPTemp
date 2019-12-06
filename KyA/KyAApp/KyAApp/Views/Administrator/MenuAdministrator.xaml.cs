using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class MenuAdministrator : ContentPage
    {
        public MenuAdministrator()
        {
            InitializeComponent();
            this.BindingContext = new MenuAdministratorViewModel();
        }
    }
}
