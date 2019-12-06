using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using KyAApp.DataBase;
using KyAApp.Models;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;

namespace KyAApp.ViewModels.Owner
{
    public class MenuOwnerViewModel : BindableBase
    {
        #region Properties
        private ObservableCollection<MenuModel> _listMenu;
        public ObservableCollection<MenuModel> ListMenu
        {
            get { return _listMenu; }
            set { SetProperty(ref _listMenu, value); }
        }
        #endregion

        #region Constructor
        public MenuOwnerViewModel()
        {
            LoadMenu();
            SelectedMenuCommand = new Command<MenuModel>(SelectedMenuCommandExecuted);
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            try
            {
                ListMenu = new ObservableCollection<MenuModel>();
                ListMenu.Add(new MenuModel { Icon = "", ID = 1, Title = "Cuartos", BackgroundColor = "#E65100" });
                ListMenu.Add(new MenuModel { Icon = "", ID = 2, Title = "Servicios", BackgroundColor = "#E65100" });
                ListMenu.Add(new MenuModel { Icon = "", ID = 3, Title = "Servicios a cuartos", BackgroundColor = "#E65100" });
                ListMenu.Add(new MenuModel { Icon = "", ID = 4, Title = "Mis Usuarios", BackgroundColor = "#E65100" });
                ListMenu.Add(new MenuModel { Icon = "", ID = 5, Title = "Cerrar sesion", BackgroundColor = "#E65100" });
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Command
        public ICommand SelectedMenuCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async void SelectedMenuCommandExecuted(MenuModel menu)
        {
            try
            {
                if(menu.ID ==1)
                {
                    //cuartos
                    await NavigationAsync(new Views.Owner.RoomOwnerPage());
                }
                else if(menu.ID == 2)
                {
                    //servicios
                    await NavigationAsync(new Views.Owner.ServiceOwnerPage());
                }
                else if(menu.ID == 3)
                {
                    //servicios a cuartos
                    await NavigationAsync(new Views.Owner.RoomServiceOwnerPage());
                }
                else if (menu.ID == 5)
                {
                    DbContext.Instance.DeleteOwner();
                    MainPage(new Views.Session.LoginPage());
                }
                else if(menu.ID == 4)
                {
                    await NavigationAsync(new Views.Owner.MisUsuariosPage());
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
