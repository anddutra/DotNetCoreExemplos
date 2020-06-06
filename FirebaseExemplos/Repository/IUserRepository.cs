using FireBaseExemplos.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FireBaseExemplos.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserFirebase>> GetUsers(Func<CollectionReference, Query> query = null);
        Task<UserFirebase> GetUsersById(string userId);
        Task<bool> CreateUser(UserFirebase user);
        Task<bool> AlterUser(UserFirebase user);
        Task<bool> AlterStatus(string userId, object status);
        Task<bool> DeleteUser(string userId);
        void Listen(Action<QuerySnapshot> callback, Func<CollectionReference, Query> query = null);
    }
}
