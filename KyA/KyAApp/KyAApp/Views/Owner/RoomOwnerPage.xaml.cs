using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Owner;
using Xamarin.Forms;

namespace KyAApp.Views.Owner
{
    public partial class RoomOwnerPage : ContentPage
    {
        public RoomOwnerPage()
        {
            InitializeComponent();
            this.BindingContext = new RoomOwnerPageViewModel();
        }
    }
}
