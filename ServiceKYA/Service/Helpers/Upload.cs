using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Service.Helpers
{
    public class Upload
    {
        private static bool WriteFile(MemoryStream stream, string folder, string name)
        {
            try
            {
                stream.Position = 0;
                var path = Path.Combine(folder, name);
                File.WriteAllBytes(path, stream.ToArray());
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static string ImagePath(byte[] fileArray)
        {
            try
            {
                var stream = new MemoryStream(fileArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = $"~/Image/";
                var folderFull = HttpContext.Current.Server.MapPath(folder);
                var response = WriteFile(stream, folderFull, file);
                if (response)
                {
                    return Path.Combine(folderFull, file);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        public static string FilePath(byte[] fileArray)
        {
            try
            {
                var stream = new MemoryStream(fileArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.pdf";
                var folder = $"~/Documents/";
                var folderFull = HttpContext.Current.Server.MapPath(folder);
                var response = WriteFile(stream, folderFull, file);
                if (response)
                {
                    return Path.Combine(folderFull, file);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}