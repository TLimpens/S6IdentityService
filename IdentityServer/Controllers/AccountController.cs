using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer.Business.Users;
using IdentityServer.Common.DataTransfer.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserManager _userManager;

        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [Route("login")]
        [HttpPost]
        public async Task<User> LoginAsync(User user)
        {
            return await _userManager.LoginAsync(user); 
        }

        [Route("create")]
        [HttpPost]
        public void CreateUser([FromBody]User user)
        {
            _userManager.CreateUser(user);
        }

    }
}