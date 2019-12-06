using System;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using KyAApp.Views.Administrator.Popup;
using KyAApp.Service;
using KyAApp.Models.Owner;
using Newtonsoft.Json.Linq;
using KyAApp.Models.Service;
using System.Collections.ObjectModel;
using KyAApp.DataBase;
using KyAApp.Models;

namespace KyAApp.ViewModels.Administrator
{
    public class ServiceAdminPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private ObservableCollection<ServiceModel> _listService;
        public ObservableCollection<ServiceModel>ListService
        {
            get { return _listService; }
            set { SetProperty(ref _listService, value); }
        }
        #endregion

        #region Constructor
        public ServiceAdminPageViewModel()
        {
            AddServiceCommand = new Command(AddServiceCommandExecuted);
            LoadService();
            MessagingCenter.Subscribe<AddServiceAdminPopup>(this, "addservice", (e) => { LoadService(); });
            UpdateServiceCommand = new Command<ServiceModel>(UpdateServiceCommandExecuted);
            DeleteServiceCommand = new Command<ServiceModel>(DeleteServiceCommandExecuted);
        }
        #endregion

        #region Methods
        private async void LoadService()
        {
            try
            {
                ListService = new ObservableCollection<ServiceModel>();
                ListService.Clear();
                var response = await client.Get<ListOwnerModel>($"administrator/selowner?status={true}");
                if (response != null)
                {
                    if (response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result)
                        {
                            var responseService = await client.Get<ListServiceModel>($"service/selservice?IdOwner={item.IdOwner}"); 
                            if(responseService != null)
                            {
                                if(responseService.Result != null && responseService.Count > 0)
                                {
                                    foreach(var itemService in responseService.Result)
                                    {
                                        itemService.NameOwner = item.Name;
                                        itemService.IconString = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + itemService.IconString;
                                        ListService.Add(itemService);
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
        #endregion

        #region Command
        public ICommand AddServiceCommand { get; set; }
        public ICommand DeleteServiceCommand { get; set; }
        public ICommand UpdateServiceCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void AddServiceCommandExecuted()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new Views.Administrator.Popup.AddServiceAdminPopup());
            }
            catch(Exception ex)
            {

            }
        }

        private async void DeleteServiceCommandExecuted(ServiceModel obj)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Eliminar", $"Desea eliminar el servicio: {obj.Name}", "Si", "No");
            if (answer)
            {
                var response = await client.Delete<ListResponse>($"service/delservice?IdService={obj.IdService}&IdOwner={obj.IdOwner}");
                if (response != null)
                {
                    if (response.Result && response.Count > 0)
                    {
                        SnackSucces("Se elimino correctamente", "KyA", Helpers.TypeSnackBar.Top);
                        LoadService();
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

        private void UpdateServiceCommandExecuted(ServiceModel obj)
        {
            
        }

        #endregion
    }
}
