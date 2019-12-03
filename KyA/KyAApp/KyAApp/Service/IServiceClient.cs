using System;
using System.Threading.Tasks;
using KyAApp.Models.Tokens;

namespace KyAApp.Service
{
    public interface IServiceClient
    {
        Task<T> Get<T>(string url);
        Task<T> Post<T,K>(K deserialice, string url);
        Task<T> Put<T,K>(K deserialice, string url);
        Task<T> Delete<T>(string url);
        Task<TokenRequest> Authenticate();
    }
}
