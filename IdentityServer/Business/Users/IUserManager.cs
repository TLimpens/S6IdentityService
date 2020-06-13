using IdentityServer.Common.DataTransfer.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Business.Users
{
    public interface IUserManager
    {

        public Task<User> LoginAsync(User user);
        public void CreateUser(User user);
    }
}
