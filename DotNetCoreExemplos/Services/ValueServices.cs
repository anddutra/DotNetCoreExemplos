using System;

namespace DotNetCoreExemplos.Services
{
    public class ValueServices
    {
        public int GetRandomValue()
        {
            Random random = new Random();
            return random.Next(1, 100);
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
