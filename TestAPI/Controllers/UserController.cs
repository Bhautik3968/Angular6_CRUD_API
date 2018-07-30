using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [RoutePrefix("api")]
    public class UserController : ApiController
    {
        [HttpGet]
        [Authorize]
        [Route("GetUserClaims")]
        public User GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            User model = new User()
            {
                Username = identityClaims.FindFirst("Username").Value,
                Password = identityClaims.FindFirst("Password").Value,
                ID = identityClaims.FindFirst("ID").Value,                
                LoggedOn = identityClaims.FindFirst("LoggedOn").Value
            };
            return model;
        }

        [HttpGet]
        [Route("Index")]
        public IHttpActionResult Index()
        {
            return Ok("Hello");
        }
    }
}
