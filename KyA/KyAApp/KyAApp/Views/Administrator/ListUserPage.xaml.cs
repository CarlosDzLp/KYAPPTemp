using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class ListUserPage : ContentPage
    {
        public ListUserPage(Models.Owner.OwnerModel obj)
        {
            InitializeComponent();
            this.BindingContext = new ListUserPageViewModel(obj);
        }
    }
}
