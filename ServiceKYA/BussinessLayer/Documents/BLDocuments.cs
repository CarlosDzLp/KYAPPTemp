using BussinessEntities.DataBases;
using BussinessLayer.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Response;

namespace BussinessLayer.Documents
{
    public class BLDocuments
    {
        Owner.BLOwner o = new Owner.BLOwner();
        User.BLUser u = new User.BLUser();
        public async Task<Response<bool>>DeleteDocuments(Guid? IdDocuments)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                using(var dc = new RentAppEntities())
                {
                    var query = dc.spDelDocument(IdDocuments);
                    if(query == -1)
                    {
                        response.Count = 1;
                        response.Message = null;
                        response.Result = true;
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No se pudo eliminar el documento";
                        response.Result = false;
                    }
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Count = 0;
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }
        
        public async Task<Response<bool>> InsertDocuments(DocumentsVM doc)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var query = dc.spInsDocument(doc.IdOwner, doc.IdAdmin, doc.Name, doc.FileDocument, doc.StringFile, ".pdf");
                    if (query == -1)
                    {
                        if (doc.IdOwner == null || doc.IdOwner == Guid.Empty)
                        {
                            //es para todos los usurios
                            var owner = await o.SelectOwnerAll(true);
                            foreach (var itemO in owner.Result)
                            {
                                Helpers.Notifications.Instance.NotificationsMessage(itemO.IdOwner, 1, "El administrador subio un nuevo documento");
                                var user = await u.GetUser(itemO.IdOwner, true);
                                foreach (var itemU in user.Result)
                                {
                                    Helpers.Notifications.Instance.NotificationsMessage(itemU.UserId, 0, "El administrador subio un nuevo documento");
                                }
                            }
                            //var user = await u.
                        }
                        else
                        {
                            //es para el propietario y los usuarios
                            var ownerFilter = await o.SelectOwnerAll(true);
                            var owner = ownerFilter.Result.Where(c => c.Status == true && c.IdOwner == doc.IdOwner).ToList();
                            foreach (var itemO in owner)
                            {
                                Helpers.Notifications.Instance.NotificationsMessage(itemO.IdOwner, 1, "El administrador subio un nuevo documento");
                                var user = await u.GetUser(itemO.IdOwner, true);
                                foreach (var itemU in user.Result)
                                {
                                    Helpers.Notifications.Instance.NotificationsMessage(itemU.UserId, 0, "El administrador subio un nuevo documento");
                                }
                            }
                        }
                        response.Count = 1;
                        response.Message = null;
                        response.Result = true;
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No se pudo insertar el documento";
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

        public async Task<Response<bool>> UpdateDocuments(DocumentsUpdVM doc)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var query = dc.spUpdDocument(doc.IdDocument, doc.IdOwner, doc.IdAdmin, doc.Name, doc.FileDocument, doc.StringFile, ".pdf");
                    if (query == -1)
                    {                        
                        response.Count = 1;
                        response.Message = null;
                        response.Result = true;
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No se pudo actualizar el documento";
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
        public async Task<Response<List<DocumentsGetVM>>> SelectDocuments(Guid? IdOwner)
        {
            Response<List<DocumentsGetVM>> response = new Response<List<DocumentsGetVM>>();
            List<DocumentsGetVM> list = new List<DocumentsGetVM>();
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var query = dc.spSelDocuments(null).ToList();
                    if (IdOwner != null || IdOwner != Guid.Empty)
                    {
                        var queryFilter = query.Where(c => c.IdOwner == IdOwner).ToList();
                        query = queryFilter;
                        //todos los usuarios
                    }                  
                    if (query.Count > 0)
                    {
                        foreach(var item in query)
                        {
                            var d = new DocumentsGetVM();
                            d.DateCreated=item.DateCreatedDocument;
                            d.DateModified=item.DateModifiedDocument;
                            d.Extensions=item.Extensions;
                            d.File=item.FileDocument;
                            d.IdAdmin=item.IdAdmin;
                            d.IdDocument=item.IdDocuments;
                            d.IdOwner=item.IdOwner;
                            d.Name=item.NameDocument;
                            d.Status=item.StatusDocument;
                            d.StringFile=item.StringFile;                          
                            list.Add(d);
                        }
                        response.Count = list.Count;
                        response.Message = null;
                        response.Result = list;
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No hay documentos";
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
