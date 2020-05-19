using DotNetCoreExemplos.Models;
using System.Collections.Generic;

namespace DotNetCoreExemplos.Repository
{
    public interface IUserRepository
    {
        bool SaveUserFile(User user);
        bool DeleteUserFile(int id);
        List<User> ReadUsersFile(string name);
    }
}
