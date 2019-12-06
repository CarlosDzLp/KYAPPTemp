using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using KyAApp.DataBase;
using KyAApp.Helpers;
using KyAApp.Models.Administrator;
using KyAApp.Models.Authenticate;
using KyAApp.Models.Owner;
using KyAApp.Models.Session;
using KyAApp.Models.User;
using KyAApp.Service;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;

namespace KyAApp.ViewModels.Session
{
    public class LoginPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private ObservableCollection<TypeUser> _listTypeUser;
        public ObservableCollection<TypeUser>ListTypeUser
        {
            get { return _listTypeUser; }
            set { SetProperty(ref _listTypeUser, value); }
        }

        private TypeUser _selectedTypeUser;
        public TypeUser SelectedTypeUser
        {
            get { return _selectedTypeUser; }
            set
            {
                SetProperty(ref _selectedTypeUser, value);
            }
        }
        private string _user;
        public string User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
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
        public LoginPageViewModel()
        {
            LoadTypeUser();
            LoginCommand = new Command(LoginCommandExecuted);
            ImagePasswordCommand = new Command(ImagePasswordCommandExecuted);
        }
        #endregion

        #region Methods
        private void LoadTypeUser()
        {
            IsPassword = true;
            Icon = "visibility_on";
            ListTypeUser = new ObservableCollection<TypeUser>();
            ListTypeUser.Add(new TypeUser { Id = 0, Type = "Usuario" });
            ListTypeUser.Add(new TypeUser { Id = 1, Type = "Propietario" });
            ListTypeUser.Add(new TypeUser { Id = 2, Type = "Administrador" });
        }
        #endregion

        #region Command
        public ICommand LoginCommand { get; set; }
        public ICommand ImagePasswordCommand { get; set; }
        #endregion

        #region CommandExecuted
        int band = 0;
        private void ImagePasswordCommandExecuted()
        {
            if(band == 0)
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
        private async void LoginCommandExecuted()
        {
            if(!string.IsNullOrWhiteSpace(User))
            {
                if(!string.IsNullOrWhiteSpace(Password))
                {
                    if (SelectedTypeUser != null)
                    {
                        var device = DbContext.Instance.GetDeviceToken();
                        var auth = new AuthenticateModel();
                        auth.User = Encrypt.Crypt(User);
                        auth.Password = Encrypt.Crypt(Password);
                        auth.PlayerId = device.PlayerId;
                        auth.PushToken = device.PushToken;
                        if (SelectedTypeUser.Id == 0)
                        {
                            //Usuario
                            Show("Cargando");
                            var response = await client.Post<Models.User.User, AuthenticateModel>(auth, "user/seluser");
                            if (response != null)
                            {
                                if (response.Result != null && response.Count > 0)
                                {
                                    Hide();
                                    //entra al masterdetailpage del usuario
                                    DbContext.Instance.InsertUser(response.Result);
                                    MainPage(new Views.User.MasterDetailUser());
                                }
                                else
                                {
                                    Hide();
                                    SnackError(response.Message, "Error", TypeSnackBar.Top);
                                }
                            }
                            else
                            {
                                Hide();
                                SnackError("Hubo un error con el servidor intentelo mas tarde", "Error", TypeSnackBar.Top);
                            }
                        }
                        else if (SelectedTypeUser.Id == 1)
                        {
                            //propietario
                            Show("Cargando");
                            var response = await client.Post<Models.Owner.Owner, AuthenticateModel>(auth, "owner/selowner");                           
                            if (response != null)
                            {
                                if (response.Result != null && response.Count > 0)
                                {
                                    Hide();
                                    //entra al masterdetailpage
                                    DbContext.Instance.InsertOwner(response.Result);
                                    MainPage(new Views.Owner.MasterDetailOwner());
                                }
                                else
                                {
                                    Hide();
                                    SnackError(response.Message, "Error", TypeSnackBar.Top);
                                }
                            }
                            else
                            {
                                Hide();
                                SnackError("Hubo un error con el servidor intentelo mas tarde", "Error", TypeSnackBar.Top);
                            }
                        }
                        else
                        {
                            //Administrador
                            Show("Cargando");
                            var response = await client.Post<ListAdministratorModel, AuthenticateModel>(auth, "administrator/seladministrator");
                            if(response != null)
                            {
                                if(response.Result != null && response.Count > 0)
                                {
                                    Hide();
                                    //entra al masterdetailpage
                                    DbContext.Instance.InsertAdministrator(response.Result);
                                    MainPage(new Views.Administrator.MasterDetailAdministrator());
                                }
                                else
                                {
                                    Hide();
                                    SnackError(response.Message, "Error", TypeSnackBar.Top);
                                }
                            }
                            else
                            {
                                Hide();
                                SnackError("Hubo un error con el servidor intentelo mas tarde", "Error", TypeSnackBar.Top);
                            }
                        }
                    }
                    else
                    {
                        SnackError("Debe seleccionar un tipo de usuario", "Error", TypeSnackBar.Top);
                    }
                }
                else
                {
                    SnackError("Debe ingresar una contraseña", "Error", TypeSnackBar.Top);
                }
            }
            else
            {
                SnackError("Debe ingresar un usuario", "Error", TypeSnackBar.Top);
            }        
        }
        #endregion
    }
}
