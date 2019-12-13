using System;
using System.Collections.Generic;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class SurchargesDetailPage : ContentPage
    {
        public SurchargesDetailPage(Models.MonthlyPayment.MonthlyPaymentModel mon)
        {
            InitializeComponent();
            this.BindingContext = new SurchargesDetailPageViewModel(mon);
        }
    }
}
