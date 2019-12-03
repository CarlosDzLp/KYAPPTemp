using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace KyAApp.Controls
{
    public class CustomListView : ListView
    {
        public static BindableProperty SelectedItemCommandProperty =
            BindableProperty.Create(
                nameof(SelectedItemCommand),
                typeof(ICommand),
                typeof(CustomListView),
                null);

        public ICommand SelectedItemCommand
        {
            get
            {
                return (ICommand)this.GetValue(SelectedItemCommandProperty);
            }
            set
            {
                this.SetValue(SelectedItemCommandProperty, value);
            }
        }



        public CustomListView() : this(ListViewCachingStrategy.RecycleElement)
        {
        }

        public CustomListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
            this.ItemTapped += OnItemTapped;
        }
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                SelectedItemCommand?.Execute(e.Item);
                SelectedItem = null;
            }
        }

        public void ScrollToFirst()
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    if (ItemsSource != null && ItemsSource.Cast<object>().Count() > 0)
                    {
                        var msg = ItemsSource.Cast<object>().FirstOrDefault();
                        if (msg != null)
                        {
                            ScrollTo(msg, ScrollToPosition.Start, false);
                        }

                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }

            });
        }

        public void ScrollToLast()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    if (ItemsSource != null && ItemsSource.Cast<object>().Count() > 0)
                    {
                        var msg = ItemsSource.Cast<object>().LastOrDefault();
                        if (msg != null)
                        {
                            ScrollTo(msg, ScrollToPosition.End, false);
                        }

                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }

            });
        }
    }
}
