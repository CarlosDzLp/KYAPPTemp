using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using KyAApp.DataBase;
using KyAApp.Helpers;
using KyAApp.Models;
using KyAApp.Models.Documents;
using KyAApp.Models.Owner;
using KyAApp.Service;
using KyAApp.ViewModels.Administrator.PopupPage;
using KyAApp.ViewModels.Base;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Xamarin.Forms;

namespace KyAApp.ViewModels.Administrator
{
    public class HomeAdministratorViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();

        #region Properties
        private ObservableCollection<DocumentsModel> _listDocument;
        public ObservableCollection<DocumentsModel> ListDocument
        {
            get { return _listDocument; }
            set { SetProperty(ref _listDocument, value); }
        }
        private bool _isVisible = false;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private byte[] _file;
        public byte[] File
        {
            get { return _file; }
            set { SetProperty(ref _file, value); }
        }

        private ObservableCollection<OwnerModel> _listOwner;
        public ObservableCollection<OwnerModel> ListOwner
        {
            get { return _listOwner; }
            set { SetProperty(ref _listOwner, value); }
        }
        private OwnerModel _selectedOwner;
        public OwnerModel SelectedOwner
        {
            get { return _selectedOwner; }
            set { SetProperty(ref _selectedOwner, value); }
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


        private string _empty;
        public string Empty
        {
            get { return _empty; }
            set { SetProperty(ref _empty, value); }
        }
        #endregion

        #region Constructor
        public HomeAdministratorViewModel()
        {
            LoadDocument();
            LoadOwner();
            AddDocumentCommand = new Command(AddDocumentCommandExecuted);
            SaveDocumentCommand = new Command(SaveDocumentCommandExecuted);
            SelectedDocumentCommand = new Command<DocumentsModel>(SelectedDocumentCommandExecuted);
            RefreshCommand = new Command(LoadDocument);
            DeleteDocumentCommand = new Command<DocumentsModel>(DeleteDocumentCommandExecuted);
        }

        
        #endregion

        #region Methods
        private async void LoadOwner()
        {
            try
            {
                
                ListOwner = new ObservableCollection<OwnerModel>();
                ListOwner.Clear();
                ListOwner.Add(new OwnerModel { Name = "Todos", IdOwner = Guid.Empty });
                
                var response = await client.Get<ListOwnerModel>($"administrator/selowner?status={true}");
                if(response != null)
                {
                    if(response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result)
                        {
                            ListOwner.Add(item);
                        }
                    }
                    
                }
                
            }
            catch(Exception ex)
            {

            }
        }
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
                    if(response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result)
                        {
                            var t = (item.IdOwner == null) ? "Todos" : "Usuario especifico";
                            item.Icon = "pdf";
                            item.TypeUser = "Ver: " + t;
                            item.StringFile = "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Documents/" + item.StringFile;
                            ListDocument.Add(item);
                        }
                    }
                    else
                    {
                        Empty = response.Message;
                        IsVisibleEmpty = true;
                        IsVisibleList = false;
                    }
                    
                }
                else
                {
                    Empty = "No hay datos";
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
        public ICommand AddDocumentCommand { get; set; }
        public ICommand SaveDocumentCommand { get; set; }
        public ICommand SelectedDocumentCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteDocumentCommand { get; set; }
        #endregion

        string extensionType = "";
        #region CommandExecuted
        private async void AddDocumentCommandExecuted()
        {
            try
            {
                var status = await Utils.PermissionsStatus(Plugin.Permissions.Abstractions.Permission.Storage);
                if(status)
                {
                    FileData fileData = await CrossFilePicker.Current.PickFile();
                    if (fileData == null)
                    {
                        return;
                    }

                    extensionType = fileData.FileName.Substring(
                        fileData.FileName.LastIndexOf(".", StringComparison.Ordinal) + 1,
                       fileData.FileName.Length - fileData.FileName.LastIndexOf(".", StringComparison.Ordinal) - 1).ToLower();

                    if (extensionType.Equals("pdf") || extensionType.Equals("docx") || extensionType.Equals("pptx"))
                    {
                        IsVisible = true;
                        File = fileData.DataArray;
                    }
                    else
                    {
                        SnackError("Archivos no adminitidos", "Error", TypeSnackBar.Top);
                    }
                }
                else
                {
                    SnackError("Debes dar permiso de obtener datos del dispositivo", "Permisos", TypeSnackBar.Top);
                }
                
            }
            catch(Exception ex)
            {

            }
        }
        private async void SaveDocumentCommandExecuted()
        {
            try
            {
                if(File == null)
                {
                    SnackError("Debe carga un archivo", "Error", TypeSnackBar.Top);
                    return;
                }
                if (SelectedOwner == null)
                {
                    SnackError("Debe seleccionar un propietario", "Error", TypeSnackBar.Top);
                    return;
                }
                if (string.IsNullOrWhiteSpace(Name))
                {
                    SnackError("Debe agregar un nombre al archivo", "Error", TypeSnackBar.Top);
                    return;
                }
                Show("Enviando documento...");
                var owner = (SelectedOwner.IdOwner == Guid.Empty) ? null : SelectedOwner.IdOwner;
                var admin = DbContext.Instance.GetAdministrator();
                var doc = new DocumentsModel();
                doc.FileDocument = File;
                doc.IdAdmin = admin.IdAdmin;
                doc.IdOwner = owner;
                doc.Name = Name;
                doc.Extensions = extensionType;
                doc.StringFile = null;
                var response = await client.Post<ListResponse, DocumentsModel>(doc, "documents/insdocument");
                if(response != null)
                {
                    if (response.Result && response.Count > 0)
                    {
                        SnackSucces("Se guardo correctamente", "KYA", TypeSnackBar.Top);
                        File = null;
                        Name = string.Empty;
                        extensionType = string.Empty;
                        SelectedOwner = null;
                        IsVisible = false;
                    }
                    else
                    {
                        SnackError(response.Message ,"Error", TypeSnackBar.Top);
                    }
                    LoadDocument();
                }
                else
                {
                    SnackError("hubo un error intentelo mas tarde", "Error", TypeSnackBar.Top);
                }
                Hide();
            }
            catch (Exception ex)
            {
                Hide();
            }
        }
        private async void SelectedDocumentCommandExecuted(DocumentsModel obj)
        {
            await NavigationPushModalAsync(new Views.Administrator.DocumentsPage(obj));
        }

        private async void DeleteDocumentCommandExecuted(DocumentsModel obj)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Eliminar", $"Desea eliminar el documento: {obj.Name}", "Si", "No");
            if (answer)
            {
                var response = await client.Delete<ListResponse>($"documents/deldocument?IdDocument={obj.IdDocument}");
                if (response != null)
                {
                    if (response.Result && response.Count > 0)
                    {
                        SnackSucces("Se elimino correctamente", "KYA", Helpers.TypeSnackBar.Top);
                        LoadDocument();
                    }
                    else
                    {
                        SnackError(response.Message, "Error", Helpers.TypeSnackBar.Top);
                    }
                }
                else
                {
                    SnackError("hubo un error intentelo mas tarde", "Error", Helpers.TypeSnackBar.Top);
                }
            }
        }
        #endregion
    }
}
