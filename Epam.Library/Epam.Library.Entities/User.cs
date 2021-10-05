using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities
{
    public class User
    {
        public Guid id;
        public string Name { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public bool IsRegister { get; set; } = false;

        public User(Guid id, string name, string password, List<string> roles)
        {
            this.id = id;
            Name = name;
            Password = password;
            Roles = roles;
            IsRegister = true;
        }

        public User(string name, string password, List<string> roles)
        {
            this.id = Guid.NewGuid();
            Name = name;
            Password = password;
            Roles = roles;
            IsRegister = true;
        }
    }
}
