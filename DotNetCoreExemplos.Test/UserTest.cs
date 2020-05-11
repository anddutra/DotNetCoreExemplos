using DotNetCoreExemplos.Models;
using DotNetCoreExemplos.Services;
using DotNetCoreExemplos.Test.Mockup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DotNetCoreExemplos.Test
{
    public class UserTest
    {
        private UserService _userServices;

        public UserTest()
        {
            _userServices = new UserService(null, new UserRepositoryMockup());
        }

        [Fact]
        public void TestSaveUserFile()
        {
            List<User> users;
            string usersjson;
            User user = new User
            {
                Name = "Carla",
                LastName = "Santos",
                Email = "mail@mail.com"
            };

            usersjson = _userServices.ReadUsersFile(null);
            users = JsonConvert.DeserializeObject<List<User>>(usersjson);
            Assert.True(users.Count() == 3);

            _userServices.SaveUserFile(user);

            usersjson = _userServices.ReadUsersFile(null);
            users = JsonConvert.DeserializeObject<List<User>>(usersjson);
            Assert.True(users.Count() == 4);
            Assert.True(users.Where(u => u.Name.Equals("Carla") && u.Id == 4).Count() == 1);
        }

        [Fact]
        public void TestDeleteUserFile()
        {
            List<User> users;
            string usersjson;
            bool deleteUser;

            usersjson = _userServices.ReadUsersFile(null);
            users = JsonConvert.DeserializeObject<List<User>>(usersjson);
            Assert.True(users.Count() == 3);

            deleteUser = _userServices.DeleteUserFile(1);
            Assert.True(deleteUser);

            usersjson = _userServices.ReadUsersFile(null);
            users = JsonConvert.DeserializeObject<List<User>>(usersjson);
            Assert.True(users.Count() == 2);

            deleteUser = _userServices.DeleteUserFile(1);
            Assert.False(deleteUser);

            usersjson = _userServices.ReadUsersFile(null);
            users = JsonConvert.DeserializeObject<List<User>>(usersjson);
            Assert.True(users.Count() == 2);
        }

        [Fact]
        public void TestReadUsersFile()
        {
            List<User> users;
            string usersjson;

            usersjson = _userServices.ReadUsersFile(null);
            users = JsonConvert.DeserializeObject<List<User>>(usersjson);
            Assert.True(users.Count() == 3);

            usersjson = _userServices.ReadUsersFile("João");
            users = JsonConvert.DeserializeObject<List<User>>(usersjson);
            Assert.True(users.Count() == 1);

            usersjson = _userServices.ReadUsersFile("Jose");
            users = JsonConvert.DeserializeObject<List<User>>(usersjson);
            Assert.True(users.Count() == 1);

            usersjson = _userServices.ReadUsersFile("Maria");
            users = JsonConvert.DeserializeObject<List<User>>(usersjson);
            Assert.True(users.Count() == 1);

            usersjson = _userServices.ReadUsersFile("Jo");
            users = JsonConvert.DeserializeObject<List<User>>(usersjson);
            Assert.True(users.Count() == 2);
        }
    }
}
