using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using KyAApp.DataBase;
using KyAApp.Helpers;
using KyAApp.Models;
using KyAApp.Models.Message;
using KyAApp.Models.User;
using KyAApp.Service;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;

namespace KyAApp.Views.Owner
{
    public partial class MessageOwnerUserPage : ContentPage
    {
        public MessageOwnerUserPage(Models.User.UserModel obj)
        {
            InitializeComponent();
            this.BindingContext = new MessageOwnerUserPageViewModel(obj);
        }
    }




    public class MessageOwnerUserPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private UserModel _user;
        public UserModel User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private ObservableCollection<MessageOwnerToUser> _listMessage;
        public ObservableCollection<MessageOwnerToUser> ListMessage
        {
            get { return _listMessage; }
            set { SetProperty(ref _listMessage, value); }
        }
        #endregion

        #region Constructor
        public MessageOwnerUserPageViewModel(UserModel user)
        {
            this.User = user;
            LoadMessage();
            ImageCommand = new Command(ImageCommandExecuted);
            SendCommand = new Command(SendCommandExecuted);
        }
        #endregion

        #region Command
        public ICommand ImageCommand { get; set; }
        public ICommand SendCommand { get; set; }
        #endregion

        #region Methods
        private async void LoadMessage()
        {
            try
            {
                IsBussy = true;
                ListMessage = new ObservableCollection<MessageOwnerToUser>();
                ListMessage.Clear();
                var owner = DbContext.Instance.GetOwner();
                var response = await client.Get<ListMessageOwnerToUser>($"messageusertoowner/selmessageusertoowner?userID={User.UserId}&IdOwner={owner.IdOwner}");
                if (response != null)
                {
                    if (response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result)
                        {
                            if (item.TypeSendUser == 1)
                            {
                                //es admin
                                item.UrlImage = "user";
                            }
                            else
                            {
                                item.UrlImage = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + owner.IconString;
                            }
                            if (item.TypeMessage == 1)
                            {
                                item.StringImage = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + item.StringImage;
                            }
                            ListMessage.Add(item);
                        }
                    }
                }
                IsBussy = false;
            }
            catch(Exception ex)
            {
                IsBussy = false;

            }
        }
        #endregion

        #region CommandExecuted
        private async void SendCommandExecuted()
        {
            var statusStorage = await Utils.PermissionsStatus(Plugin.Permissions.Abstractions.Permission.Storage);
            var statuscamera = await Utils.PermissionsStatus(Plugin.Permissions.Abstractions.Permission.Camera);
            if (statuscamera && statusStorage)
            {
                if (!string.IsNullOrWhiteSpace(Message))
                {
                    var owner = DbContext.Instance.GetOwner();
                    var message = new MessageOwnerToUser();
                    message.IdOwner = owner.IdOwner;
                    message.Image = null;
                    message.Message = Message;
                    message.StringImage = string.Empty;
                    message.TypeMessage = 0;
                    message.TypeSendUser = 1;
                    message.UserId = User.UserId;
                    var response = await client.Post<ListResponse, MessageOwnerToUser>(message, "messageusertoowner/insmessageusertoowner");
                    if (response != null)
                    {
                        if (response.Result && response.Count > 0)
                        {
                            Message = string.Empty;
                            LoadMessage();
                        }
                    }
                }

            }
            else
            {
                SnackError("habilita los permisos", "Error", TypeSnackBar.Top);
            }
        }

        private async void ImageCommandExecuted()
        {
            var statusStorage = await Utils.PermissionsStatus(Plugin.Permissions.Abstractions.Permission.Storage);
            var statuscamera = await Utils.PermissionsStatus(Plugin.Permissions.Abstractions.Permission.Camera);
            if (statuscamera && statusStorage)
            {
                byte[] Image = null;
                string action = await App.Current.MainPage.DisplayActionSheet("", "Cancelar", null, "Galeria", "Camara");
                if (action == "Galeria")
                {
                    Image = await PhotoCamera.PickPhoto();
                }
                else if (action == "Camara")
                {
                    Image = await PhotoCamera.TakePhoto();
                }
                if (Image != null)
                {
                    var owner = DbContext.Instance.GetOwner();
                    var message = new MessageOwnerToUser();
                    message.IdOwner = owner.IdOwner;
                    message.Image = Image;
                    message.Message = string.Empty;
                    message.StringImage = string.Empty;
                    message.TypeMessage = 1;
                    message.TypeSendUser = 1;
                    message.UserId = User.UserId;
                    var response = await client.Post<ListResponse, MessageOwnerToUser>(message, "messageusertoowner/insmessageusertoowner");
                    if (response != null)
                    {
                        if (response.Result && response.Count > 0)
                        {
                            LoadMessage();
                        }
                    }
                }
            }
            else
            {
                SnackError("habilita los permisos", "Error", TypeSnackBar.Top);
            }
        }
        #endregion

        protected async override void ExitCommanExecuted()
        {
            await NavigationPopAsync();
        }
    }
}
