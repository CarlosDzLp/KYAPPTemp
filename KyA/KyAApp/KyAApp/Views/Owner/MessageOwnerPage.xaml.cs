using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KyAApp.Views.Owner
{
    public partial class MessageOwnerPage : ContentPage
    {
        public MessageOwnerPage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.Owner.MessageOwnerPageViewModel();
        }
    }
}
