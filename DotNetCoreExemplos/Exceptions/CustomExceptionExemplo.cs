using System;

namespace DotNetCoreExemplos.Exceptions
{
    public class CustomExceptionExemplo : HttpResponseException
    {
        public CustomExceptionExemplo() : base(499, "Exemplo custom Exception")
        {
        }

        public CustomExceptionExemplo(Exception innerException) : base(499, "Exemplo custom Exception", innerException)
        {
        }
    }
}
