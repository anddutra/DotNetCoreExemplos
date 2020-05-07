using DotNetCoreExemplos.Models;
using DotNetCoreExemplos.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCoreExemplos.Test.Mockup
{
    public class UserRepositoryMockup : IUserRepository
    {
        private List<User> users = new List<User>();

        public UserRepositoryMockup()
        {
            users.Add(new User
            {
                Id = 1,
                Name = "João",
                LastName = "Silva",
                Email = "mail@mail.com"
            });
            users.Add(new User
            {
                Id = 2,
                Name = "Jose",
                LastName = "Souza",
                Email = "mail@mail.com"
            });
            users.Add(new User
            {
                Id = 3,
                Name = "Maria",
                LastName = "Ferreira",
                Email = "mail@mail.com"
            });
        }

        public bool DeleteUserFile(int id)
        {
            if (this.users.Where(u => u.Id == id).Count() == 1)
            {
                this.users.RemoveAll(u => u.Id == id);
                return true;
            }
            return false;
        }

        public string ReadUsersFile(string name)
        {
            List<User> users = this.users;
            if (name != null)
                users = users.Where(u => u.Name.ToUpper().StartsWith(name.ToUpper())).ToList();

            return JsonConvert.SerializeObject(users);
        }

        public bool SaveUserFile(User user)
        {
            if (this.users.Count() > 0)
                user.Id = this.users.Max(u => u.Id) + 1;
            else
                user.Id = 1;

            this.users.Add(user);
            return true;
        }
    }
}
