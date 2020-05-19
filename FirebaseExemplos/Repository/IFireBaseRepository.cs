using FireBaseExemplos.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FireBaseExemplos.Repository
{
    public interface IFireBaseRepository
    {
        Task<IEnumerable<UserFirebase>> GetUsers(Func<CollectionReference, Query> query = null);
        Task<UserFirebase> GetDocument(string id);
        Task<bool> CreateUser(UserFirebase user);
        Task<bool> DeleteUser(string userId);
    }
}
