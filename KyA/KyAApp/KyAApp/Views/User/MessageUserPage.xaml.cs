using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KyAApp.Views.User
{
    public partial class MessageUserPage : ContentPage
    {
        public MessageUserPage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.User.MessageUserPageViewModel();
        }
    }
}
