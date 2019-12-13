using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using KyAApp.Models.Owner;
using KyAApp.Models.User;
using KyAApp.Service;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;

namespace KyAApp.ViewModels.Administrator
{
    public class MessageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private ObservableCollection<UserModel> _listUser;
        public ObservableCollection<UserModel> ListUser
        {
            get { return _listUser; }
            set { SetProperty(ref _listUser, value); }
        }

        private ObservableCollection<OwnerModel> _listOwner;
        public ObservableCollection<OwnerModel> ListOwner
        {
            get { return _listOwner; }
            set { SetProperty(ref _listOwner, value); }
        }
        #endregion

        #region Constructor
        public MessageViewModel()
        {
            LoadOwner();
            LoadUser();
            SelectedUser = new Command<UserModel>(SelectedUserExecuted);
            SelectedOwner = new Command<OwnerModel>(SelectedOwnerExecuted);
        }
        #endregion

        #region Methods
        private async void LoadUser()
        {
            try
            {
                ListUser = new ObservableCollection<UserModel>();
                ListUser.Clear();
                var response = await client.Get<ListUserModel>($"administrator/seluser?IdOwner={null}&status={true}");
                if (response != null)
                {
                    if (response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result.Where(c => c.Status == true))
                        {
                            var img = (item.IconString == null || item.IconString == string.Empty) ? "user" : "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + item.IconString;
                            item.IconString = img;
                            ListUser.Add(item);
                        }
                    }
                }
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
        public ICommand SelectedUser { get; set; }
        public ICommand SelectedOwner { get; set; }
        #endregion

        #region CommandExecuted
        private async void SelectedOwnerExecuted(OwnerModel obj)
        {
            await NavigationPushModalAsync(new Views.Administrator.MessageOwnerPage(obj));
        }

        private async void SelectedUserExecuted(UserModel obj)
        {
            await NavigationPushModalAsync(new Views.Administrator.MessageUserPage(obj));
        }
        #endregion
    }
}
