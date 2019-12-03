using BussinessLayer.Administrator;
using BussinessLayer.Owner;
using BussinessLayer.User;
using Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModels.Response;

namespace Service.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/authenticate")]
    public class AuthenticateController : ApiController
    {
        [HttpPost]
        [Route("aouth")]
        public async Task<IHttpActionResult> AuthenticateAdministrator([FromBody]AuthenticateRoot auth)
        {
            if (auth == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var login = new BLAdministrator();
            //TODO: This code is only for demo - extract method in new class & validate correctly in your application !!
            var isUserValid = await login.UserRoot(auth.User, auth.Password);
            if (isUserValid.Result != null)
            {
                var rolename = "Administrator";
                var token = TokenGenerator.GenerateTokenJwt(auth.User, rolename);
                return Ok(token);
            }
            return BadRequest(isUserValid.Message);
        }

    }
}
