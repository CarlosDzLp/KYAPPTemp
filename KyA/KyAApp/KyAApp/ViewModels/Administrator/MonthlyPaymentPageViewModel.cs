using System;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.ObjectModel;
using KyAApp.Models.Owner;
using KyAApp.Service;
using KyAApp.Models.User;
using KyAApp.Models;
using System.Linq;
using KyAApp.Models.Rooms;
using KyAApp.Models.MonthlyPayment;
using KyAApp.DataBase;
using Rg.Plugins.Popup.Services;
using KyAApp.Views.Administrator;

namespace KyAApp.ViewModels.Administrator
{
    public class MonthlyPaymentPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();

        #region Properties
        private ObservableCollection<OwnerModel> _listOwner;
        public ObservableCollection<OwnerModel> ListOwner
        {
            get { return _listOwner; }
            set { SetProperty(ref _listOwner, value); }
        }

        private OwnerModel _owner;
        public OwnerModel SelectedOwner
        {
            get { return _owner; }
            set
            {
                if(_owner != value)
                {
                    SetProperty(ref _owner, value);
                    OnTapOwnerSelected();
                }
                
            }
        }

        private ObservableCollection<AssignRoomUserModel> _listRoom;
        public ObservableCollection<AssignRoomUserModel> ListRoom
        {
            get { return _listRoom; }
            set { SetProperty(ref _listRoom, value); }
        }

        private AssignRoomUserModel _selectedRoom;
        public AssignRoomUserModel SelectedRoom
        {
            get { return _selectedRoom; }
            set { SetProperty(ref _selectedRoom, value); }
        }

        private DateTime? _date = DateTime.Now;
        public DateTime? Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private ObservableCollection<MonthlyPaymentModel> _listMonthly;
        public ObservableCollection<MonthlyPaymentModel> ListMonthly
        {
            get { return _listMonthly; }
            set { SetProperty(ref _listMonthly, value); }
        }        
        #endregion

        #region Constructor
        public MonthlyPaymentPageViewModel()
        {
            LoadOwner();
            LoadMonthly();
            SaveMonthlyCommand = new Command(SaveMonthlyCommandExecuted);
            ListMonthlyCommand = new Command<MonthlyPaymentModel>(ListMonthlyCommandExecuted);
            DeleteMonthlyCommand = new Command<MonthlyPaymentModel>(DeleteMonthlyCommandExecuted);
            SelectedMonthlyCommand = new Command<MonthlyPaymentModel>(SelectedMonthlyCommandExecuted);
            PayMonthlyCommand = new Command<MonthlyPaymentModel>(PayMonthlyCommandExecuted);
            MessagingCenter.Subscribe<SurchargesPage>(this, "loadMonthly", (e) => { LoadMonthly(); });
            MessagingCenter.Subscribe<SurchargesDetailPageViewModel>(this, "loadsurcharges", (e) => { LoadMonthly(); });
        }

        
        #endregion

        #region Methods
        private async void LoadOwner()
        {
            try
            {

                ListOwner = new ObservableCollection<OwnerModel>();
                ListOwner.Clear();
                var response = await client.Get<ListOwnerModel>($"administrator/selowner?status={true}");
                if (response != null)
                {
                    if (response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result)
                        {
                            ListOwner.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void LoadRoom()
        {
            try
            {
                ListRoom = new ObservableCollection<AssignRoomUserModel>();
                ListRoom.Clear();
                var responseRoom = await client.Get<ListRoomsModel>($"room/selroom?IdOwner={SelectedOwner.IdOwner}");
                if (responseRoom != null)
                {
                    if (responseRoom.Result != null && responseRoom.Count > 0)
                    {
                        foreach (var itemRoom in responseRoom.Result.Where(c=>c.TypeStatusRoom == 1))
                        {
                            var response = await client.Get<ListAssignRoomUserModel>($"assignmentroomuser/selassignroomuserxidroom?IdRoom={itemRoom.IdRoom}");
                            if(response != null)
                            {
                                if (response.Result != null && response.Count > 0)
                                {
                                    foreach(var item in response.Result)
                                    {
                                        item.NameRoom = itemRoom.Name;
                                        ListRoom.Add(item);
                                    }                                  
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void LoadMonthly()
        {
            try
            {
                IsBussy = true;
                ListMonthly = new ObservableCollection<MonthlyPaymentModel>();
                ListMonthly.Clear();
                var responseOwner = await client.Get<ListOwnerModel>($"administrator/selowner?status={true}");
                if (responseOwner != null)
                {
                    if (responseOwner.Result != null && responseOwner.Count > 0)
                    {
                        foreach (var item in responseOwner.Result)
                        {
                            var response = await client.Get<ListMonthlyPaymentModel>($"monthlypayments/selmonthlypayments?IdOwner={item.IdOwner}");
                            if(response != null)
                            {
                                if(response.Result != null && response.Count > 0)
                                {
                                    foreach(var itemMonthly in response.Result)
                                    {
                                        itemMonthly.NameOwner = item.Name;
                                        ListMonthly.Add(itemMonthly);
                                    }
                                }
                            }
                        }
                    }
                }
                IsBussy = false;
            }
            catch(Exception ex)
            {
                IsBussy = false;
            }
        }
        #endregion

        #region Command
        public ICommand SaveMonthlyCommand { get; set; }
        public ICommand ListMonthlyCommand { get; set; }
        public ICommand DeleteMonthlyCommand { get; set; }
        public ICommand SelectedMonthlyCommand { get; set; }
        public ICommand PayMonthlyCommand { get; set; }
        #endregion

        #region CommandExecuted
        private void OnTapOwnerSelected()
        {
            if(SelectedOwner != null)
            {
                LoadRoom();
            }
        }

        private async void SaveMonthlyCommandExecuted()
        {
            if(SelectedOwner !=null)
            {
                if (SelectedRoom != null)
                {
                    if (Date != null)
                    {
                        var admin = DbContext.Instance.GetAdministrator();
                        var mon = new MonthlyPaymentModel();
                        mon.DateMonthlyPayment = Date;
                        mon.IdAdmin = admin.IdAdmin;
                        mon.IdOwner = SelectedOwner.IdOwner;
                        mon.IdRoom = SelectedRoom.IdRoom;
                        mon.UserId = SelectedRoom.UserId;
                        var responseMon = await client.Post<ListResponse, MonthlyPaymentModel>(mon, "monthlypayments/insmonthlypayments");
                        if (responseMon != null)
                        {
                            if (responseMon.Result && responseMon.Count > 0)
                            {
                                SnackSucces("Se registro correctamente", "KyA", Helpers.TypeSnackBar.Top);
                                LoadMonthly();
                            }
                            else
                            {
                                SnackError(responseMon.Message, "Error", Helpers.TypeSnackBar.Top);
                            }
                        }
                        else
                        {
                            SnackError("Hubo un error intentelo mas tarde", "Error", Helpers.TypeSnackBar.Top);
                        }
                    }
                    else
                    {
                        SnackError("Ingrese una fecha", "Error", Helpers.TypeSnackBar.Top);
                    }
                }
                else
                {
                    SnackError("Ingrese un cuarto", "Error", Helpers.TypeSnackBar.Top);
                }
            }
            else
            {
                SnackError("Ingrese un propietario", "Error", Helpers.TypeSnackBar.Top);
            }
        }

        private async void ListMonthlyCommandExecuted(MonthlyPaymentModel mon)
        {
            await NavigationPushModalAsync(new Views.Administrator.SurchargesDetailPage(mon));
        }

        private async void SelectedMonthlyCommandExecuted(MonthlyPaymentModel obj)
        {
            await PopupNavigation.Instance.PushAsync(new Views.Administrator.SurchargesPage(obj));
        }

        private async void DeleteMonthlyCommandExecuted(MonthlyPaymentModel mon)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Eliminar", $"Desea eliminar la mensualidad: {mon.DateMonthlyPayment.Value.ToString("dd/MM/yyyy")}", "Si", "No");
            if (answer)
            {
                var response = await client.Delete<ListResponse>($"monthlypayments/delmonthlypayments?IdMonthlyPayment={mon.IdMonthly}");
                if (response != null)
                {
                    if (response.Result && response.Count > 0)
                    {
                        SnackSucces("Se elimino correctamente", "KYA", Helpers.TypeSnackBar.Top);
                        LoadMonthly();
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

        private async void PayMonthlyCommandExecuted(MonthlyPaymentModel obj)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Pagos", $"Desea cambiar el estatus ha pagado de la mensualidad: {obj.DateMonthlyPayment.Value.ToString("dd/MM/yyyy")} del cuarto: {obj.NameRoom}", "Si", "No");
            if (answer)
            {
                var pay = new Payment();
                pay.IdMonthly = obj.IdMonthly;
                var response = await client.Put<ListResponse,Payment>(pay, $"monthlypayments/paymonthlypayments");
                if (response != null)
                {
                    if (response.Result && response.Count > 0)
                    {
                        SnackSucces("Se hizo el pago correctamente", "KYA", Helpers.TypeSnackBar.Top);
                        LoadMonthly();
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
    }
}
