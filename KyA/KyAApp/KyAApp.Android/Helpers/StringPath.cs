
using KyAApp.Droid.Helpers;
using KyAApp.Helpers;
using System;
using System.Diagnostics;
using Xamarin.Forms;

[assembly: Dependency(typeof(StringPath))]
namespace KyAApp.Droid.Helpers
{
    public class StringPath : IStringPath
    {
        public string FilePath()
        {
            try
            {
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                return System.IO.Path.Combine(path, "kyaapp.db3");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}