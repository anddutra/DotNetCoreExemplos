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
            User user = new User
            {
                Name = "Carla",
                LastName = "Santos",
                Email = "mail@mail.com"
            };

            users = _userServices.ReadUsersFile(null);
            Assert.True(users.Count() == 3);

            _userServices.SaveUserFile(user);

            users = _userServices.ReadUsersFile(null);
            Assert.True(users.Count() == 4);
            Assert.True(users.Where(u => u.Name.Equals("Carla") && u.Id == 4).Count() == 1);
        }

        [Fact]
        public void TestDeleteUserFile()
        {
            List<User> users;
            bool deleteUser;

            users = _userServices.ReadUsersFile(null);
            Assert.True(users.Count() == 3);

            deleteUser = _userServices.DeleteUserFile(1);
            Assert.True(deleteUser);

            users = _userServices.ReadUsersFile(null);
            Assert.True(users.Count() == 2);

            deleteUser = _userServices.DeleteUserFile(1);
            Assert.False(deleteUser);

            users = _userServices.ReadUsersFile(null);
            Assert.True(users.Count() == 2);
        }

        [Fact]
        public void TestReadUsersFile()
        {
            List<User> users;

            users = _userServices.ReadUsersFile(null);
            Assert.True(users.Count() == 3);

            users = _userServices.ReadUsersFile("João");
            Assert.True(users.Count() == 1);

            users = _userServices.ReadUsersFile("Jose");
            Assert.True(users.Count() == 1);

            users = _userServices.ReadUsersFile("Maria");
            Assert.True(users.Count() == 1);

            users = _userServices.ReadUsersFile("Jo");
            Assert.True(users.Count() == 2);
        }
    }
}
