using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KyAApp.Controls;
using KyAApp.DataBase;
using KyAApp.Helpers;
using KyAApp.Models;
using KyAApp.Models.AssignRoomUser;
using KyAApp.Models.Owner;
using KyAApp.Models.Rooms;
using KyAApp.Models.User;
using KyAApp.Service;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator.Popup
{
    public partial class AddAssignmentRoomUserPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        ServiceClient client = new ServiceClient();
        public ObservableCollection<OwnerModel> ListOwner { get; set; }
        public OwnerModel Owner { get; set; }

        public ObservableCollection<RoomsModel> ListRoom { get; set; }
        public RoomsModel Room { get; set; }

        public ObservableCollection<UserModel> ListUser { get; set; }
        public UserModel User { get; set; }
        public AddAssignmentRoomUserPopup()
        {
            InitializeComponent();
            LoadPickerOwner();
            pickerroom.IsVisible = false;
            pickeruser.IsVisible = false;
            pickerowner.SelectedIndexChanged += Pickerowner_SelectedIndexChanged;
            pickerroom.SelectedIndexChanged += Pickerroom_SelectedIndexChanged;
            pickeruser.SelectedIndexChanged += Pickeruser_SelectedIndexChanged;

            btnsave.Clicked += Btnsave_Clicked;
        }

        private async void Btnsave_Clicked(object sender, EventArgs e)
        {
            try
            {
                var snack = DependencyService.Get<IDialogs>();
                if (Owner != null)
                {
                    if (Room != null)
                    {
                        if (User != null)
                        {
                            var admin = DbContext.Instance.GetAdministrator();
                            var rs = new Models.AssignRoomUserModel();
                            rs.DateCreated = DateTime.Now;
                            rs.DateModified = DateTime.Now;
                            rs.IdAdmin = admin.IdAdmin;
                            rs.IdAssign = Guid.NewGuid();
                            rs.IdOwner = Owner.IdOwner;
                            rs.IdRoom = Room.IdRoom;
                            rs.StatusAssign = true;
                            rs.UserId = User.UserId;
                            var response = await client.Post<ListResponse, Models.AssignRoomUserModel>(rs, "assignmentroomuser/insassignmentroomuser");
                            if (response != null)
                            {
                                if (response.Result && response.Count > 0)
                                {
                                    await PopupNavigation.Instance.PopAllAsync();
                                    MessagingCenter.Send<AddAssignmentRoomUserPopup>(this, "loadassignroomuser");
                                    await snack.SnackBarSucces("Se registro correctamente", "KyA", TypeSnackBar.Top);
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
                            await snack.SnackBarError("Ingrese un usuario", "Error", TypeSnackBar.Top);
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
            catch(Exception exx)
            { }
        }

        private void Pickeruser_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as CustomPicker;
            User = (UserModel)picker.SelectedItem;
        }

        private void Pickerroom_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as CustomPicker;
            Room = (RoomsModel)picker.SelectedItem;

        }
        private async void ExitImage(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync(); 
        }
        #region LoadPickers
        private void Pickerowner_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as CustomPicker;
            Owner = (OwnerModel)picker.SelectedItem;
            if (Owner != null)
            {
                LoadUser();
                LoadRoom();
            }

        }
        private async void LoadPickerOwner()
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
                        foreach (var itemRoom in responseRoom.Result.Where(c=>c.TypeStatusRoom==0))
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

        private async void LoadUser()
        {
            try
            {              
                pickeruser.IsVisible = true;
                ListUser = new ObservableCollection<UserModel>();
                ListUser.Clear();
                var response = await client.Get<ListUserModel>($"administrator/seluser?IdOwner={Owner.IdOwner}&status={true}");
                if (response != null)
                {
                    if (response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result.Where(c=>c.StatusAssignUser==0))
                        {
                            ListUser.Add(item);
                        }
                    }
                }
                pickeruser.ItemsSource = ListUser;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
