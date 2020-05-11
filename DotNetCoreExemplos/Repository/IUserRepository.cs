using DotNetCoreExemplos.Models;

namespace DotNetCoreExemplos.Repository
{
    public interface IUserRepository
    {
        bool SaveUserFile(User user);
        bool DeleteUserFile(int id);
        string ReadUsersFile(string name);
    }
}
