using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [Route("authorize")]
        [HttpGet]
        public async Task<IActionResult> GenerateToken()
        {
            HttpClient httpClient = new HttpClient();
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5020/connect/token",
                ClientId = "ClientId",
                ClientSecret = "ClientSecret",
                Scope = "SampleService"
            });
            return Ok(tokenResponse.Json);
        }
    }
}