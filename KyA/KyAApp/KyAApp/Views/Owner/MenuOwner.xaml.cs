using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Owner;
using Xamarin.Forms;

namespace KyAApp.Views.Owner
{
    public partial class MenuOwner : ContentPage
    {
        public MenuOwner()
        {
            InitializeComponent();
            this.BindingContext = new MenuOwnerViewModel();
        }
    }
}
