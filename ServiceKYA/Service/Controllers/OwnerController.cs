using BussinessLayer.Helpers;
using BussinessLayer.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModels.Owner;
using ViewModels.Response;

namespace Service.Controllers
{
    [Authorize]
    [RoutePrefix("api/owner")]
    public class OwnerController : ApiController
    {
        BLOwner owner = new BLOwner();
        [HttpPost]
        [Route("selowner")]
        public async Task<IHttpActionResult> SelectOwner(Authenticate au)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await owner.DoLogin(au.User, au.Password,au.PlayerId,au.PushToken);
            return Ok(response);
        }

        [HttpPut]
        [Route("updateowner")]
        public async Task<IHttpActionResult> UpdateOwner(OwnerUpdVM ownervm)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await owner.UpdateOwner(ownervm);
            Notifications.Instance.NotificationsMessage(ownervm.IdAdmin, 2, $"El propietario {ownervm.Name} ha actualizado sus datos");
            return Ok(response);
        }
    }
}
