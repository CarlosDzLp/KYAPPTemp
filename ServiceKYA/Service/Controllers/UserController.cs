using BussinessLayer.Helpers;
using BussinessLayer.Notifications;
using BussinessLayer.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModels.Response;
using ViewModels.User;

namespace Service.Controllers
{
    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        BLUser user = new BLUser();

        [HttpPost]
        [Route("seluser")]
        public async Task<IHttpActionResult> SelectUser(Authenticate au)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await user.DoLogin(au.User, au.Password,au.PlayerId,au.PushToken);          
            return Ok(response);
        }

        [HttpPut]
        [Route("updateuser")]
        public async Task<IHttpActionResult> UpdateUser(UserUpdVM uservm)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await user.UpdateUser(uservm);
            Notifications.Instance.NotificationsMessage(uservm.IdOwner, 1, $"El usuario: {uservm.Name} ha actualizado sus datos");
            Notifications.Instance.NotificationsMessage(uservm.IdAdmin, 2, $"El usuario: {uservm.Name} ha actualizado sus datos");
            return Ok(response);
        }
    }
}
