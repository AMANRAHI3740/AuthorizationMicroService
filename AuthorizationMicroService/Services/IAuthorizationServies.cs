using AuthorizationMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Services
{
    public interface IAuthorizationServies
    {
        string Authorization(int CustomerId, string Password);
    }
}
