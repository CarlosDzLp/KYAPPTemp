using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class RoomServiceAdminPage : ContentPage
    {
        public RoomServiceAdminPage()
        {
            InitializeComponent();
            this.BindingContext = new RoomServiceAdminPageViewModel();
        }
    }
}
