using System;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using KyAApp.Views.Administrator.Popup;
using KyAApp.Service;

namespace KyAApp.ViewModels.Administrator
{
    public class AssignmentRoomUserPageViewModel : BindableBase
    {
        ServiceClient client = new ServiceClient();
        #region Constructor
        public AssignmentRoomUserPageViewModel()
        {
            AddAssignmentRoomUserCommand = new Command(AddAssignmentRoomUserCommandExecuted);
            LoadassignmentRoomUser();
            MessagingCenter.Subscribe<AddAssignmentRoomUserPopup>(this, "loadassignroomuser", (e) => { LoadassignmentRoomUser(); });
        }
        #endregion

        #region Methods
        private async void LoadassignmentRoomUser()
        {
            try
            {
                var response = await client.Get<string>("");
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Command
        public ICommand AddAssignmentRoomUserCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void AddAssignmentRoomUserCommandExecuted()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new Views.Administrator.Popup.AddAssignmentRoomUserPopup());
            }
            catch(Exception ex)
            {
            }
        }
        #endregion
    }
}
