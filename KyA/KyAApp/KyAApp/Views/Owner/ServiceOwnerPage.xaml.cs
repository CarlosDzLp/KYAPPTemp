using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Owner;
using Xamarin.Forms;

namespace KyAApp.Views.Owner
{
    public partial class ServiceOwnerPage : ContentPage
    {
        public ServiceOwnerPage()
        {
            InitializeComponent();
            this.BindingContext = new ServiceOwnerPageViewModel();
        }
    }
}
