using System;
using Xamarin.Forms;
using System.Windows.Input;
using KyAApp.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using KyAApp.Views.Administrator.Popup;
using KyAApp.ViewModels.Administrator.PopupPage;
using KyAApp.Service;
using System.Collections.ObjectModel;
using KyAApp.Models.Owner;
using KyAApp.Models.Rooms;
using KyAApp.DataBase;
using KyAApp.Models;

namespace KyAApp.ViewModels.Administrator
{
    public class RoomAdminPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private ObservableCollection<RoomsModel> _listRoom;
        public ObservableCollection<RoomsModel>ListRoom
        {
            get { return _listRoom; }
            set { SetProperty(ref _listRoom, value); }
        }
        #endregion

        #region Constructor
        public RoomAdminPageViewModel()
        {
            AddRoomCommand = new Command(AddRoomCommandExecuted);
            LoadOwner();
            DeleteRoomCommand = new Command<RoomsModel>(DeleteRoomCommandExecuted);
            SelectedRoom = new Command<RoomsModel>(SelectedRoomExecuted);
            MessagingCenter.Subscribe<AddRoomAdminPopupViewModel>(this, "loadroom", (e) => { LoadOwner(); });
            MessagingCenter.Subscribe<UpdateRoomAdminPopup>(this, "loadupdateroom", (e) => { LoadOwner(); });
        }

        
        #endregion

        #region Methods
        private async void LoadOwner()
        {
            try
            {
                IsBussy = true;
                ListRoom = new ObservableCollection<RoomsModel>();
                ListRoom.Clear();
                var response = await client.Get<ListOwnerModel>($"administrator/selowner?status={true}");
                if (response != null)
                {
                    if (response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result)
                        {
                            var responseRoom = await client.Get<ListRoomsModel>($"room/selroom?IdOwner={item.IdOwner}");
                            if(responseRoom != null)
                            {
                                if(responseRoom.Result != null && responseRoom.Count > 0)
                                {
                                    foreach(var itemRoom in responseRoom.Result)
                                    {
                                        itemRoom.NameOwner = item.Name;
                                        if (itemRoom.TypeStatusRoom == 0)
                                        {
                                            itemRoom.ColorRoom = Color.Black;
                                            itemRoom.StatusRoom = "Libre";
                                        }
                                        else if(itemRoom.TypeStatusRoom == 1)
                                        {
                                            itemRoom.ColorRoom = Color.Red;
                                            itemRoom.StatusRoom = "Ocupado";
                                        }
                                        ListRoom.Add(itemRoom);
                                    }
                                }
                            }
                        }
                    }
                }
                IsBussy = false;
            }
            catch (Exception ex)
            {
                IsBussy = false;
            }
        }
        #endregion

        #region Command
        public ICommand AddRoomCommand { get; set; }
        public ICommand DeleteRoomCommand { get; set; }
        public ICommand SelectedRoom { get; set; }
        #endregion

        #region CommandExecuted
        private async void AddRoomCommandExecuted()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new Views.Administrator.Popup.AddRoomAdminPopup());
            }
            catch(Exception ex)
            {

            }
        }

        private async void DeleteRoomCommandExecuted(RoomsModel obj)
        {
            if(obj.TypeStatusRoom == 0 || obj.TypeStatusRoom == 2)
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Eliminar", $"Desea eliminar el cuarto: {obj.Name}", "Si", "No");
                if (answer)
                {
                    var response = await client.Delete<ListResponse>($"room/delroom?IdRoom={obj.IdRoom}&IdOwner={obj.IdOwner}");
                    if (response != null)
                    {
                        if (response.Result && response.Count > 0)
                        {
                            SnackSucces("Se elimino correctamente", "KYA", Helpers.TypeSnackBar.Top);
                            LoadOwner();
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
            else
            {
                SnackError("No puedes eliminar un cuarto que esta ocupado", "Error", Helpers.TypeSnackBar.Top);
            }
        }

        private async void SelectedRoomExecuted(RoomsModel obj)
        {
            await PopupNavigation.Instance.PushAsync(new Views.Administrator.Popup.UpdateRoomAdminPopup(obj));
        }
        #endregion
    }
}
