﻿using System;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;
using System.Windows.Input;
using KyAApp.Helpers;
using KyAApp.Models.Documents;
using System.Collections.ObjectModel;
using KyAApp.Service;
using KyAApp.DataBase;

namespace KyAApp.ViewModels.User
{
    public class HomeUserViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();

        #region Properties
        private ObservableCollection<DocumentsModel> _listDocument;
        public ObservableCollection<DocumentsModel> ListDocument
        {
            get { return _listDocument; }
            set { SetProperty(ref _listDocument, value); }
        }

        private bool _isVisibleEmpty;
        public bool IsVisibleEmpty
        {
            get { return _isVisibleEmpty; }
            set { SetProperty(ref _isVisibleEmpty, value); }
        }

        private bool _isVisibleList;
        public bool IsVisibleList
        {
            get { return _isVisibleList; }
            set { SetProperty(ref _isVisibleList, value); }
        }
        #endregion


        #region Constructor
        public HomeUserViewModel()
        {
            LoadDocument();
            AccountCommand = new Command(AccountCommandExecuted);
            SelectedDocumentCommand = new Command<DocumentsModel>(SelectedDocumentCommandExecuted);
            ChatAdminCommand = new Command(ChatAdminCommandExecuted);
            var user = DbContext.Instance.GetUser();
            ImageConvert = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + user.IconString;
            MessagingCenter.Subscribe<App>((App)Xamarin.Forms.Application.Current, "user", (s) =>
            {
                var users = DbContext.Instance.GetUser();
                ImageConvert = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + users.IconString;
            });
        }
        #endregion

        #region Methods
        private async void LoadDocument()
        {
            try
            {
                IsBussy = true;
                IsVisibleEmpty = false;
                IsVisibleList = true;
                ListDocument = new ObservableCollection<DocumentsModel>();
                ListDocument.Clear();
                var response = await client.Get<ListDocumentsModel>($"documents/seldocument?IdOwner={null}");
                if (response != null)
                {
                    if (response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result)
                        {
                            var t = (item.IdOwner == null) ? "Todos" : "Usuario especifico";
                            item.Icon = "pdf";
                            item.StringFile = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Documents/" + item.StringFile;
                            ListDocument.Add(item);
                        }
                    }
                    else
                    {
                        IsVisibleEmpty = true;
                        IsVisibleList = false;
                    }

                }
                else
                {
                    IsVisibleEmpty = true;
                    IsVisibleList = false;
                    SnackError("Hubo un error intentelo mas tarde", "Error", TypeSnackBar.Top);
                }
                IsBussy = false;
            }
            catch (Exception ex)
            {
                IsBussy = false;
            }
        }
        #endregion

        #region Command
        public ICommand AccountCommand { get; set; }
        public ICommand SelectedDocumentCommand { get; set; }
        public ICommand ChatAdminCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void AccountCommandExecuted()
        {
            //abre un poup para modificar sus datos personnales
            await NavigationPushModalAsync(new Views.User.AccountUserPage());
        }
        private async void ChatAdminCommandExecuted()
        {
            var user = DbContext.Instance.GetUser();
            await NavigationPushModalAsync(new Views.User.MessageUserPage());
        }
        #endregion

        private async void SelectedDocumentCommandExecuted(DocumentsModel obj)
        {
            await NavigationPushModalAsync(new Views.Administrator.DocumentsPage(obj));
        }
    }
}
