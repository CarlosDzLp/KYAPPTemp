using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace KyAApp.Converters
{
    public class ImageConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var file = value as byte[];
            if (file == null)
            {
                return "user";
            }
            else
            {
                ImageSource image = null;
                var result = ImageSource.FromStream(
                    () => new MemoryStream(file));
                image = result;

                return image;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
