using System;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.ObjectModel;
using KyAApp.Models.Owner;
using KyAApp.Service;
using KyAApp.Models;
using KyAApp.Models.Rooms;
using KyAApp.DataBase;
using Rg.Plugins.Popup.Services;

namespace KyAApp.ViewModels.Administrator.PopupPage
{
    public class AddRoomAdminPopupViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();

        #region Properties
        private ObservableCollection<OwnerModel> _listOwner;
        public ObservableCollection<OwnerModel> ListOwner
        {
            get { return _listOwner; }
            set { SetProperty(ref _listOwner, value); }
        }
        public OwnerModel _selectedOwner;
        public OwnerModel SelectedOwner
        {
            get { return _selectedOwner; }
            set { SetProperty(ref _selectedOwner, value); }
        }
        public string _room;
        public string Room
        {
            get { return _room; }
            set { SetProperty(ref _room, value); }
        }
        public string _price;
        public string Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
        #endregion

        #region Constructor
        public AddRoomAdminPopupViewModel()
        {
            LoadOwner();
            AddRoomCommand = new Command(AddRoomCommandExecuted);
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
                            var img = (item.IconString == null || item.IconString == string.Empty) ? "user" : "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + item.IconString;
                            item.IconString = img;
                            ListOwner.Add(item);
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
        public ICommand AddRoomCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void AddRoomCommandExecuted()
        {
            if(SelectedOwner != null)
            {
                if(!string.IsNullOrWhiteSpace(Room))
                {
                    if(!string.IsNullOrWhiteSpace(Price))
                    {
                        var admin = DbContext.Instance.GetAdministrator();
                        var room = new RoomsModel();
                        room.IdAdmin = admin.IdAdmin;
                        room.IdOwner = SelectedOwner.IdOwner;
                        room.Name = Room;
                        room.Price = Convert.ToDouble(Price);
                        var response = await client.Post<ListResponse, RoomsModel>(room, "room/insroom");
                        if(response != null)
                        {
                            if(response.Result && response.Count > 0)
                            {
                                await PopupNavigation.Instance.PopAllAsync();
                                MessagingCenter.Send<AddRoomAdminPopupViewModel>(this, "loadroom");
                                SnackSucces("Se registro correctamente", "KyA", Helpers.TypeSnackBar.Top);
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
                    else
                    {
                        SnackError("Ingrese un precio", "Error", Helpers.TypeSnackBar.Top);
                    }
                }
                else
                {
                    SnackError("Ingrese un nombre de cuarto", "Error", Helpers.TypeSnackBar.Top);
                }
            }
            else
            {
                SnackError("seleccione un propietario", "Error", Helpers.TypeSnackBar.Top);
            }
        }
        #endregion

    }
}
