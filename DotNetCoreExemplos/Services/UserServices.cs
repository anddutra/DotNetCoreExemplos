using DotNetCoreExemplos.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace DotNetCoreExemplos.Services
{
    public class UserServices
    {
        private readonly ILogger _logger;
        private int userId = 0;

        public UserServices(ILogger<UserServices> logger)
        {
            _logger = logger;
        }

        public bool SaveUserFile(User user)
        {
            user.Id = GetNextId();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "UserFile.txt");
            string userJson = JsonConvert.SerializeObject(user);
            try
            {
                if (!File.Exists(path))
                    File.WriteAllText(path, userJson);
                else
                    File.AppendAllText(path, String.Format("{0}{1}", Environment.NewLine, userJson));

                _logger.LogInformation($"Usuário {user.Name} salvo com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(1, ex, $"Erro ao gravar usuario {user.Name}!");
                return false;
            }
        }

        public string ReadUserFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "UserFile.txt");
            string usuarios = string.Empty;
            if (File.Exists(path))
                usuarios = File.ReadAllText(path);

            if (String.IsNullOrEmpty(usuarios))
                usuarios = "Nenhum usuário cadastrado!";

            return usuarios;
        }

        private int GetNextId()
        {
            if (userId == 0)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "UserFile.txt");

                if (File.Exists(path))
                {
                    string userJson = File.ReadLines(path).Last();
                    if (!String.IsNullOrEmpty(userJson))
                    {
                        User usuario = JsonConvert.DeserializeObject<User>(userJson);
                        userId = usuario.Id + 1;
                        return userId;
                    }
                }
            }
            userId += 1;

            _logger.LogInformation($"Próximo Id {userId}");
            return userId;
        }
    }
}
