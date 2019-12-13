using System;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;
using System.Windows.Input;
using KyAApp.Helpers;
using KyAApp.Models.Owner;
using KyAApp.Models.Message;
using KyAApp.DataBase;
using KyAApp.Service;
using KyAApp.Models;
using System.Collections.ObjectModel;

namespace KyAApp.ViewModels.Administrator
{
    public class MessageOwnerPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Properties
        private OwnerModel _owner;
        public OwnerModel Owner
        {
            get { return _owner; }
            set { SetProperty(ref _owner, value); }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        private ObservableCollection<MessageOwnerToAdmin> _listMessage;
        public ObservableCollection<MessageOwnerToAdmin> ListMessage
        {
            get { return _listMessage; }
            set { SetProperty(ref _listMessage, value); }
        }
        #endregion
        #region Constructor
        public MessageOwnerPageViewModel(OwnerModel obj)
        {
            this.Owner = obj;
            LoadMessage();
            ImageCommand = new Command(ImageCommandExecuted);
            SendCommand = new Command(SendCommandExecuted);
        }
        #endregion

        #region Methods
        private async void LoadMessage()
        {
            try
            {
                ListMessage = new ObservableCollection<MessageOwnerToAdmin>();
                ListMessage.Clear();
                var response = await client.Get<ListMessageOwnerToAdmin>($"messageownertoadmin/selmessageownertoadmin?IdOwner={Owner.IdOwner}");
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
                                item.UrlImage = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + Owner.IconString;
                            }
                            if (item.TypeMessage == 1)
                            {
                                item.StringImage = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + item.StringImage;
                            }
                            ListMessage.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Command
        public ICommand ImageCommand { get; set; }
        public ICommand SendCommand { get; set; }
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
                    var admin = DbContext.Instance.GetAdministrator();
                    var message = new MessageOwnerToAdmin();
                    message.IdAdmin = admin.IdAdmin;
                    message.Image = null;
                    message.Message = Message;
                    message.StringImage = string.Empty;
                    message.TypeMessage = 0;
                    message.TypeSendUser = 1;
                    message.IdOwner = Owner.IdOwner;
                    var response = await client.Post<ListResponse, MessageOwnerToAdmin>(message, "messageownertoadmin/insmessageownertoadmin");
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
                if(Image != null)
                {
                    var admin = DbContext.Instance.GetAdministrator();
                    var message = new MessageOwnerToAdmin();
                    message.IdAdmin = admin.IdAdmin;
                    message.IdOwner = Owner.IdOwner;
                    message.Image = Image;
                    message.Message = string.Empty;
                    message.StringImage = string.Empty;
                    message.TypeMessage = 1;
                    message.TypeSendUser = 1;
                    var response = await client.Post<ListResponse, MessageOwnerToAdmin>(message, "messageownertoadmin/insmessageownertoadmin");
                    if(response != null)
                    {
                        if(response.Result && response.Count > 0)
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
        #endregion


        protected async override void ExitCommanExecuted()
        {
            await NavigationPopAsync();
        }
    }
}
