using BussinessEntities.DataBases;
using BussinessLayer.Helpers;
using BussinessLayer.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Owner;
using ViewModels.Response;

namespace BussinessLayer.Owner
{
    public class BLOwner
    {
        public async Task<Response<OwnerVM>> DoLogin(string user,string password,string PlayerId,string PushToken)
        {
            Response<OwnerVM> response = new Response<OwnerVM>();
            try
            {
                using(var dc = new RentAppEntities())
                {
                    var query = dc.spSelOwnerDoLogin(user, password).Where(c => c.StatusOwner == true).FirstOrDefault();
                    if(query != null)
                    {
                        var o = new OwnerVM();
                        o.Address=query.AddressOwner;
                        o.DateCreated=query.DateCreatedOwner;
                        o.DateModified=query.DateModifiedOwner;
                        o.Icon=query.IconOwner;
                        o.IconString=query.IconStringOwner;
                        o.IdAdmin=query.IdAdmin;
                        o.IdOwner=query.IdOwner;
                        o.Name=query.NameOwner;
                        o.Password=query.PasswordOwner;
                        o.Phone=query.PhoneOwner;
                        o.Status=query.StatusOwner;
                        o.User=query.UserOwner;                       
                        response.Count = 1;
                        response.Message = null;
                        response.Result = o;
                        await BLNotifications.InsertNotification(o.IdOwner, PlayerId, PushToken, 1);
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

        public async Task<Response<bool>> InsertOwner(OwnerInsVM owner)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var query = dc.spInsOwner(
                        owner.IdAdmin, 
                        owner.Name, 
                        owner.Address, 
                        owner.Phone, 
                        owner.User, 
                        owner.Icon, 
                        owner.IconString, 
                        owner.Password);
                    if (query == -1)
                    {
                        
                        response.Count = 1;
                        response.Message = null;
                        response.Result = true;
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No se pudo guardar el propietario";
                        response.Result = false;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Count = 0;
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteOwner(Guid? idOwner,Guid? idAdmin,string NameOwner)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var query = dc.spDelOwner(idOwner);
                    if (query == -1)
                    {
                        response.Count = 1;
                        response.Message = null;
                        response.Result = true;                        
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No se pudo eliminar el propietario";
                        response.Result = false;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Count = 0;
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }

        public async Task<Response<bool>> UpdateOwner(OwnerUpdVM o)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var query = dc.spUpdOwner(o.IdOwner, o.Name, o.Address, o.Phone, o.User, o.Icon, o.IconString, o.Password, o.Status);
                    if (query == -1)
                    {
                        response.Count = 1;
                        response.Message = null;
                        response.Result = true;
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No se pudo actualizar el propietario";
                        response.Result = false;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Count = 0;
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }

        public async Task<Response<List<OwnerVM>>> SelectOwnerAll(bool? status)
        {
            Response<List<OwnerVM>> response = new Response<List<OwnerVM>>();
            List<OwnerVM> list = new List<OwnerVM>();
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var query = dc.spSelOwner().ToList();
                    if (status != null)
                    {
                        var queryFilter = dc.spSelOwner().Where(c => c.StatusOwner == true).ToList();
                        query = queryFilter;
                    }                   
                    if (query.Count > 0)
                    {
                        foreach(var item in query)
                        {
                            var o = new OwnerVM();
                            o.Address=item.AddressOwner;
                            o.DateCreated=item.DateCreatedOwner;
                            o.DateModified=item.DateModifiedOwner;
                            o.Icon=item.IconOwner;
                            o.IconString=item.IconStringOwner;
                            o.IdAdmin=item.IdAdmin;
                            o.IdOwner=item.IdOwner;
                            o.Name=item.NameOwner;
                            o.Password=item.PasswordOwner;
                            o.Phone=item.PhoneOwner;
                            o.Status=item.StatusOwner;
                            o.User=item.UserOwner;
                            list.Add(o);
                        }
                        response.Count = list.Count;
                        response.Message = null;
                        response.Result = list;                        
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
