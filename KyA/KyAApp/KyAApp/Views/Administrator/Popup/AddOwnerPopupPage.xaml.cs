using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator.PopupPage;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator.Popup
{
    public partial class AddOwnerPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public AddOwnerPopupPage()
        {
            InitializeComponent();
            this.BindingContext = new AddOwnerPopupPageViewModel();
        }
    }
}
