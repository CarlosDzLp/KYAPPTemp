using System;
using System.Collections.Generic;
using KyAApp.Helpers;
using KyAApp.Models;
using KyAApp.Models.Rooms;
using KyAApp.Service;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator.Popup
{
    public partial class UpdateRoomAdminPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        ServiceClient client = new ServiceClient();
        private RoomsModel Room { get; set; }
        public UpdateRoomAdminPopup(RoomsModel room)
        {
            InitializeComponent();
            this.Room = room;
            lblHeader.Text = room.Name;
            txtname.Text = room.Name;
            txtprice.Text = room.Price.ToString();
            btnupdateroom.Clicked += Btnupdateroom_Clicked;
        }

        private async void Btnupdateroom_Clicked(object sender, EventArgs e)
        {
            var snack = DependencyService.Get<IDialogs>();
            if(!string.IsNullOrWhiteSpace(txtname.Text))
            {
                if(!string.IsNullOrWhiteSpace(txtprice.Text))
                {
                    //"Idroom": "267f42d8-eada-4768-8ce6-3fca35bee501",
                    //"IdOwner": "fa7de544-cc60-4106-b19b-080f12f20600",
                    //"IdAdmin": "2b3bea03-af6d-4b33-b8cc-c39dde99e210",
                    //"Name": "sample string 1",
                    //"Price": 1.0
                    Room.Idroom = Room.IdRoom;
                    Room.Name = txtname.Text;
                    Room.Price = Convert.ToDouble(txtprice.Text);
                    var response = await client.Put<ListResponse, RoomsModel>(Room, "room/updroom");
                    if(response != null)
                    {
                        if(response.Result && response.Count > 0)
                        {
                            await snack.SnackBarSucces("Se actualizo correctamente", "KyA", TypeSnackBar.Top);
                            await PopupNavigation.Instance.PopAllAsync();
                            MessagingCenter.Send<UpdateRoomAdminPopup>(this, "loadupdateroom");
                        }
                        else
                        {
                            await snack.SnackBarError(response.Message, "Error", TypeSnackBar.Top);
                        }
                    }
                    else
                    {
                        await snack.SnackBarError("Hubo un error intentelo mas tarde", "Error", TypeSnackBar.Top);
                    }
                }
                else
                {
                    await snack.SnackBarError("Ingrese un precio", "Error", TypeSnackBar.Top);
                }
            }
            else
            {
                await snack.SnackBarError("Ingrese un nombre al cuarto", "Error", TypeSnackBar.Top);
            }
        }
        private async void tappedImageclose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}
