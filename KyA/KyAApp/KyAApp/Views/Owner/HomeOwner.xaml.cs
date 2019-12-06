using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Owner;
using KyAApp.ViewModels.User;
using Xamarin.Forms;

namespace KyAApp.Views.Owner
{
    public partial class HomeOwner : ContentPage
    {
        public HomeOwner()
        {
            InitializeComponent();
            this.BindingContext = new HomeOwnerViewModel();
        }
    }
}
