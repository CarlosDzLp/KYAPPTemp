using KyAApp.Helpers;
using KyAApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KyAApp.DataBase
{
    public class DbContext
    {
        #region Singleton
        private static DbContext _instance = null;
        public static DbContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbContext();
                }
                return _instance;
            }
        }
        #endregion

        #region Constructor
        private readonly SQLiteConnection connection;
        public DbContext()
        {
            try
            {
                var dbPath = DependencyService.Get<IStringPath>().FilePath();
                connection = new SQLiteConnection(dbPath, true);
                connection.CreateTable<DeviceToken>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeviceToken
        public void InsertDeviceToken(string playerId,string pushToken)
        {
            try
            {
                connection.DeleteAll<DeviceToken>();
                var device = new DeviceToken();
                device.Id = 1;
                device.PlayerId = playerId;
                device.PushToken = pushToken;
                connection.Insert(device);
            }
            catch(Exception ex)
            {

            }
        }
        public DeviceToken GetDeviceToken()
        {
            try
            {
                return connection.Table<DeviceToken>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
