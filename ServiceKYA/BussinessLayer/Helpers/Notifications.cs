using BussinessEntities.DataBases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Helpers
{
    public class Notifications
    {
        private static Notifications _instance = null;
        public static Notifications Instance
        {
            get
            {
                // The first call will create the one and only instance.
                if (_instance == null)
                {
                    _instance = new Notifications();
                }

                // Every call afterwards will return the single instance created above.
                return _instance;
            }
        }
        private void NotifyAllUser(string message)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", $"Basic {Settings.ApiKey}");

            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                    + "\"app_id\": \"" + Settings.App_Id + "\","
                                                    + "\"contents\": {\"en\": \"" + message + "\"},"
                                                    + "\"included_segments\": [\"All\"]}");

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }

        private void NotifyUser(string PlayerID, string message)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                    + "\"app_id\": \"" + Settings.App_Id + "\","
                                                    + "\"contents\": {\"en\": \"" + message + "\"},"
                                                    + "\"include_player_ids\": [\"" + PlayerID + "\"]}");

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }
        //0=usuario, 1= propietario, 2=administrador
        public void NotificationsMessage(Guid? Id,int typeUser,string Message)
        {
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var queryFilter = dc.spSelNotifications(Id,typeUser).FirstOrDefault();
                    if (queryFilter != null)
                    {
                        NotifyUser(queryFilter.PlayerId, Message);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
