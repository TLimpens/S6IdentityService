using IdentityServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Data.Context
{
    public class UserContext : DbContext
    {

        public virtual DbSet<User> User { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

    }
}
