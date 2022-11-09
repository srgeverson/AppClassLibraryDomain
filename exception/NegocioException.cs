using System;

namespace AppClassLibraryDomain.exception
{
    public class NegocioException : Exception
    {
        public NegocioException(string mensagem, Exception exception) : base(mensagem + exception.Message)
        {
        }
    }
}