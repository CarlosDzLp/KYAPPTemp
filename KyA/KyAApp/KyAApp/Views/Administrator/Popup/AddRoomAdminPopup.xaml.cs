using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator.PopupPage;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator.Popup
{
    public partial class AddRoomAdminPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public AddRoomAdminPopup()
        {
            InitializeComponent();
            this.BindingContext = new AddRoomAdminPopupViewModel();
        }
    }
}
