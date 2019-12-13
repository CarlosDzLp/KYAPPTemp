using System;
using KyAApp.Models.MonthlyPayment;
using KyAApp.Models.Surcharges;
using KyAApp.Service;
using System.Linq;
using System.Collections.ObjectModel;
using KyAApp.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System.Windows.Input;
using Xamarin.Forms;
using KyAApp.Models;

namespace KyAApp.ViewModels.Administrator
{
    public class SurchargesDetailPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private MonthlyPaymentModel _monthly;
        public MonthlyPaymentModel Monthly { get { return _monthly; } set { SetProperty(ref _monthly, value); } }
        private ObservableCollection<SurchargesModel> _listSurchargeDetail;
        public ObservableCollection<SurchargesModel>ListSurchargeDetail
        {
            get { return _listSurchargeDetail; }
            set { SetProperty(ref _listSurchargeDetail, value); }
        }
        #endregion

        #region Constructor
        public SurchargesDetailPageViewModel(MonthlyPaymentModel mon)
        {
            this.Monthly = mon;
            LoadSurchargesDetail();
            DeleteSurchargeCommand = new Command<SurchargesModel>(DeleteSurchargeCommandExecuted);
        }
        #endregion

        #region Methods
        private async void LoadSurchargesDetail()
        {
            try
            {
                ListSurchargeDetail = new ObservableCollection<SurchargesModel>();
                ListSurchargeDetail.Clear();
                var response = await client.Get<ListSurchargesModel>($"surcharges/selsurcharges?IdOwner={Monthly.IdOwner}");
                if(response != null)
                {
                    if(response.Result != null && response.Count > 0)
                    {
                        foreach(var item in response.Result.Where(c=>c.IdMothly==Monthly.IdMonthly))
                        {
                            ListSurchargeDetail.Add(item);
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Command
        public ICommand DeleteSurchargeCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void DeleteSurchargeCommandExecuted(SurchargesModel obj)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Eliminar", $"Desea eliminar el recargo: {obj.Description}", "Si", "No");
            if (answer)
            {
                var response = await client.Delete<ListResponse>($"surcharges/delsurcharges?IdSurcharges={obj.IdSurcharges}");
                if (response != null)
                {
                    if (response.Result && response.Count > 0)
                    {
                        SnackSucces("Se elimino correctamente", "KYA", Helpers.TypeSnackBar.Top);
                        LoadSurchargesDetail();
                        MessagingCenter.Send<SurchargesDetailPageViewModel>(this, "loadsurcharges");
                    }
                    else
                    {
                        SnackError(response.Message, "Error", Helpers.TypeSnackBar.Top);
                    }
                }
                else
                {
                    SnackError("Hubo un error intentelo mas tarde", "Error", Helpers.TypeSnackBar.Top);
                }
            }
        }
        #endregion

        protected async override void ExitCommanExecuted()
        {
            await NavigationPopAsync();
        }

    }
}
