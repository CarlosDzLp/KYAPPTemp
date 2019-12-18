using System;
using System.Collections.Generic;
using System.Windows.Input;
using KyAApp.Service;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;
using KyAApp.Models.Message;
using KyAApp.DataBase;
using KyAApp.Helpers;
using KyAApp.Models;

namespace KyAApp.Views.User
{
    public partial class MessageUserToOwnerPage : ContentPage
    {
        public MessageUserToOwnerPage()
        {
            InitializeComponent();
            Title = "Mensajes";
            this.BindingContext = new MessageUserToOwnerPageViewModel();
        }
    }
    public class MessageUserToOwnerPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
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
        public MessageUserToOwnerPageViewModel()
        {
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
                var user = DbContext.Instance.GetUser();
                var response = await client.Get<ListMessageOwnerToUser>($"messageusertoowner/selmessageusertoowner?userID={user.UserId}&IdOwner={user.IdOwner}");
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
                                item.UrlImage = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + user.IconString;
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
            catch (Exception ex)
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
                    var user = DbContext.Instance.GetUser();
                    var message = new MessageOwnerToUser();
                    message.IdOwner = user.IdOwner;
                    message.Image = null;
                    message.Message = Message;
                    message.StringImage = string.Empty;
                    message.TypeMessage = 0;
                    message.TypeSendUser = 0;
                    message.UserId = user.UserId;
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
                    var user = DbContext.Instance.GetUser();
                    var message = new MessageOwnerToUser();
                    message.IdOwner = user.IdOwner;
                    message.Image = Image;
                    message.Message = string.Empty;
                    message.StringImage = string.Empty;
                    message.TypeMessage = 1;
                    message.TypeSendUser = 0;
                    message.UserId = user.UserId;
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
