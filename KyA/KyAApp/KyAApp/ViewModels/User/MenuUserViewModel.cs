using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using KyAApp.DataBase;
using KyAApp.Models;
using KyAApp.ViewModels.Base;
using Xamarin.Forms;

namespace KyAApp.ViewModels.User
{
    public class MenuUserViewModel : BindableBase
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
        public MenuUserViewModel()
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
                if (menu.ID == 5)
                {
                    DbContext.Instance.DeleteUser();
                    MainPage(new Views.Session.LoginPage());
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
