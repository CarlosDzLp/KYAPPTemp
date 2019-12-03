using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using KyAApp.DataBase;
using KyAApp.Helpers;
using KyAApp.Models.Authenticate;
using KyAApp.Models.Tokens;

namespace KyAApp.Service
{
    public class ServiceClient : IServiceClient,IDisposable
    {
        public async Task<T> Delete<T>(string url)
        {
            try
            {
                T deserializer = default(T);
                var token = await Authenticate();
                if(token != null)
                {
                    HttpClient client = new HttpClient();
                    var urltype = Settings.URL + url;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    var response = await client.DeleteAsync(urltype);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        deserializer = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseString);
                    }
                }               
                return deserializer;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public async Task<T> Get<T>(string url)
        {
            try
            {
                T deserializer = default(T);
                var token = await Authenticate();
                if(token != null)
                {
                    HttpClient client = new HttpClient();
                    var urlType = Settings.URL + url;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    var response = await client.GetAsync(urlType);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        deserializer = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseString);
                    }
                }               
                return deserializer;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public async Task<T> Post<T, K>(K deserialice, string url)
        {
            try
            {
                var token = await Authenticate();
                T deserializer = default(T);
                if(token != null)
                {
                    var serializer = Newtonsoft.Json.JsonConvert.SerializeObject(deserialice);
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.URL);
                    HttpContent content = new StringContent(serializer, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    var response = await client.PostAsync(url, content);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        deserializer = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseString);
                    }
                }             
                return deserializer;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public async Task<T> Put<T, K>(K deserialice, string url)
        {
            try
            {
                var token = await Authenticate();
                T deserializer = default(T);
                if(token != null)
                {
                    var serializer = Newtonsoft.Json.JsonConvert.SerializeObject(deserialice);
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.URL);
                    HttpContent content = new StringContent(serializer, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    var response = await client.PutAsync(url, content);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        deserializer = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseString);
                    }
                }             
                return deserializer;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public void Dispose()
        {
            this.Dispose();
        }

        public async Task<TokenRequest> Authenticate()
        {
            try
            {
                var deserializer = new TokenRequest();
                var devicetoken = DbContext.Instance.GetDeviceToken();
                var tokenModel = new AuthenticateModel
                {
                    User = "aplicaciondekyapp",
                    Password = "aplicaciondekyapp"
                };
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(tokenModel);
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.URL);
                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("authenticate/aouth", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    deserializer = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(responseString);
                    return deserializer;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
