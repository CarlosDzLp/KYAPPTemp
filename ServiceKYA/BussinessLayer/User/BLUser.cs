using BussinessEntities.DataBases;
using BussinessLayer.Helpers;
using BussinessLayer.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Response;
using ViewModels.User;

namespace BussinessLayer.User
{
    public class BLUser
    {
        public async Task<Response<UserVM>> DoLogin(string user,string password, string PlayerId,string PushToken)
        {
            Response<UserVM> response = new Response<UserVM>();
            try
            {
                using(var dc = new RentAppEntities())
                {
                    var queryFilter = dc.spSelUserDoLogin(user, password).ToList();
                    var query = queryFilter.Where(c => c.StatusUser == true).FirstOrDefault();
                    if(query != null)
                    {
                        var usr = new UserVM();
                        usr.Address = query.AddressUser;
                        usr.DateCreated = query.DateCreatedUser;
                        usr.DateModified = query.DateModifiedUser;
                        usr.IconString = query.IconStringUser;
                        usr.Icon = query.IconUser;
                        usr.IdAdmin = query.IdAdmin;
                        usr.IdOwner = query.IdOwner;
                        usr.Name = query.NameUser;
                        usr.Password = query.PasswordUser;
                        usr.Phone = query.PhoneUser;
                        usr.Status = query.StatusUser;
                        usr.UserId = query.UserId;
                        usr.User = query.UserUser;
                        response.Count = 1;
                        response.Message = null;
                        response.Result = usr;
                        await BLNotifications.InsertNotification(usr.UserId, PlayerId, PushToken, 0);
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
    
        public async Task<Response<List<UserVM>>> GetUser(Guid? idOwner,bool? status)
        {
            Response<List<UserVM>> response = new Response<List<UserVM>>();
            List<UserVM> list = new List<UserVM>();
            try
            {
                using(var dc = new RentAppEntities())
                {
                    var query = dc.spSelUser(idOwner).ToList();
                    if(query.Count > 0)
                    {
                        if (status != null)
                        {
                            var queryFilter = query.Where(c => c.StatusUser == status).ToList();
                            query = queryFilter;
                        }
                        foreach (var item in query)
                        {
                            var usr = new UserVM();
                            usr.Address = item.AddressUser;
                            usr.DateCreated = item.DateCreatedUser;
                            usr.DateModified = item.DateModifiedUser;
                            usr.IconString = item.IconStringUser;
                            usr.Icon = item.IconUser;
                            usr.IdAdmin = item.IdAdmin;
                            usr.IdOwner = item.IdOwner;
                            usr.Name = item.NameUser;
                            usr.Password = item.PasswordUser;
                            usr.Phone = item.PhoneUser;
                            usr.Status = item.StatusUser;
                            usr.UserId = item.UserId;
                            usr.User = item.UserUser;
                            list.Add(usr);
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
            catch(Exception ex)
            {
                response.Count = 0;
                response.Message = ex.Message;
                response.Result = null;
                return response;
            }
        }

        public async Task<Response<bool>> UpdateUser(UserUpdVM user)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var queryFilter = dc.spUpdUser(user.UserId,user.IdOwner,user.IdAdmin,user.Name,user.Address,user.Phone,user.User,user.Password,user.Icon,user.IconString,user.Status);
                    if(queryFilter == -1)
                    {                        
                        response.Count = 1;
                        response.Message = null;
                        response.Result = true;
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No se pudo actualizar";
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

        public async Task<Response<bool>> DeleteUser(Guid? IdUser,Guid? IdOwner,Guid? IdAdmin,string Name)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var queryFilter = dc.spDelUser(IdUser);
                    if (queryFilter == -1)
                    {
                        response.Count = 1;
                        response.Message = null;
                        response.Result = true;
                        //0=usuario, 1= propietario, 2=administrador
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No se puede eliminar el usuario";
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

        public async Task<Response<bool>> InsertUser(UserInsVM user)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var query = dc.spInsUser(user.IdOwner,user.IdAdmin,user.Name,user.Address,user.Phone,user.User,user.Password,user.Icon,user.IconString);
                    if (query == -1)
                    {
                        response.Count = 1;
                        response.Message = null;
                        response.Result = true;
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No se puedo insertar el usuario";
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

    }
}
