using System;
using System.Collections.Generic;
using KyAApp.Models.Owner;
using KyAApp.ViewModels.Administrator.PopupPage;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator.Popup
{
    public partial class AddUserAdminPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public AddUserAdminPopupPage(OwnerModel owner)
        {
            InitializeComponent();
            this.BindingContext = new AddUserAdminPopupPageViewModel(owner);
        }
    }
}
