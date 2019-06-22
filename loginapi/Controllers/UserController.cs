using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace loginapi.Controllers
{
    public class UserController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/user/getall")]
        public IHttpActionResult Get()
        {
            return Ok(DateTime.Now.ToString());

        }

        [Authorize]
        [HttpGet]
        [Route("api/user/userAuth")]
        public IHttpActionResult GetAuth()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello" + identity.Name);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/user/authorize")]
        public IHttpActionResult GetAuthForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            return Ok("Hello" + identity.Name + "Role" + string.Join(",", roles.ToList()));
        }
    }
}
