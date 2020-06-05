using FireBaseExemplos.Repository;
using Google.Cloud.Firestore;
using System;
using System.Reactive.Subjects;

namespace FireBaseExemplos.Services
{
    public class UserListenerService : IDisposable
    {
        private readonly IUserRepository _userRepository;
        public Subject<QuerySnapshot> Stream { get; private set; } = new Subject<QuerySnapshot>();

        public UserListenerService(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
            StartListeningUsers();
        }

        private void StartListeningUsers()
        {
            _userRepository.Listen(querySnapshot =>
            {
                Stream.OnNext(querySnapshot);
            });
        }

        public IDisposable Subscribe(Action<QuerySnapshot> next)
        {
            return Stream.Subscribe(next);
        }

        public IDisposable Subscribe(IObserver<QuerySnapshot> observer)
        {
            return Stream.Subscribe(observer);
        }

        public void Dispose()
        {
            Stream.Dispose();
        }
    }
}
