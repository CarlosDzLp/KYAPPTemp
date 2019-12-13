using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class Message : TabbedPage
    {
        public Message()
        {
            InitializeComponent();
            this.BindingContext = new MessageViewModel();
        }
    }
}
