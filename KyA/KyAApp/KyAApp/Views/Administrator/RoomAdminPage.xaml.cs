using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class RoomAdminPage : ContentPage
    {
        public RoomAdminPage()
        {
            InitializeComponent();
            this.BindingContext = new RoomAdminPageViewModel();
            
        }
    }
}
