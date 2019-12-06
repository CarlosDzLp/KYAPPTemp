using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using KyAApp.DataBase;
using KyAApp.Models.User;
using KyAApp.Service;
using KyAApp.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace KyAApp.ViewModels.Owner
{
    public class MisUsuariosPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private ObservableCollection<UserModel> _listUser;
        public ObservableCollection<UserModel> ListUser
        {
            get { return _listUser; }
            set { SetProperty(ref _listUser, value); }
        }
        #endregion

        #region Constructor
        public MisUsuariosPageViewModel()
        {
            LoadUser();
            SelectedUserCommand = new Command<UserModel>(SelectedUserCommandExecuted);
        }

        
        #endregion

        #region Command
        public ICommand SelectedUserCommand { get; set; }
        #endregion

        #region Methods
        private async void LoadUser()
        {
            try
            {
                var owner = DbContext.Instance.GetOwner();
                ListUser = new ObservableCollection<UserModel>();
                ListUser.Clear();
                var response = await client.Get<ListUserModel>($"administrator/seluser?IdOwner={owner.IdOwner}&status={true}");
                if (response != null)
                {
                    if (response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result)
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
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region CommandExecuted
        private async void SelectedUserCommandExecuted(UserModel use)
        {
            //mostrar datos solo de lectura
            await PopupNavigation.Instance.PushAsync(new Views.Owner.UserPagePopup(use));
        }
        #endregion
    }
}
