using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private TokenController _tokenController;

        public AccountController()
        {
            _tokenController = new TokenController();
        }

        [Route("login")]
        [HttpPost]
        public async Task<User> LoginAsync(User user)
        {
            HttpClient httpClient = new HttpClient();
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5020/connect/token",
                ClientId = "ClientId",
                ClientSecret = "ClientSecret",
                Scope = "SampleService"
            });

            var retrievedUser = user; //TODO: Hook up to database
            retrievedUser.id = user.id;
            retrievedUser.name = user.name;
            retrievedUser.username = user.username;
            retrievedUser.token = tokenResponse.AccessToken;

            if (retrievedUser == null)
                throw new Exception("Invalid username/password");

            return retrievedUser;
        }

    }
}