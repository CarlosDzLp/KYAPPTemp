using System;
using System.Linq;
using System.Collections.ObjectModel;
using KyAApp.DataBase;
using KyAApp.Models.MonthlyPayment;
using KyAApp.Models.Surcharges;
using KyAApp.Service;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;

namespace KyAApp.Views.User
{
    public partial class MyPaymentsPage : ContentPage
    {
        public MyPaymentsPage()
        {
            InitializeComponent();
            this.BindingContext = new MyPaymentsPageViewModel();
        }
    }
    public class MyPaymentsPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private ObservableCollection<MonthlyPaymentModel> _listMonthly;
        public ObservableCollection<MonthlyPaymentModel> ListMonthly
        {
            get { return _listMonthly; }
            set { SetProperty(ref _listMonthly, value); }
        }
        #endregion

        #region Constructor
        public MyPaymentsPageViewModel()
        {
            LoadPayment();
        }
        #endregion

        #region Methods
        private async void LoadPayment()
        {
            try
            {
                var user = DbContext.Instance.GetUser();
                var surcharges = await client.Get<ListSurchargesModel>($"surcharges/selsurcharges?IdOwner={user.IdOwner}");
                ListMonthly = new ObservableCollection<MonthlyPaymentModel>();
                ListMonthly.Clear();
                
                var response = await client.Get<ListMonthlyPaymentModel>($"monthlypayments/selmonthlypaymentshistoryuser?userID={user.UserId}");
                if (response != null)
                {
                    if (response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result)
                        {
                            if(surcharges != null)
                            {
                                if(surcharges.Result != null && surcharges.Count > 0)
                                {
                                    var filter = surcharges.Result.Where(c => c.IdMothly == item.IdMonthly && c.StatusSurcharges == true);
                                    foreach(var it in filter)
                                    {
                                        item.Surcharges += it.Description + ",";
                                    }
                                }
                            }
                            if (item.StatusActivePay.Value)
                            {
                                item.ColorMothly = Color.Red;
                                item.StringStatus = "Mensualidad actual";
                            }
                            else
                            {
                                item.ColorMothly = Color.Black;
                                item.StringStatus = "Mensualidad Pagada";
                            }
                            ListMonthly.Add(item);
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

        #endregion

        #region CommandExecuted

        #endregion
    }
}
