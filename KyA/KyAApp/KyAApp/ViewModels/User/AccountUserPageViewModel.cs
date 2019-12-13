using System;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;
using System.Windows.Input;
using KyAApp.Models.User;
using KyAApp.Helpers;
using KyAApp.DataBase;
using KyAApp.Service;
using KyAApp.Models;
using KyAApp.Models.Authenticate;

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
            LoadImage();
            UpdateUserCommand = new Command(UpdateUserCommandExecuted);
            ImageCommand = new Command(ImageCommandExecuted);
            ImagePasswordCommand = new Command(ImagePasswordCommandExecuted);
        }


        private void LoadImage()
        {
            User = DbContext.Instance.GetUser();
            ImageConvert = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + User.IconString;
            var user = Encrypt.DeCrypt(User.User);
            var password = Encrypt.DeCrypt(User.Password);
            User.User = user;
            User.Password = password;
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
                            if (!ImageConvert.Contains("http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image"))
                            {
                                User.Icon = Convert.FromBase64String(ImageConvert);
                            }
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
                                var device = DbContext.Instance.GetDeviceToken();
                                var auth = new AuthenticateModel();
                                auth.User = User.User;
                                auth.Password = User.Password;
                                auth.PlayerId = device.PlayerId;
                                auth.PushToken = device.PushToken;
                                var responseUSer = await client.Post<Models.User.User, AuthenticateModel>(auth, "user/seluser");
                                if(responseUSer != null)
                                {
                                    if(responseUSer.Result != null && responseUSer.Count > 0)
                                    {
                                        DbContext.Instance.InsertUser(responseUSer.Result);
                                        LoadImage();
                                        MessagingCenter.Send<App>((App)Xamarin.Forms.Application.Current, "user");
                                    }
                                }
                                //
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
                        var img  = await PhotoCamera.TakePhoto();
                        ImageConvert = Convert.ToBase64String(img);
                    }
                    else if (action == "Galeria")
                    {
                        var img = await PhotoCamera.PickPhoto();
                        ImageConvert = Convert.ToBase64String(img);
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
