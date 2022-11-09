using System;

namespace AppClassLibraryDomain.exception
{
    public class AlterarException : Exception
    {
        public AlterarException(string origem, Exception exception) : base("Erro ao alterar os dados. Erro: " + exception.Message + ". Origem: " + origem)
        {
        }
    }
}