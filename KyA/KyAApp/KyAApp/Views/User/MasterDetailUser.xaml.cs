using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KyAApp.Views.User
{
    public partial class MasterDetailUser : MasterDetailPage
    {
        public MasterDetailUser()
        {
            InitializeComponent();
            App.Master = this;
        }
    }
}
