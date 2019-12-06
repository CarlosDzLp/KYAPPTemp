using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KyAApp.Views.Owner
{
    public partial class MasterDetailOwner : MasterDetailPage
    {
        public MasterDetailOwner()
        {
            InitializeComponent();
            App.Master = this;
        }
    }
}
