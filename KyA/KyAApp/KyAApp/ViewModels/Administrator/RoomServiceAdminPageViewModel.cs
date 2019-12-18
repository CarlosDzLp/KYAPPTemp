using System;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using KyAApp.Views.Administrator.Popup;
using KyAApp.Service;
using KyAApp.Models.Owner;
using KyAApp.Models;
using System.Collections.ObjectModel;

namespace KyAApp.ViewModels.Administrator
{
    public class RoomServiceAdminPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private ObservableCollection<RoomServiceModel> _listRoomService;
        public ObservableCollection<RoomServiceModel>ListRoomService
        {
            get { return _listRoomService; }
            set { SetProperty(ref _listRoomService, value); }
        }
        #endregion

        #region Constructor
        public RoomServiceAdminPageViewModel()
        {
            AddRoomServiceCommand = new Command(AddRoomServiceCommandExecuted);
            DeleteRoomServiceCommand = new Command<RoomServiceModel>(DeleteRoomServiceCommandExecuted);
            MessagingCenter.Subscribe<AddRoomServicePopup>(this, "loadroomservice", (e) => { LoadRoomService(); });
            LoadRoomService();
        }

        
        #endregion

        #region Methods
        private async void LoadRoomService()
        {
            try
            {
                IsBussy = true;
                ListRoomService = new ObservableCollection<RoomServiceModel>();
                ListRoomService.Clear();
                var response = await client.Get<ListOwnerModel>($"administrator/selowner?status={true}");
                if (response != null)
                {
                    if (response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result)
                        {
                            var responseRoomService = await client.Get<ListRoomServiceModel>($"roomservice/selroomservice?IdOwner={item.IdOwner}");
                            if (responseRoomService != null)
                            {
                                if (responseRoomService.Result != null && responseRoomService.Count > 0)
                                {
                                    foreach (var itemRoomService in responseRoomService.Result)
                                    {
                                        itemRoomService.NameOwner = item.Name;
                                        ListRoomService.Add(itemRoomService);
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
        public ICommand AddRoomServiceCommand { get; set; }
        public ICommand DeleteRoomServiceCommand { get; set; }
        #endregion

        #region CommmandExecuted
        private async void AddRoomServiceCommandExecuted()
        {
            await PopupNavigation.Instance.PushAsync(new Views.Administrator.Popup.AddRoomServicePopup());
        }
        private async void DeleteRoomServiceCommandExecuted(RoomServiceModel obj)
        {
            try
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Eliminar", $"Desea eliminar el servicio de cuarto: {obj.NameRoom},{obj.NameService}", "Si", "No");
                if (answer)
                {
                    var response = await client.Delete<ListResponse>($"roomservice/delroomservice?IdRoomService={obj.IdRoomService}&IdOwner={obj.IdOwner}");
                    if (response != null)
                    {
                        if (response.Result && response.Count > 0)
                        {
                            SnackSucces("Se elimino correctamente", "KyA", Helpers.TypeSnackBar.Top);
                            LoadRoomService();
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
            catch(Exception ex)
            {

            }
        }
        #endregion

    }
}

