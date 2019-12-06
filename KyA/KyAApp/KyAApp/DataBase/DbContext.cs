using KyAApp.Helpers;
using KyAApp.Models;
using KyAApp.Models.Administrator;
using KyAApp.Models.Owner;
using KyAApp.Models.User;
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
                connection.CreateTable<AdministratorModel>();
                connection.CreateTable<UserModel>();
                connection.CreateTable<OwnerModel>();
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
        public void DeleteDeviceToken()
        {
            try
            {
                connection.DeleteAll<DeviceToken>();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Administrator
        public void InsertAdministrator(AdministratorModel admin)
        {
            try
            {
                connection.DeleteAll<AdministratorModel>();
                connection.Insert(admin);
            }
            catch(Exception ex)
            {

            }
        }
        public AdministratorModel GetAdministrator()
        {
            try
            {
                return connection.Table<AdministratorModel>().FirstOrDefault();              
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteAdministrator()
        {
            try
            {
                connection.DeleteAll<AdministratorModel>();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region User
        public UserModel GetUser()
        {
            try
            {
                return connection.Table<UserModel>().FirstOrDefault();
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public void DeleteUser()
        {
            try
            {
                connection.DeleteAll<UserModel>();
            }
            catch (Exception ex)
            {
                
            }
        }
        public void InsertUser(UserModel user)
        {
            try
            {
                connection.DeleteAll<UserModel>();
                connection.Insert(user);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Owner
        public OwnerModel GetOwner()
        {
            try
            {
                return connection.Table<OwnerModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void DeleteOwner()
        {
            try
            {
                connection.DeleteAll<OwnerModel>();
            }
            catch (Exception ex)
            {

            }
        }
        public void InsertOwner(OwnerModel user)
        {
            try
            {
                connection.DeleteAll<OwnerModel>();
                connection.Insert(user);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
