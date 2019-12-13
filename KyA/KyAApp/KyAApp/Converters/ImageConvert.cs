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
            var file = value as string;
            if (file == string.Empty || string.IsNullOrWhiteSpace(file))
            {
                return "user";
            }
            else
            {
                if (file.Contains("http://rentapp.carlosdiaz.com.elpumavp.arvixevps.com/Image"))
                {
                    var img = file.Substring(59);
                    if(string.IsNullOrWhiteSpace(img))
                    {
                        return "user";
                    }
                    else
                    {
                        return file;
                    }                
                }
                else
                {
                    var img = System.Convert.FromBase64String(file);
                    ImageSource image = null;
                    var result = ImageSource.FromStream(
                        () => new MemoryStream(img));
                    image = result;

                    return image;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
