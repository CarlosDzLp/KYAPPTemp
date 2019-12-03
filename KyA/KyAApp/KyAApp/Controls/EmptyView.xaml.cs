using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace KyAApp.Controls
{
    public partial class EmptyView : ContentView
    {
        public static BindableProperty LoadingCommandProperty =
            BindableProperty.Create(
                nameof(LoadingCommand),
                typeof(ICommand),
                typeof(EmptyView),
                null);

        public ICommand LoadingCommand
        {
            get
            {
                return (ICommand)this.GetValue(LoadingCommandProperty);
            }
            set
            {
                this.SetValue(LoadingCommandProperty, value);
            }
        }


        public static readonly BindableProperty TextButtonProperty =
        BindableProperty.Create(
          nameof(TextButton),
         typeof(string),
        typeof(EmptyView),
        string.Empty,
        propertyChanged:UpdatebuttonText);

        public string TextButton
        {
            get
            {
                return (string)this.GetValue(TextButtonProperty);
            }
            set
            {
                this.SetValue(TextButtonProperty, value);
            }
        }

        public EmptyView()
        {
            InitializeComponent();
            btnEmpty.Clicked += BtnEmpty_Clicked;
        }

        private static void UpdatebuttonText(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (EmptyView)bindable;
            control.btnEmpty.Text = newValue as string;
        }
        //private static void UpdatebuttonText(string newValue)
        //{
            //btnEmpty.Text = newValue;
        //}

        private void BtnEmpty_Clicked(object sender, EventArgs e)
        {
            LoadingCommand?.Execute(null);
        }
    }
}
