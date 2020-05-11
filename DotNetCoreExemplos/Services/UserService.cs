using DotNetCoreExemplos.Models;
using DotNetCoreExemplos.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace DotNetCoreExemplos.Services
{
    public class UserService
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public bool SaveUserFile(User user)
        {
            bool saveUser = _userRepository.SaveUserFile(user);

            if(saveUser)
                _logger?.LogInformation($"Usuário {user.Name} salvo com sucesso!");
            else
                _logger?.LogError(1, $"Erro ao gravar usuario {user.Name}!");

            return saveUser;
        }

        public bool DeleteUserFile(int id)
        {
            bool deleteUser = _userRepository.DeleteUserFile(id);

            if (deleteUser)
                _logger?.LogInformation($"Usuário {id} excluído com sucesso!");
            else
                _logger?.LogError(1, $"Usuário {id} não encontrado para exclusão!");

            return deleteUser;
        }


        public string ReadUsersFile(string name)
        {
            return _userRepository.ReadUsersFile(name);
        }
    }
}
