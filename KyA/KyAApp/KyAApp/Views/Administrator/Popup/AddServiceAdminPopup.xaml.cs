using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using KyAApp.Controls;
using KyAApp.DataBase;
using KyAApp.Helpers;
using KyAApp.Models;
using KyAApp.Models.Owner;
using KyAApp.Models.Service;
using KyAApp.Service;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator.Popup
{
    public partial class AddServiceAdminPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        ServiceClient client = new ServiceClient();
        public ObservableCollection<OwnerModel> ListOwner { get; set; }
        public OwnerModel Owner { get; set; }

        private byte[] ImageConvert { get; set; }
        public AddServiceAdminPopup()
        {
            InitializeComponent();
            imgservice.Source = "user";
            LoadOwner();
            btnsaveservice.Clicked += Btnsaveservice_Clicked;
            combopicker.SelectedIndexChanged += Combopicker_SelectedIndexChanged;
        }

        private void Combopicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as CustomPicker;
            Owner = (OwnerModel)picker.SelectedItem;           
        }

        private async void Btnsaveservice_Clicked(object sender, EventArgs e)
        {
            var snack = DependencyService.Get<IDialogs>();
            if (!string.IsNullOrWhiteSpace(txtname.Text))
            {
                if (!string.IsNullOrWhiteSpace(txtprice.Text))
                {
                    if (Owner != null)
                    {
                        var admin = DbContext.Instance.GetAdministrator();
                        //"Icon": "QEA=",
                        //"IconString": "sample string 2"
                        var service = new ServiceModel();
                        service.IdOwner = Owner.IdOwner;
                        service.IdAdmin = admin.IdAdmin;
                        service.Name = txtname.Text;
                        service.Price = Convert.ToDouble(txtprice.Text);
                        service.IconString = string.Empty;
                        service.Icon = ImageConvert;
                        var response = await client.Post<ListResponse, ServiceModel>(service, "service/insservice");
                        if (response != null)
                        {
                            if(response.Result && response.Count > 0)
                            {
                                await PopupNavigation.Instance.PopAllAsync();
                                MessagingCenter.Send<AddServiceAdminPopup>(this, "addservice");
                                await snack.SnackBarSucces("Se guardo correctamente", "KyA", TypeSnackBar.Top);
                            }
                            else
                            {
                                await snack.SnackBarError(response.Message, "Error", TypeSnackBar.Top);
                            }
                        }
                        else
                        {
                            await snack.SnackBarError("Ingrese el propietario del servicio", "Error", TypeSnackBar.Top);
                        }
                    }
                    else
                    {
                        await snack.SnackBarError("Ingrese el precio del servicio", "Error", TypeSnackBar.Top);
                    }
                }
                else
                {
                    await snack.SnackBarError("Ingrese un nombre del servicio", "Error", TypeSnackBar.Top);
                }
            }
        }
        private async void ExitImageTapped(object sender, EventArgs e)
        {
            await PopupNavigation.PopAllAsync();
        }

        string extensionType = "";
        private async void ImageTappedService(object sender, EventArgs e)
        {
            //
            var snack = DependencyService.Get<IDialogs>();
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
                var upper = extensionType.ToUpper();
                extensionType = upper;
                if (extensionType.Equals("JPG") || extensionType.Equals("PNG") || extensionType.Equals("JPEG"))
                {

                    ImageConvert = fileData.DataArray;
                }
                else
                {
                    await snack.SnackBarError("Archivos no adminitidos", "Error", TypeSnackBar.Top);
                }
            }
            else
            {
                await snack.SnackBarError("Habilita el permiso", "Error", TypeSnackBar.Top);
            }
            if(ImageConvert == null)
            {
                imgservice.Source = "user";
            }
            else
            {

                var result = ImageSource.FromStream(
                    () => new MemoryStream(ImageConvert));
                imgservice.Source = result;
            }
        }


        private async void LoadOwner()
        {
            try
            {
                ListOwner = new ObservableCollection<OwnerModel>();
                ListOwner.Clear();
                var response = await client.Get<ListOwnerModel>($"administrator/selowner?status={true}");
                if (response != null)
                {
                    if (response.Result != null && response.Count > 0)
                    {
                        foreach (var item in response.Result)
                        {
                            var img = (item.IconString == null || item.IconString == string.Empty) ? "user" : "http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image/" + item.IconString;
                            item.IconString = img;
                            ListOwner.Add(item);
                        }
                    }
                }
                combopicker.ItemsSource = ListOwner;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
