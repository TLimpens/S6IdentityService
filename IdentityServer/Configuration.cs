using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> { new ApiResource("SampleService"),

            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client> {  new Client { 
                    ClientId = "ClientId",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("ClientSecret".Sha256())
                    },
                    AllowedScopes = { "SampleService" },
                    AccessTokenLifetime =3600
                }
            };

    }
}
