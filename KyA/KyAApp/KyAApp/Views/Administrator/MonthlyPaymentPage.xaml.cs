using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using KyAApp.Controls;
using KyAApp.ViewModels.Administrator;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class MonthlyPaymentPage : ContentPage
    {
        public MonthlyPaymentPage()
        {
            InitializeComponent();
            this.BindingContext = new MonthlyPaymentPageViewModel();
        }
        private ICommand _tapCommand;
        public ICommand TapCommand => _tapCommand ?? (_tapCommand = new Command(p =>
        {
            //DisplayAlert("Tapped", p.ToString(), "Ok");
        }));

        protected override void OnAppearing()
        {
            base.OnAppearing();
            expandableView.StatusChanged += OnStatusChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            expandableView.StatusChanged -= OnStatusChanged;
        }

        private async void OnStatusChanged(object sender, StatusChangedEventArgs e)
        {
            var rotation = 0;
            switch (e.Status)
            {
                case ExpandStatus.Collapsing:
                    break;
                case ExpandStatus.Expanding:
                    rotation = 180;
                    break;
                default:
                    return;
            }

            await arrow.RotateTo(rotation, 200, Easing.CubicInOut);
        }
    }
}
