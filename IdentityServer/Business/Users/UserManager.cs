using IdentityModel.Client;
using IdentityServer.Common.DataTransfer.User;
using IdentityServer.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using D = IdentityServer.Models;

namespace IdentityServer.Business.Users
{
    public class UserManager : IUserManager
    {
        private readonly UserContext _userContext;

        public UserManager(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<User> LoginAsync(User user)
        {
            var retrievedUser = _userContext.User.SingleOrDefault(u => u.username == user.username);

            if (retrievedUser == null)
                throw new Exception("Invalid username/password");
            if (retrievedUser.password != GetHashedString(user.password, Convert.FromBase64String(retrievedUser.Salt)))
                throw new Exception("Invalid username/password");

            HttpClient httpClient = new HttpClient();
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5020/connect/token",
                ClientId = "ClientId",
                ClientSecret = "ClientSecret",
                Scope = "SampleService"
            });


            user.id = retrievedUser.id;
            user.name = retrievedUser.name;
            user.token = tokenResponse.AccessToken;

            return user;
        }


        public void CreateUser(User user)
        {
            var dbUser = this._userContext.User
                .Where(u => u.username == user.username)
                .SingleOrDefault();
            if (dbUser != null)
                throw new Exception($"User with username '{user.username}' already exists.");
            var salt = GenerateSalt();
            var entity = new D.User
            {
                name = user.name,
                username = user.username,
                password = GetHashedString(user.password, salt),
                Salt = Convert.ToBase64String(salt),
            };
            this._userContext.User.Add(entity);
            this._userContext.SaveChanges();
        }

        private static byte[] GenerateSalt()
        {
            using var crypto = RNGCryptoServiceProvider.Create();
            byte[] salt = new byte[32];
            crypto.GetBytes(salt);
            return salt;
        }

        private static string GetHashedString(string input, byte[] salt)
        {
            return Convert.ToBase64String(GetHash(input, salt));
        }

        private static byte[] GetHash(string input, byte[] salt)
        {
            byte[] saltedInput = salt.Concat(Encoding.UTF8.GetBytes(input)).ToArray();
            using var algorithm = SHA256.Create();
            return algorithm.ComputeHash(saltedInput);
        }
    }
}
