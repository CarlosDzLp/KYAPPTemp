using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Owner;
using Xamarin.Forms;

namespace KyAApp.Views.Owner
{
    public partial class RoomServiceOwnerPage : ContentPage
    {
        public RoomServiceOwnerPage()
        {
            InitializeComponent();
            this.BindingContext = new RoomServiceOwnerPageViewModel();
        }
    }
}
