using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KyAApp.Controls;
using KyAApp.DataBase;
using KyAApp.Helpers;
using KyAApp.Models;
using KyAApp.Models.Owner;
using KyAApp.Models.Rooms;
using KyAApp.Models.Service;
using KyAApp.Service;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator.Popup
{
    public partial class AddRoomServicePopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        ServiceClient client = new ServiceClient();
        public ObservableCollection<OwnerModel> ListOwner { get; set; }
        public OwnerModel Owner { get; set; }

        public ObservableCollection<RoomsModel> ListRoom{ get; set; }
        public RoomsModel Room { get; set; }

        public ObservableCollection<ServiceModel> ListService { get; set;  }
        public ServiceModel Service { get; set; }
        public AddRoomServicePopup()
        {
            InitializeComponent();
            LoadOwner();

            //Click button
            btnsaverroomservice.Clicked += Btnsaverroomservice_Clicked;
            //

            //SelectedItemPickers
            pickerowner.SelectedIndexChanged += Pickerowner_SelectedIndexChanged;
            pickerroom.SelectedIndexChanged += Pickerroom_SelectedIndexChanged;
            pickerservice.SelectedIndexChanged += Pickerservice_SelectedIndexChanged;
            //


            //Isvisible
            pickerservice.IsVisible = false;
            pickerroom.IsVisible = false;
            //
        }

        private void Pickerservice_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as CustomPicker;
            Service = (ServiceModel)picker.SelectedItem;
        }

        private void Pickerroom_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as CustomPicker;
            Room = (RoomsModel)picker.SelectedItem;

        }

        private void Pickerowner_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as CustomPicker;
            Owner = (OwnerModel)picker.SelectedItem;
            if(Owner != null)
            {
                LoadService();
                LoadRoom();
            }
            
        }
        private async void LoadService()
        {
            try
            {
                pickerservice.IsVisible = true;
                ListService = new ObservableCollection<ServiceModel>();
                ListService.Clear();
                var responseService = await client.Get<ListServiceModel>($"service/selservice?IdOwner={Owner.IdOwner}");
                if (responseService != null)
                {
                    if (responseService.Result != null && responseService.Count > 0)
                    {
                        foreach (var itemService in responseService.Result)
                        {
                            ListService.Add(itemService);
                        }

                    }
                }
                pickerservice.ItemsSource = ListService;
            }
            catch (Exception ex)
            {

            }
        }

        private async void LoadRoom()
        {
            try
            {
                ListRoom = new ObservableCollection<RoomsModel>();
                ListRoom.Clear();
                pickerroom.IsVisible = true;
                var responseRoom = await client.Get<ListRoomsModel>($"room/selroom?IdOwner={Owner.IdOwner}");
                if (responseRoom != null)
                {
                    if (responseRoom.Result != null && responseRoom.Count > 0)
                    {
                        foreach (var itemRoom in responseRoom.Result)
                        {
                            ListRoom.Add(itemRoom);
                        }
                    }
                }
                pickerroom.ItemsSource = ListRoom;
            }
            catch (Exception ex)
            {

            }
        }


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
                pickerowner.ItemsSource = ListOwner;
            }
            catch(Exception ex)
            {

            }
        }

        private async void imgclose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void Btnsaverroomservice_Clicked(object sender, EventArgs e)
        {
            var snack = DependencyService.Get<IDialogs>();
            if(Owner != null)
            {
                if (Room != null)
                {
                    if (Service != null)
                    {
                        var admin = DbContext.Instance.GetAdministrator();
                        //"IdOwner": "c6302649-66d6-46d9-a83c-8601613083f2",
                        //"IdAdmin": "ed983b6a-a1fc-42b8-80aa-3e581fa18b16",
                        //"IdService": "711d8abb-c6de-4797-a90c-e9cea7bdf295",
                        //"IdRoom": "ac0b6ebc-537d-486b-9f6e-e7c802c41af0"
                        var rs = new RoomServiceModel();
                        rs.IdAdmin = admin.IdAdmin;
                        rs.IdOwner = Owner.IdOwner;
                        rs.IdRoom = Room.IdRoom;
                        rs.IdService = Service.IdService;
                        var response = await client.Post<ListResponse, RoomServiceModel>(rs, "roomservice/insroomservice");
                        if(response != null)
                        {
                            if(response.Result && response.Count > 0)
                            {
                                await PopupNavigation.Instance.PopAllAsync();
                                MessagingCenter.Send<AddRoomServicePopup>(this, "loadroomservice");
                            }
                            else
                            {
                                await snack.SnackBarError(response.Message, "Error", TypeSnackBar.Top);
                            }
                        }
                        else
                        {
                            await snack.SnackBarError("Hubo un error intentelo mas tarde", "Error", TypeSnackBar.Top);
                        }
                        
                    }
                    else
                    {
                        await snack.SnackBarError("Ingrese un Servicio", "Error", TypeSnackBar.Top);
                    }
                }
                else
                {
                    await snack.SnackBarError("Ingrese un Cuarto", "Error", TypeSnackBar.Top);
                }
            }
            else
            {
                await snack.SnackBarError("Ingrese un propietario", "Error", TypeSnackBar.Top);
            }
        }
    }
}
