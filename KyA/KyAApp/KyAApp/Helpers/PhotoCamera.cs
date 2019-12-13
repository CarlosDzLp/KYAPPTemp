using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace KyAApp.Helpers
{
    public static class PhotoCamera
    {
        public static async Task<byte[]> TakePhoto()
        {
            if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return null;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
                return null;
            byte[] img = null;
            var stream = file.GetStream();
            file.Dispose();
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                img = ms.ToArray();
            }
            return img;
        }

        public static async Task<byte[]> PickPhoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return null;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });

            if (file == null)
                return null;

            byte[] img = null;
            var stream = file.GetStream();
            file.Dispose();
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                img = ms.ToArray();
            }
            return img;
        }
    }
}
