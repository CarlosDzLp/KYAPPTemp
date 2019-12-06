using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KyAApp.DataBase;
using Com.OneSignal;
using DLToolkit.Forms.Controls;

namespace KyAApp
{
    public partial class App : Application
    {
        public static MasterDetailPage Master { get; set; }

        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            var admin = DbContext.Instance.GetAdministrator();
            var user = DbContext.Instance.GetUser();
            var owner = DbContext.Instance.GetOwner();
            if(admin != null)
            {
                MainPage = new Views.Administrator.MasterDetailAdministrator();
            }
            else if(user != null)
            {
                MainPage = new Views.User.MasterDetailUser();
            }
            else if(owner != null)
            {
                MainPage = new Views.Owner.MasterDetailOwner();
            }
            else
            {
                MainPage = new Views.Session.LoginPage();
            }          
            OneSignal.Current.IdsAvailable(IdsAvailable);
        }

        public async void IdsAvailable(string playerId, string pushToken)
        {
            DbContext.Instance.InsertDeviceToken(playerId, pushToken);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        //AudioRecorderService recorder;
        //AudioPlayer player;
        //public ICommand LongCommand { get; set; }
        //public ICommand PlayCommand { get; set; }
        //public MainPageViewModel()
        //{
        //    recorder = new AudioRecorderService
        //    {
        //        StopRecordingAfterTimeout = true,
        //        TotalAudioTimeout = TimeSpan.FromMinutes(1),
        //        AudioSilenceTimeout = TimeSpan.FromSeconds(5)
        //    };
        //    player = new AudioPlayer();
        //    LongCommand = new Command(LongCommandExecuted);
        //    PlayCommand = new Command(PlayCommandExecuted);
        //}
        //byte[] audioByte = null;
        //private void PlayCommandExecuted(object obj)
        //{
        //    var filePath = recorder.FilePath;
        //    if (filePath != null)
        //    {
        //        var audioStream = recorder.GetAudioFileStream();
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            audioStream.CopyTo(ms);
        //            audioByte = ms.ToArray();
        //        }
        //        player.Play(filePath);
        //    }
        //}

        //private async void LongCommandExecuted(object obj)
        //{
        //    try
        //    {
        //        var val = (bool)obj;
        //        if (val)
        //        {
        //            var audioRecordTask = await recorder.StartRecording();
        //            await audioRecordTask;
        //            //iniciar grabacion
        //        }
        //        else
        //        {
        //            await recorder.StopRecording();
        //            //detener grabacion
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
