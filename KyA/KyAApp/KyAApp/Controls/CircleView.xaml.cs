using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace KyAApp.Controls
{
    public partial class CircleView : ContentView
    {

        public static BindableProperty CircleColorProperty = BindableProperty.Create(nameof(CircleColor), typeof(Color), typeof(CircleView), Color.White);
        public static BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CircleView), Color.White);
        public static BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(int), typeof(CircleView), 14);
        public static BindableProperty FontProperty = BindableProperty.Create(nameof(Font), typeof(Font), typeof(CircleView), Font.Default);
        public static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(CircleView), 0.0, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var initialsView = bindable as CircleView;
            if (initialsView != null)
                initialsView.UpdateCornerRadius((double)newVal);
        });
        public static BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(CircleView), string.Empty,
        propertyChanged: (bindable, oldVal, newVal) =>
        {
            var initialsView = bindable as CircleView;
            if (initialsView != null)
                initialsView.UpdateTextWithName(newVal?.ToString());
        });

        public double CornerRadius
        {
            get
            {
                return (double)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }
        public string Name
        {
            get
            {
                return (string)GetValue(NameProperty);
            }
            set
            {
                SetValue(NameProperty, value);
            }
        }
        public int FontSize
        {
            get
            {
                return (int)GetValue(FontSizeProperty);
            }
            set
            {
                SetValue(FontSizeProperty, value);
            }
        }
        public Font Font
        {
            get
            {
                return (Font)GetValue(FontProperty);
            }
            set
            {
                SetValue(FontProperty, value);
            }
        }
        public Color CircleColor
        {
            get
            {
                return (Color)GetValue(CircleColorProperty);
            }
            set
            {
                SetValue(CircleColorProperty, value);
            }
        }
        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }
        public CircleView()
        {
            InitializeComponent();
            Container.BindingContext = this;
        }
        private void UpdateTextWithName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            var separateWords = name.Split(' ');
            if (separateWords.Length > 0)
            {
                var initialsArray = separateWords.Select(word => word[0].ToString().ToUpper()).ToArray(); // array of string of initials upper cased
                if (initialsArray.Length > 1)
                {
                    // grab the first and last
                    initialsArray = new string[2] { initialsArray[0], initialsArray[initialsArray.Length - 1] };
                }
                var initialsString = string.Join(string.Empty, initialsArray);
                InitialsLabel.Text = initialsString;
            }
            else
            {
                InitialsLabel.Text = string.Empty;
            }
        }

        /// <summary>
        /// Updates the corner radius.
        /// </summary>
        /// <param name="radius">Radius.</param>
        private void UpdateCornerRadius(double radius)
        {
            Circle.CornerRadius = radius;
        }
    }
}
