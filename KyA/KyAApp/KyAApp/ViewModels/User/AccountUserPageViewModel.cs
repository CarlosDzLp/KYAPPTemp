using System;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;
using System.Windows.Input;
using KyAApp.Models.User;
using KyAApp.Helpers;
using KyAApp.DataBase;
using KyAApp.Service;
using KyAApp.Models;

namespace KyAApp.ViewModels.User
{
    public class AccountUserPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private UserModel _user;
        public UserModel User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }
        private bool _isPassword;
        public bool IsPassword
        {
            get { return _isPassword; }
            set { SetProperty(ref _isPassword, value); }
        }
        private string _icon;
        public string Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }
        #endregion

        #region Constructor
        public AccountUserPageViewModel()
        {
            IsPassword = true;
            Icon = "visibility_on";
            User = DbContext.Instance.GetUser();
            ImageConvert = User.Icon;
            User.User = Encrypt.DeCrypt(User.User);
            User.Password = Encrypt.DeCrypt(User.Password);
            UpdateUserCommand = new Command(UpdateUserCommandExecuted);
            ImageCommand = new Command(ImageCommandExecuted);
            ImagePasswordCommand = new Command(ImagePasswordCommandExecuted);
        }




        #endregion

        #region Methods
        int band = 0;
        private void ImagePasswordCommandExecuted()
        {
            if (band == 0)
            {
                IsPassword = false;
                Icon = "visibility_off";
                band = 1;
            }
            else
            {
                IsPassword = true;
                Icon = "visibility_on";
                band = 0;
            }
            //visibility_on
        }
        #endregion

        #region Command
        public ICommand UpdateUserCommand { get; set; }
        public ICommand ImageCommand { get; set; }
        public ICommand ImagePasswordCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void UpdateUserCommandExecuted()
        {
            //accion para actualiza r el usuario
            try
            {
                if (!string.IsNullOrWhiteSpace(User.Password))
                {
                    if (!string.IsNullOrWhiteSpace(User.Name))
                    {
                        if (ImageConvert != null)
                        {
                            User.Icon = ImageConvert;
                        }
                        User.Status = true;
                        User.User = Encrypt.Crypt(User.User);
                        User.Password = Encrypt.Crypt(User.Password);
                        Show("Actualizando datos..");
                        var response = await client.Put<ListResponse, UserModel>(User, "user/updateuser");
                        Hide();
                        if (response != null)
                        {
                            if (response.Result && response.Count > 0)
                            {
                                SnackSucces("Datos actualizados", "KYA", Helpers.TypeSnackBar.Top);
                                DbContext.Instance.InsertUser(User);
                            }
                        }
                        else
                        {
                            SnackError("Hubo un error intentelo mas tarde", "Error", Helpers.TypeSnackBar.Top);
                        }
                    }
                    else
                    {
                        SnackError("Ingrese un nombre", "Error", Helpers.TypeSnackBar.Top);
                    }
                }
                else
                {
                    SnackError("Ingrese una contraseña", "Error", Helpers.TypeSnackBar.Top);
                }
            }
            catch (Exception ex)
            {
                Hide();
            }
        }
        private async void ImageCommandExecuted()
        {
            try
            {
                var status = await Utils.PermissionsStatus(Plugin.Permissions.Abstractions.Permission.Camera);
                if (status)
                {
                    string action = await App.Current.MainPage.DisplayActionSheet("", "Cancelar", null, "Galeria", "Camara");
                    if (action == "Camara")
                    {
                        ImageConvert = await PhotoCamera.TakePhoto();
                    }
                    else if (action == "Galeria")
                    {
                        ImageConvert = await PhotoCamera.PickPhoto();
                    }
                    else
                    {
                        ImageConvert = null;
                    }

                }
                else
                {
                    SnackError("Habilite los permisos de uso de la camara", "Error", Helpers.TypeSnackBar.Top);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        protected async override void ExitCommanExecuted()
        {
            await NavigationPopAsync();
        }
    }
}
