using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Owner;
using Xamarin.Forms;

namespace KyAApp.Views.Owner
{
    public partial class MisUsuariosPage : ContentPage
    {
        public MisUsuariosPage()
        {
            InitializeComponent();
            this.BindingContext = new MisUsuariosPageViewModel();
        }
    }
}
