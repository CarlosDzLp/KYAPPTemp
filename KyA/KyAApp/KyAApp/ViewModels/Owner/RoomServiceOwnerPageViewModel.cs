using System;
using System.Collections.ObjectModel;
using KyAApp.DataBase;
using KyAApp.Models;
using KyAApp.Service;
using KyAApp.ViewModels.Base;

namespace KyAApp.ViewModels.Owner
{
    public class RoomServiceOwnerPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private ObservableCollection<RoomServiceModel> _listRoomService;
        public ObservableCollection<RoomServiceModel> ListRoomService
        {
            get { return _listRoomService; }
            set { SetProperty(ref _listRoomService, value); }
        }
        #endregion

        #region Constructor
        public RoomServiceOwnerPageViewModel()
        {
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
                var owner = DbContext.Instance.GetOwner();
                var responseRoomService = await client.Get<ListRoomServiceModel>($"roomservice/selroomservice?IdOwner={owner.IdOwner}");
                if (responseRoomService != null)
                {
                    if (responseRoomService.Result != null && responseRoomService.Count > 0)
                    {
                        foreach (var itemRoomService in responseRoomService.Result)
                        {
                           
                            ListRoomService.Add(itemRoomService);
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

    }
}
