using System;
using System.Windows.Input;
using KyAApp.DataBase;
using KyAApp.Helpers;
using KyAApp.Models;
using KyAApp.Models.Authenticate;
using KyAApp.Models.Owner;
using KyAApp.Models.User;
using KyAApp.Service;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;

namespace KyAApp.ViewModels.Owner
{
    public class AccountOwnerPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private OwnerModel _owner;
        public OwnerModel Owner
        {
            get { return _owner; }
            set { SetProperty(ref _owner, value); }
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
        public AccountOwnerPageViewModel()
        {
            IsPassword = true;
            Icon = "visibility_on";
            LoadImage();
            UpdateOwnerCommand = new Command(UpdateOwnerCommandExecuted);
            ImageCommand = new Command(ImageCommandExecuted);
            ImagePasswordCommand = new Command(ImagePasswordCommandExecuted);
        }
        private void LoadImage()
        {
            Owner = DbContext.Instance.GetOwner();           
            ImageConvert = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + Owner.IconString;
            var password = Encrypt.DeCrypt(Owner.Password);
            var user = Encrypt.DeCrypt(Owner.User);
            Owner.Password = password;
            Owner.User = user;
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
        public ICommand UpdateOwnerCommand { get; set; }
        public ICommand ImageCommand { get; set; }
        public ICommand ImagePasswordCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void UpdateOwnerCommandExecuted()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Owner.Password))
                {
                    if(!string.IsNullOrWhiteSpace(Owner.Name))
                    {
                        if(ImageConvert != null)
                        {
                            if (!ImageConvert.Contains("http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image"))
                            {
                                Owner.Icon = Convert.FromBase64String(ImageConvert);
                            }                          
                        }
                        Owner.User = Encrypt.Crypt(Owner.User);
                        Owner.Password = Encrypt.Crypt(Owner.Password);
                        Show("Actualizando datos...");
                        var response = await client.Put<ListResponse, OwnerModel>(Owner, "owner/updateowner");
                        Hide();
                        if (response != null)
                        {
                            if(response.Result && response.Count > 0)
                            {
                                SnackSucces("Datos actualizados", "KYA", Helpers.TypeSnackBar.Top);
                                var device = DbContext.Instance.GetDeviceToken();
                                var auth = new AuthenticateModel();
                                auth.User = Owner.User;
                                auth.Password = Owner.Password;
                                auth.PlayerId = device.PlayerId;
                                auth.PushToken = device.PushToken;
                                var responseOwner = await client.Post<Models.Owner.Owner, AuthenticateModel>(auth, "owner/selowner");
                                if(responseOwner != null)
                                {
                                    if(responseOwner.Result != null && responseOwner.Count > 0)
                                    {
                                        DbContext.Instance.InsertOwner(responseOwner.Result);
                                        LoadImage();
                                        MessagingCenter.Send<App>((App)Xamarin.Forms.Application.Current, "owner");
                                    }
                                }
                                
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
            catch(Exception ex)
            {
                Hide();
            }
        }

        private async void ImageCommandExecuted()
        {
            try
            {
                var status = await Utils.PermissionsStatus(Plugin.Permissions.Abstractions.Permission.Camera);
                if(status)
                {
                    string action = await App.Current.MainPage.DisplayActionSheet("", "Cancelar", null, "Galeria", "Camara");
                    if(action == "Camara")
                    {
                        var img = await PhotoCamera.TakePhoto();
                        ImageConvert = Convert.ToBase64String(img);
                    }
                    else if(action == "Galeria")
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
            catch(Exception ex)
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
