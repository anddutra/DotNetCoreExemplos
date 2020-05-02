﻿using Microsoft.Extensions.Logging;
using System;

namespace DotNetCoreExemplos.Services
{
    public class ValueServices
    {
        private readonly ILogger _logger;

        public ValueServices(ILogger<ValueServices> logger)
        {
            _logger = logger;
        }

        public int GetRandomValue()
        {
            Random random = new Random();
            int value = random.Next(1, 100);

            _logger.LogInformation($"Número aleatório {value}");
            return value;
        }

        public string GetOddOrEven(int value)
        {
            if (value % 2 == 0)
                return "Número par";
            else
                return "Número impar";
        }

        public int GetSum3Values(int value1, int value2, int value3)
        {
            return value1 + value2 + value3;
        }
    }
}