using System;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using KyAApp.ViewModels.Administrator.PopupPage;
using KyAApp.Service;
using KyAApp.Models.Owner;
using System.Collections.ObjectModel;
using KyAApp.DataBase;
using KyAApp.Models;

namespace KyAApp.ViewModels.Administrator
{
    public class UserAdminPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();

        #region Properties
        private ObservableCollection<OwnerModel> _listOwner;
        public ObservableCollection<OwnerModel>ListOwner
        {
            get { return _listOwner; }
            set { SetProperty(ref _listOwner, value); }
        }
        #endregion

        #region Constructor
        public UserAdminPageViewModel()
        {
            LoadOwner();
            MessagingCenter.Subscribe<AddOwnerPopupPageViewModel>(this, "home", (sender) =>
            {
                LoadOwner();
            });
            AddOwnerCommand = new Command(AddOwnerCommandExecuted);
            DeleteOwnerCommand = new Command<OwnerModel>(DeleteOwnerCommandExecuted);
            AddUserOwnerCommand = new Command<OwnerModel>(AddUserOwnerCommandExecuted);
            SelectedOwnerCommand = new Command<OwnerModel>(SelectedOwnerCommandExecuted);
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
                if(response != null)
                {
                    if(response.Result != null && response.Count > 0)
                    {
                        foreach(var item in response.Result)
                        {
                            var img = (item.IconString == null || item.IconString == string.Empty) ? "user" : "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + item.IconString;
                            item.IconString = img;
                            ListOwner.Add(item);
                        }
                    }
                    else
                    {
                        SnackError(response.Message, "Error", Helpers.TypeSnackBar.Top);
                    }
                }
                else
                {
                    SnackError("Hubo un error intentelo de nuevo", "Error", Helpers.TypeSnackBar.Top);
                }
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Command
        public ICommand AddOwnerCommand { get; set; }
        public ICommand DeleteOwnerCommand { get; set; }
        public ICommand AddUserOwnerCommand { get; set; }
        public ICommand SelectedOwnerCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void AddOwnerCommandExecuted()
        {
            await PopupNavigation.Instance.PushAsync(new Views.Administrator.Popup.AddOwnerPopupPage());
        }

        private async void DeleteOwnerCommandExecuted(OwnerModel obj)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Eliminar", $"Desea eliminar al propietario: {obj.Name}", "Si", "No");
            if (answer)
            {
                var db = DbContext.Instance.GetAdministrator();
                var response = await client.Delete<ListResponse>($"administrator/delowner?IdOwner={obj.IdOwner}&IdAdmin={db.IdAdmin}&Name={obj.Name}");
                if (response != null)
                {
                    if (response.Result && response.Count > 0)
                    {
                        SnackSucces("Se elimino correctamente","KYA",Helpers.TypeSnackBar.Top);
                        LoadOwner();
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


        private async void AddUserOwnerCommandExecuted(OwnerModel obj)
        {
            //abre un popup para registrar usuarios
            await PopupNavigation.Instance.PushAsync(new Views.Administrator.Popup.AddUserAdminPopupPage(obj));
        }

        private async void SelectedOwnerCommandExecuted(OwnerModel obj)
        {
            //muestra una pagina para mostrar lista de usuarios
            await NavigationPushModalAsync(new Views.Administrator.ListUserPage(obj));
        }
        #endregion
    }
}
