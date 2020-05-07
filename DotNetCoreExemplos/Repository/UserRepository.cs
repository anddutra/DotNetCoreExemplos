using DotNetCoreExemplos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNetCoreExemplos.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "UsersFile.txt");

        public bool SaveUserFile(User user)
        {
            string usersJson;
            
            try
            {
                List<User> users = new List<User>();
                if (File.Exists(_path))
                {
                    usersJson = File.ReadAllText(_path);
                    if (!String.IsNullOrEmpty(usersJson))
                        users = JsonConvert.DeserializeObject<List<User>>(usersJson);
                }

                user.Id = GetNextId(users);

                users.Add(user);
                usersJson = JsonConvert.SerializeObject(users);

                File.WriteAllText(_path, usersJson);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUserFile(int id)
        {
            if (File.Exists(_path))
            {
                string usersJson = File.ReadAllText(_path);

                if (!String.IsNullOrEmpty(usersJson))
                {
                    List<User> usuarios = JsonConvert.DeserializeObject<List<User>>(usersJson);
                    usuarios.RemoveAll(u => u.Id == id);

                    usersJson = JsonConvert.SerializeObject(usuarios);
                    File.WriteAllText(_path, usersJson);

                    return true;
                }
            }
            return false;
        }

        public string ReadUsersFile(string name)
        {
            if (File.Exists(_path))
            {
                string usersJson = File.ReadAllText(_path);

                if (!String.IsNullOrEmpty(usersJson))
                {
                    List<User> users = JsonConvert.DeserializeObject<List<User>>(usersJson);

                    if (name != null)
                        users = users.Where(u => u.Name.ToUpper().StartsWith(name.ToUpper())).ToList();

                    return JsonConvert.SerializeObject(users);
                }
            }

            return string.Empty;
        }

        private int GetNextId(List<User> users)
        {
            int id = 1;
            
            if(users.Count > 0)
            {
                id = users.Max(u => u.Id) + 1;
            }

            return id;
        }
    }
}
