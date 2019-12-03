using System;
using FormsToolkit;
using System.Collections.Generic;
using KyAApp.Helpers;
using KyAApp.ViewModels.Session;
using Xamarin.Forms;

namespace KyAApp.Views.Session
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginPageViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingService.Current.SendMessage(MessageKeys.StatusBar, true);
        }
    }
}
