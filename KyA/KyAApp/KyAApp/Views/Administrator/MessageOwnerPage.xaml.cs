using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class MessageOwnerPage : ContentPage
    {
        public MessageOwnerPage(Models.Owner.OwnerModel obj)
        {
            InitializeComponent();
            txtname.Text = obj.Name;
            this.BindingContext = new MessageOwnerPageViewModel(obj);
        }
    }
}
