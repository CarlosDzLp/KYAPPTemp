using System;
using System.Collections.Generic;
using KyAApp.DataBase;
using KyAApp.Helpers;
using KyAApp.Models;
using KyAApp.Models.MonthlyPayment;
using KyAApp.Models.Surcharges;
using KyAApp.Service;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class SurchargesPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        ServiceClient client = new ServiceClient();
        private MonthlyPaymentModel Monthly { get; set; }
        public SurchargesPage(MonthlyPaymentModel obj)
        {
            InitializeComponent();
            this.Monthly = obj;
            btnsave.Clicked += Btnsave_Clicked;
        }

        private async void Btnsave_Clicked(object sender, EventArgs e)
        {
            var snack = DependencyService.Get<IDialogs>();
            if(!string.IsNullOrWhiteSpace(txtdescription.Text))
            {
                if(!string.IsNullOrWhiteSpace(txtprice.Text))
                {
                    var admin = DbContext.Instance.GetAdministrator();
                    var sur = new SurchargesModel();
                    sur.Description = txtdescription.Text;
                    sur.IdAdmin = admin.IdAdmin;
                    sur.IdMothly = Monthly.IdMonthly;
                    sur.IdOwner = Monthly.IdOwner;
                    sur.IdRoom = Monthly.IdRoom;
                    sur.Price = Convert.ToDouble(txtprice.Text);
                    sur.UserId = Monthly.UserId;
                    var response = await client.Post<ListResponse, SurchargesModel>(sur, "surcharges/inssurcharges");
                    if(response != null)
                    {
                        if(response.Result && response.Count > 0)
                        {
                            MessagingCenter.Send<SurchargesPage>(this, "loadMonthly");
                            await PopupNavigation.Instance.PopAllAsync();
                            await snack.SnackBarSucces("se agrego correctamente", "KyA", TypeSnackBar.Top);
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
                await snack.SnackBarError("Ingrese una descripion", "Error", TypeSnackBar.Top);
            }
        }

        private async void ExitCommand(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}
