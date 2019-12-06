using System;
using Xamarin.Forms;

namespace KyAApp.Controls
{
    public class CustomPdf : WebView
    {
        public static readonly BindableProperty UriProperty = BindableProperty.Create(propertyName: "Uri",
                returnType: typeof(string),
                declaringType: typeof(CustomPdf),
                defaultValue: default(string));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public static readonly BindableProperty IsPdfProperty = BindableProperty.Create(propertyName: "IsPdf",
        returnType: typeof(bool),
        declaringType: typeof(CustomPdf),
        defaultValue: default(bool));

        public bool IsPdf
        {
            get { return (bool)GetValue(IsPdfProperty); }
            set { SetValue(IsPdfProperty, value); }
        }
    }
}
