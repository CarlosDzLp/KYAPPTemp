using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Owner;
using Xamarin.Forms;

namespace KyAApp.Views.Owner
{
    public partial class AccountOwnerPage : ContentPage
    {
        public AccountOwnerPage()
        {
            InitializeComponent();
            this.BindingContext = new AccountOwnerPageViewModel();
        }
    }
}
