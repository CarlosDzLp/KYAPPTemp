using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class ServiceAdminPage : ContentPage
    {
        public ServiceAdminPage()
        {
            InitializeComponent();
            this.BindingContext = new ServiceAdminPageViewModel();
        }
    }
}
