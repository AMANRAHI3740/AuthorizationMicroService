using AuthorizationMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private IAuthorizationServies _authenticateServies;
        public AuthorizationController(IAuthorizationServies authenticateServies)
        {
            _authenticateServies = authenticateServies;
        }
        [HttpGet]
        public ActionResult<string> Post(int Id, string Password)
        {
            string user = _authenticateServies.Authorization(Id,Password);
            if(user == null)
            {
                return "fail";
            }
            else
            {
                return user; ;
            }
        }
    }
}
