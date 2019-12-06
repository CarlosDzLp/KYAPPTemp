using System;
using System.Collections.Generic;
using FormsToolkit;
using KyAApp.Helpers;
using Xamarin.Forms;

namespace KyAApp.Views.Administrator
{
    public partial class DocumentsPage : ContentPage
    {
        public DocumentsPage(Models.Documents.DocumentsModel obj)
        {
            InitializeComponent();
            pdf.IsPdf = true;
            pdf.Uri = obj.StringFile;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingService.Current.SendMessage(MessageKeys.StatusBar, true);
        }
    }
}
