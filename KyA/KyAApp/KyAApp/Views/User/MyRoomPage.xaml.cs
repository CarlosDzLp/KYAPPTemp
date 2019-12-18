using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using KyAApp.DataBase;
using KyAApp.Models;
using KyAApp.Models.Rooms;
using KyAApp.Service;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;

namespace KyAApp.Views.User
{
    public partial class MyRoomPage : ContentPage
    {
        public MyRoomPage()
        {
            InitializeComponent();
            this.BindingContext = new MyRoomPageViewModel();
        }
    }
    public class MyRoomPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private ObservableCollection<RoomsModel> _listRoom;
        public ObservableCollection<RoomsModel> ListRoom
        {
            get { return _listRoom; }
            set { SetProperty(ref _listRoom, value); }
        }
        #endregion

        #region Constructor
        public MyRoomPageViewModel()
        {
            LoadRoom();
        }
        #endregion

        #region Methods
        private async void LoadRoom()
        {
            try
            {
                IsBussy = true;
                ListRoom = new ObservableCollection<RoomsModel>();
                ListRoom.Clear();
                var user = DbContext.Instance.GetUser();
                //obtengo todos los cuartos
                var responseRoom = await client.Get<ListRoomsModel>($"room/selroom?IdOwner={user.IdOwner}");

                //todos los cuartos asignados
                var AssignmentRoomUser = await client.Get<ListAssignRoomUserModel>($"assignmentroomuser/selassignroomuserxidowner?IdOwner={user.IdOwner}");


                //todos los servicios a cuartos
                var responseroomService = await client.Get<ListRoomServiceModel>($"roomservice/selroomservice?IdOwner={user.IdOwner}");



                var filterAssignmentRoomUser = (AssignmentRoomUser == null) ? null : (AssignmentRoomUser.Result != null) ? AssignmentRoomUser.Result.Where(c => c.UserId == user.UserId).FirstOrDefault() : null;
                string service = "";
                if (filterAssignmentRoomUser != null)
                {
                    var filterService = (responseroomService == null) ? null : (responseroomService.Result != null) ? responseroomService.Result.Where(c => c.IdRoom == filterAssignmentRoomUser.IdRoom).ToList() : null;
                    foreach (var item in filterService)
                    {
                        service += item.NameService + " " + item.PriceService + " ,";
                    }
                    var FilterRoom = (responseRoom == null) ? null : (responseRoom.Result != null) ? responseRoom.Result.Where(c => c.IdRoom == filterAssignmentRoomUser.IdRoom).FirstOrDefault() : null;
                    if (FilterRoom != null)
                    {
                        FilterRoom.DescriptionRoomService = service;
                        ListRoom.Add(FilterRoom);
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

        #endregion

        #region CommandExecuted

        #endregion
    }
}
