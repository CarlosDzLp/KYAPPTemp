using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class AssignmentRoomUserPage : ContentPage
    {
        public AssignmentRoomUserPage()
        {
            InitializeComponent();
            this.BindingContext = new AssignmentRoomUserPageViewModel();
        }
    }
}
