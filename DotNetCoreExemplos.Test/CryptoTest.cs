using DotNetCoreExemplos.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DotNetCoreExemplos.Test
{
    public class CryptoTest
    {
        private CryptoRijndaelService _cryptoRijndaelService;

        public CryptoTest()
        {
            _cryptoRijndaelService = new CryptoRijndaelService();
        }

        [Theory]
        [InlineData("123", "RsybjZfIhnUCPzQOpX1hcA==")]
        [InlineData("654321", "O5wXbyFCmqjkHCUN5ekJeA==")]
        public void TestGetEncryptPassword(string password, string result)
        {
            string encryptPassword = _cryptoRijndaelService.GetEncryptPassword(password);

            Assert.Equal(result, encryptPassword);
        }

        [Theory]
        [InlineData("RsybjZfIhnUCPzQOpX1hcA==", "123")]
        [InlineData("O5wXbyFCmqjkHCUN5ekJeA==", "654321")]
        public void TestGetDecryptPassword(string encryptPassword, string result)
        {
            string password = _cryptoRijndaelService.GetDecryptPassword(encryptPassword);

            Assert.Equal(result, password);
        }
    }
}
