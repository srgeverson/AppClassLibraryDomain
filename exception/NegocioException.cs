using System;

namespace SisContatos.domain.exception
{
    public class NegocioException : Exception
    {
        public NegocioException(string mensagem, Exception exception) : base(mensagem + exception.Message)
        {
        }
    }
}