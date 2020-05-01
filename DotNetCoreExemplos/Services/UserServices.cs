using DotNetCoreExemplos.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace DotNetCoreExemplos.Services
{
    public class UserServices
    {
        private int userId = 0;
        public bool SaveUserFile(User user)
        {
            if (userId == 0)
                deleteUserFile();

            userId += 1;
            user.Id = userId;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "UserFile.txt");
            string userJson = JsonConvert.SerializeObject(user);
            try
            {
                if (!File.Exists(path))
                    File.WriteAllText(path, userJson);
                else
                    File.AppendAllText(path, String.Format("{0}{1}", Environment.NewLine, userJson));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void deleteUserFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "UserFile.txt");
            
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
