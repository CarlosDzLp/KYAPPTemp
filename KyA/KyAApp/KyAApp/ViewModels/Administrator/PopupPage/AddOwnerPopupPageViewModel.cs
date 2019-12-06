using System;
using System.Windows.Input;
using KyAApp.DataBase;
using KyAApp.Helpers;
using KyAApp.Models;
using KyAApp.Models.Owner;
using KyAApp.Service;
using KyAApp.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace KyAApp.ViewModels.Administrator.PopupPage
{
    public class AddOwnerPopupPageViewModel : BindableBase
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
        #endregion


        #region Constructor
        public AddOwnerPopupPageViewModel()
        {
            AddSaveOwnerCommand = new Command(AddSaveOwnerCommandExecuted);
        }
        #endregion

        #region Command
        public ICommand AddSaveOwnerCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void AddSaveOwnerCommandExecuted()
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
                var o = new OwnerModel();
                o.Password = Encrypt.Crypt(User);
                o.IdAdmin = admin.IdAdmin;
                o.Address = Address;
                o.Name = Name;
                o.IconString = string.Empty;
                o.Phone = Phone;
                o.User = Encrypt.Crypt(User);
                o.Icon = null;
                var response = await client.Post<ListResponse, OwnerModel>(o, $"administrator/insowner");
                if(response != null)
                {
                    if(response.Result && response.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAllAsync(true);
                        MessagingCenter.Send<AddOwnerPopupPageViewModel>(this, "home");
                        SnackSucces("Se agrego correctamente" , "KyA", TypeSnackBar.Top);
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
