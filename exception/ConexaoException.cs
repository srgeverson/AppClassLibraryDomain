using System;

namespace AppClassLibraryDomain.exception
{
    public class ConexaoException : Exception
    {
        public ConexaoException(Exception exception) : base("Erro de conexão: Detalhes do erro " + exception.Message)
        {
        }
    }
}