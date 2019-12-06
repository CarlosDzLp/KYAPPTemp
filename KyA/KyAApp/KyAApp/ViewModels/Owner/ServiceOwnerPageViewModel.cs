using System;
using System.Collections.ObjectModel;
using KyAApp.DataBase;
using KyAApp.Models.Service;
using KyAApp.Service;
using KyAApp.ViewModels.Base;

namespace KyAApp.ViewModels.Owner
{
    public class ServiceOwnerPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private ObservableCollection<ServiceModel> _listService;
        public ObservableCollection<ServiceModel> ListService
        {
            get { return _listService; }
            set { SetProperty(ref _listService, value); }
        }
        #endregion

        #region Constructor
        public ServiceOwnerPageViewModel()
        {
            LoadService();
        }
        #endregion

        #region Methods
        private async void LoadService()
        {
            try
            {
                ListService = new ObservableCollection<ServiceModel>();
                ListService.Clear();
                var owner = DbContext.Instance.GetOwner();
                var responseService = await client.Get<ListServiceModel>($"service/selservice?IdOwner={owner.IdOwner}");
                if (responseService != null)
                {
                    if (responseService.Result != null && responseService.Count > 0)
                    {
                        foreach (var itemService in responseService.Result)
                        {
                            itemService.IconString = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + itemService.IconString;
                            ListService.Add(itemService);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

    }
}
