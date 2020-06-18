using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Common.DataTransfer.User
{
    public class User
    {

        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string token { get; set; }

        public User(int id, string name, string username, string password)
        {
            this.id = id;
            this.name = name;
            this.username = username;
            this.password = password;

        }

    }
}
