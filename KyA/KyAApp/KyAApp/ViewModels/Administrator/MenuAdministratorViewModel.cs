using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using KyAApp.DataBase;
using KyAApp.Models;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;

namespace KyAApp.ViewModels.Administrator
{
    public class MenuAdministratorViewModel : BindableBase
    {
        #region Properties
        private ObservableCollection<MenuModel> _listMenu;
        public ObservableCollection<MenuModel>ListMenu
        {
            get { return _listMenu; }
            set { SetProperty(ref _listMenu, value); }
        }
        #endregion

        #region Constructor
        public MenuAdministratorViewModel()
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
                ListMenu.Add(new MenuModel { Icon = "owner", ID = 0, Title = "Propietarios", BackgroundColor = "#004D40" });
                ListMenu.Add(new MenuModel { Icon = "service", ID = 1, Title = "Servicios", BackgroundColor = "#E65100" });
                ListMenu.Add(new MenuModel { Icon = "room", ID = 2, Title = "Cuartos", BackgroundColor = "#DD2C00" });
                ListMenu.Add(new MenuModel { Icon = "Asignar Servicios", ID = 3, Title = "Asignar Servicios a cuartos", BackgroundColor = "#311B92" });
                ListMenu.Add(new MenuModel { Icon = "assignmentroom", ID = 4, Title = "Asignar cuartos", BackgroundColor = "#311B92" });
                ListMenu.Add(new MenuModel { Icon = "message", ID = 5, Title = "Mensajes", BackgroundColor = "#004D40" });
                ListMenu.Add(new MenuModel { Icon = "", ID = 6, Title = "Cerrar sesion", BackgroundColor = "#E65100" });
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
                if(menu.ID == 0)
                {
                    await NavigationAsync(new Views.Administrator.UserAdminPage());
                }
                else if(menu.ID == 1)
                {
                    await NavigationAsync(new Views.Administrator.ServiceAdminPage());
                }
                else if(menu.ID == 2)
                {
                    await NavigationAsync(new Views.Administrator.RoomAdminPage());
                }
                else if (menu.ID == 3)
                {
                    await NavigationAsync(new Views.Administrator.RoomServiceAdminPage());
                }
                else if(menu.ID == 6)
                {
                    DbContext.Instance.DeleteAdministrator();
                    MainPage(new Views.Session.LoginPage());
                }
            }
            catch(Exception ex)
            {

            }
        }
        #endregion
    }
}
