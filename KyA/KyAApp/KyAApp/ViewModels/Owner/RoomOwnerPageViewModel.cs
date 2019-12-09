using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using KyAApp.DataBase;
using KyAApp.Models.Rooms;
using KyAApp.Service;
using KyAApp.ViewModels.Base;

namespace KyAApp.ViewModels.Owner
{
    public class RoomOwnerPageViewModel : BindableBase
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
        public RoomOwnerPageViewModel()
        {
            LoadRoom();
        }
        #endregion

        #region Methods
        private async void LoadRoom()
        {
            ListRoom = new ObservableCollection<RoomsModel>();
            ListRoom.Clear();
            var owner = DbContext.Instance.GetOwner();
            var response = await client.Get<ListRoomsModel>($"room/selroom?IdOwner={owner.IdOwner}");
            if(response != null)
            {
                if(response.Result != null && response.Count > 0)
                {
                    foreach(var item in response.Result)
                    {
                        if(item.TypeStatusRoom == 0)
                        {
                            item.ColorRoom = Color.Black;
                            item.StatusRoom = "Libre";
                        }
                        else if(item.TypeStatusRoom == 1)
                        {
                            item.ColorRoom = Color.Red;
                            item.StatusRoom = "Ocupado";
                        }
                        ListRoom.Add(item);
                    }
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
        #endregion
    }
}
