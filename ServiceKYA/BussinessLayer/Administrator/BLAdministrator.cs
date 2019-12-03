using BussinessEntities.DataBases;
using BussinessLayer.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Administrator;
using ViewModels.Response;

namespace BussinessLayer.Administrator
{
    public class BLAdministrator
    {
        public async Task<Response<AdministratorVM>> DoLogin(string user,string password,string PlayerId,string PushToken)
        {
            Response<AdministratorVM> response = new Response<AdministratorVM>();
            try
            {
                using(var dc = new RentAppEntities())
                {
                    var query = dc.spSelAdministratorDoLogin(user, password).Where(c => c.StatusAdmin == true).FirstOrDefault();
                    if(query != null)
                    {
                        var ad = new AdministratorVM();
                        ad.Address=query.AdddresAdmin;
                        ad.DateCreated=query.DateCreatedAdmin;
                        ad.DateModified=query.DateModifiedAdmin;
                        ad.Icon=query.IconAdmin;
                        ad.IconString = query.IconStringAdmin;
                        ad.IdAdmin=query.IdAdmin;
                        ad.Name=query.NameAdmin;
                        ad.Password=query.PasswordAdmin;
                        ad.Phone=query.PhoneAdmin;
                        ad.Status=query.StatusAdmin;
                        ad.User=query.UserAdmin;
                        response.Count = 1;
                        response.Message = null;
                        response.Result = ad;
                        await BLNotifications.InsertNotification(ad.IdAdmin, PlayerId, PushToken, 2);
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No hay datos";
                        response.Result = null;
                    }
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Count = 0;
                response.Message = ex.Message;
                response.Result = null;
                return response;
            }
        }

        public async Task<Response<AdministratorVM>> UserRoot(string user, string password)
        {
            Response<AdministratorVM> response = new Response<AdministratorVM>();
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var query = dc.spSelAdministratorDoLogin(user, password).Where(c => c.StatusAdmin == true).FirstOrDefault();
                    if (query != null)
                    {
                        var ad = new AdministratorVM();
                        ad.Address = query.AdddresAdmin;
                        ad.DateCreated = query.DateCreatedAdmin;
                        ad.DateModified = query.DateModifiedAdmin;
                        ad.Icon = query.IconAdmin;
                        ad.IconString = query.IconStringAdmin;
                        ad.IdAdmin = query.IdAdmin;
                        ad.Name = query.NameAdmin;
                        ad.Password = query.PasswordAdmin;
                        ad.Phone = query.PhoneAdmin;
                        ad.Status = query.StatusAdmin;
                        ad.User = query.UserAdmin;
                        response.Count = 1;
                        response.Message = null;
                        response.Result = ad;
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No hay datos";
                        response.Result = null;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Count = 0;
                response.Message = ex.Message;
                response.Result = null;
                return response;
            }
        }
    }
}
