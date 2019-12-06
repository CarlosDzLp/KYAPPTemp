using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using KyAApp.DataBase;
using KyAApp.Helpers;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace KyAApp.ViewModels.Base
{
    public class BindableBase : INotifyPropertyChanged
    {
        #region Properties
        private byte[] _imageConvert;
        public byte[]ImageConvert
        {
            get { return _imageConvert; }
            set { SetProperty(ref _imageConvert, value); }
        }
        #endregion

        #region NotifyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(field, value)) { return false; }

            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
        #endregion

        private IDialogs dialogs = null;

        #region Constructor
        public BindableBase()
        {
            dialogs = DependencyService.Get<IDialogs>();
            ExitCommand = new Command(async (sender) =>
            {
                await PopupNavigation.Instance.PopAllAsync();
            });
            Exit = new Command(ExitCommanExecuted);
        }
        #endregion

        #region Dialogs
        public void Toast(string message)
        {
            dialogs.Toast(message);
        }
        public async void Show(string message)
        {
            await dialogs.Show(message);
        }
        public async void Hide()
        {
            await dialogs.Hide();
        }
        public async void SnackSucces(string message,string title, TypeSnackBar typeSnackBar)
        {
            await dialogs.SnackBarSucces(message,title, typeSnackBar);
        }
        public async void SnackError(string message,string title ,TypeSnackBar typeSnackBar)
        {
            await dialogs.SnackBarError(message,title, typeSnackBar);
        }
        #endregion

        #region Navigation
        public void MainPage(Page page)
        {
            App.Current.MainPage = page;
        }
        public async Task NavigationAsync(Page page)
        {
            App.Master.IsPresented = false;
            await App.Master.Detail.Navigation.PushAsync(page);
        }
        public async Task NavigationPushModalAsync(Page page)
        {
            App.Master.IsPresented = false;
            await App.Master.Detail.Navigation.PushModalAsync(page,true);
        }
        public async Task NavigationPopAsync()
        {
            App.Master.IsPresented = false;
            await App.Master.Detail.Navigation.PopModalAsync(true);
        }
        #endregion

        #region Command
        public ICommand ExitCommand { get; set; }
        public ICommand Exit { get; set; }
        #endregion
        protected virtual void ExitCommanExecuted()
        {
            
        }
    }
}
