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

        public User(Guid id, string name, string password, List<string> roles)
        {
            this.id = id;
            Name = name;
            Password = password;
            Roles = roles;
        }

        public User(Guid id, string name, string password, string role)
        {
            this.id = id;
            Name = name;
            Password = password;
            Roles.Add(role);
        }

        public User(string name, string password, List<string> roles)
        {
            this.id = Guid.NewGuid();
            Name = name;
            Password = password;
            Roles = roles;
        }

        public User(string name, string password)
        {
            this.id = Guid.NewGuid();
            Name = name;
            Password = password;
        }

        public User(Guid id, string name, string password)
        {
            this.id = id;
            Name = name;
            Password = password;
        }

        public User()
        {
        }

        public User(string name)
        {
            Name = name;
        }
    }
}
