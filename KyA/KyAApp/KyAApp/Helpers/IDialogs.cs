using System;
using System.Threading.Tasks;

namespace KyAApp.Helpers
{
    public interface IDialogs
    {
        void Toast(string message);
        Task SnackBarSucces(string message,string title, TypeSnackBar typeSnackBar);
        Task SnackBarError(string message,string title, TypeSnackBar typeSnackBar);
        Task Show(string message);
        Task Hide();
    }
    public enum TypeSnackBar
    {
        Top,
        Bottom
    }
}
