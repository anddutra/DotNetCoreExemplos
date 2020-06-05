using FireBaseExemplos.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Type = Google.Cloud.Firestore.DocumentChange.Type;

namespace FireBaseExemplos.Services
{
    public class FirestoreListenerService : IHostedService
    {
        private readonly ConcurrentDictionary<string, UserFirebase> _users;
        UserListenerService _userListenerService;

        public FirestoreListenerService(ConcurrentDictionary<string, UserFirebase> users,
            UserListenerService userListenerService)
        {
            _users = users;
            _userListenerService = userListenerService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            StartListeningUsers();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public void StartListeningUsers()
        {
            _userListenerService.Stream
                .SelectMany(t => t.Changes)
                //.Select(snapshot =>
                //{
                //    var user = snapshot.Document.ConvertTo<UserFirebase>();
                //    if (snapshot.ChangeType == DocumentChange.Type.Removed)
                //    {
                //        user.Status = UserStatus.Inactive;
                //    }
                //    return (snapshot, user);
                //})
                //.Where(data => data.user.Status == UserStatus.Active)
                .Subscribe(change =>
                {
                    _ = change.ChangeType switch
                    {
                        Type.Added => _users.TryAdd(change.Document.Id, change.Document.ConvertTo<UserFirebase>()),
                        Type.Modified => _users.TryUpdate(change.Document.Id, change.Document.ConvertTo<UserFirebase>(), _users[change.Document.Id]),
                        Type.Removed => _users.TryRemove(change.Document.Id, out _),
                        _ => false
                    };
                });
        }
    }
}
