using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using KyAApp.DataBase;
using KyAApp.Models;
using KyAApp.Models.Owner;
using KyAApp.Models.User;
using KyAApp.Service;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;

namespace KyAApp.ViewModels.Administrator
{
    public class ListUserPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        public OwnerModel Owner { get; set; }
        private ObservableCollection<UserModel> _listUser;
        public ObservableCollection<UserModel>ListUser
        {
            get { return _listUser; }
            set { SetProperty(ref _listUser, value); }
        }
        #endregion

        #region Constructor
        public ListUserPageViewModel(OwnerModel owner)
        {
            this.Owner = owner;
            LoadUser();
            DeleteUserCommand = new Command<UserModel>(DeleteUserCommandExecuted);
        }
        #endregion

        #region Methods
        private async void LoadUser()
        {
            try
            {
                ListUser = new ObservableCollection<UserModel>();
                ListUser.Clear();
                var response = await client.Get<ListUserModel>($"administrator/seluser?IdOwner={Owner.IdOwner}&status={true}");
                if(response != null)
                {
                    if(response.Result != null && response.Count > 0)
                    {
                        foreach(var item in response.Result)
                        {
                            var img = (item.IconString == null || item.IconString == string.Empty) ? "user" : "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + item.IconString;
                            item.IconString = img;
                            ListUser.Add(item);
                        }
                    }
                }
                else
                {
                    SnackError("Hubo un erro intentelo mas tarde", "Error", Helpers.TypeSnackBar.Top);
                }
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Command
        public ICommand DeleteUserCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void DeleteUserCommandExecuted(UserModel user)
        {
            try
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Eliminar", $"Desea eliminar al usuario: {user.Name}", "Si", "No");
                if (answer)
                {
                    var db = DbContext.Instance.GetAdministrator();
                    var response = await client.Delete<ListResponse>($"administrator/deluser?IdUser={user.UserId}&IdOwner={user.IdOwner}&IdAdmin={user.IdAdmin}&Name={user.Name}");
                    if (response != null)
                    {
                        if (response.Result && response.Count > 0)
                        {
                            SnackSucces("Se elimino correctamente", "KYA", Helpers.TypeSnackBar.Top);
                            LoadUser();
                        }
                        else
                        {
                            SnackError(response.Message, "Error", Helpers.TypeSnackBar.Top);
                        }
                    }
                    else
                    {
                        SnackError("hubo un error intentelo mas tarde", "Error", Helpers.TypeSnackBar.Top);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        #endregion
        protected override async void ExitCommanExecuted()
        {
            await NavigationPopAsync();
        }
    }
}
