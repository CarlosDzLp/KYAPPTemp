using System;
using System.Windows.Input;
using Xamarin.Forms;
using KyAApp.ViewModels.Base;
using KyAApp.Service;
using System.Collections.ObjectModel;
using KyAApp.Models.Documents;
using KyAApp.Helpers;

namespace KyAApp.ViewModels.Owner
{
    public class HomeOwnerViewModel : BindableBase
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
        public HomeOwnerViewModel()
        {
            LoadDocument();
            AccountCommand = new Command(AccountCommandExecuted);
            SelectedDocumentCommand = new Command<DocumentsModel>(SelectedDocumentCommandExecuted);
        }
        #endregion

        #region Methods
        private async void LoadDocument()
        {
            try
            {
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
                    SnackError("hubo un error intentelo mas tarde", "Error", TypeSnackBar.Top);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Command
        public ICommand AccountCommand { get; set; }
        public ICommand SelectedDocumentCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void AccountCommandExecuted()
        {
            //abre un poup para modificar sus datos personnales
            await NavigationPushModalAsync(new Views.Owner.AccountOwnerPage());
        }
        #endregion

        private async void SelectedDocumentCommandExecuted(DocumentsModel obj)
        {
            await NavigationPushModalAsync(new Views.Administrator.DocumentsPage(obj));
        }
    }
}
