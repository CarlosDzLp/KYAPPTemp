using System;
using System.Collections.Generic;
using FormsToolkit;
using KyAApp.Helpers;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class MasterDetailAdministrator : MasterDetailPage
    {
        public MasterDetailAdministrator()
        {
            InitializeComponent();
            App.Master = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingService.Current.SendMessage(MessageKeys.StatusBar, false);
        }
    }
}
