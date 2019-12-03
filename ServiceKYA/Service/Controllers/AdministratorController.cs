using BussinessLayer.Administrator;
using BussinessLayer.Owner;
using BussinessLayer.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModels.Owner;
using ViewModels.Response;
using ViewModels.User;

namespace Service.Controllers
{
    [Authorize]
    [RoutePrefix("api/administrator")]
    public class AdministratorController : ApiController
    {
        BLAdministrator admin = new BLAdministrator();
        BLOwner owner = new BLOwner();
        BLUser user = new BLUser();

        [HttpPost]
        [Route("seladministrator")]
        public async Task<IHttpActionResult> SelectAdministrator(Authenticate au)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await admin.DoLogin(au.User, au.Password,au.PlayerId,au.PushToken);
            return Ok(response);
        }


        //////////////////////////////////////OWNER////////////////////////////////////////////////
        [HttpPost]
        [Route("insowner")]
        public async Task<IHttpActionResult> InsertOwner(OwnerInsVM ownervm)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await owner.InsertOwner(ownervm);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delowner")]
        public async Task<IHttpActionResult> DeleteOwner(Guid? IdOwner,Guid? IdAdmin,string Name)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await owner.DeleteOwner(IdOwner,IdAdmin,Name);
            return Ok(response);
        }

        [HttpPut]
        [Route("updowner")]
        public async Task<IHttpActionResult> UpdateOwner(OwnerUpdVM ownervm)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await owner.UpdateOwner(ownervm);
            return Ok(response);
        }

        [HttpGet]
        [Route("selowner")]
        public async Task<IHttpActionResult> SelectOwner(bool? status)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await owner.SelectOwnerAll(status);
            return Ok(response);
        }
        //////////////////////////////////////OWNER////////////////////////////////////////////////






        //////////////////////////////////////USER////////////////////////////////////////////////
        [HttpPost]
        [Route("insuser")]
        public async Task<IHttpActionResult> InsertUser(UserInsVM uservm)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await user.InsertUser(uservm);
            return Ok(response);
        }

        [HttpDelete]
        [Route("deluser")]
        public async Task<IHttpActionResult> DeleteUser(Guid? IdUser,Guid? IdOwner, Guid? IdAdmin, string Name)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await user.DeleteUser(IdUser,IdOwner,IdAdmin,Name);
            return Ok(response);
        }

        [HttpPut]
        [Route("upduser")]
        public async Task<IHttpActionResult> UpdateUser(UserUpdVM uservm)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await user.UpdateUser(uservm);
            return Ok(response);
        }

        [HttpGet]
        [Route("seluser")]
        public async Task<IHttpActionResult> SelectUser(Guid? IdOwner,bool? status)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await user.GetUser(IdOwner,status);
            return Ok(response);
        }
        //////////////////////////////////////USER/////////////////////////////////////////////
    }
}
