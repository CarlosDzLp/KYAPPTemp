using System;
using System.Windows.Input;
using KyAApp.DataBase;
using KyAApp.Helpers;
using KyAApp.Models;
using KyAApp.Models.Owner;
using KyAApp.Models.User;
using KyAApp.Service;
using KyAApp.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace KyAApp.ViewModels.Administrator.PopupPage
{
    public class AddUserAdminPopupPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        private string _user;
        public string User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }
        private OwnerModel Owner { get; set; }
        #endregion

        #region Constructor
        public AddUserAdminPopupPageViewModel(OwnerModel owner)
        {
            this.Owner = owner;
            AddSaveUserCommand = new Command(AddSaveUserCommandExecuted);
        }
        #endregion

        #region Command
        public ICommand AddSaveUserCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void AddSaveUserCommandExecuted()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    SnackError("Ingrese un nombre", "Error", Helpers.TypeSnackBar.Top);
                    return;
                }
                if (string.IsNullOrWhiteSpace(Address))
                {
                    SnackError("Ingrese una direccion", "Error", Helpers.TypeSnackBar.Top);
                    return;
                }
                if (string.IsNullOrWhiteSpace(User))
                {
                    SnackError("Ingrese un usuario", "Error", Helpers.TypeSnackBar.Top);
                    return;
                }
                if (string.IsNullOrWhiteSpace(Phone))
                {
                    SnackError("Ingrese un telefono", "Error", Helpers.TypeSnackBar.Top);
                    return;
                }
                var admin = DbContext.Instance.GetAdministrator();
                var o = new UserModel();
                o.Address = Address;
                o.Icon = null;
                o.IconString = string.Empty;
                o.IdAdmin = admin.IdAdmin;
                o.IdOwner = Owner.IdOwner;
                o.Name = Name;
                o.Password = Encrypt.Crypt(User);
                o.Phone = Phone;
                o.User = Encrypt.Crypt(User);
                var response = await client.Post<ListResponse, UserModel>(o, $"administrator/insuser");
                if (response != null)
                {
                    if (response.Result && response.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAllAsync(true);
                        SnackSucces("Se agrego correctamente", "KyA", TypeSnackBar.Top);
                    }
                    else
                    {
                        SnackError(response.Message, "Error", TypeSnackBar.Top);
                    }
                }
                else
                {
                    SnackError("Hubo un error intentelo mas tarde", "Error", TypeSnackBar.Top);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
