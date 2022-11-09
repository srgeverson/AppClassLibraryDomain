using System;

namespace AppClassLibraryDomain.exception
{
    public class SelecioneException : Exception
    {
        public SelecioneException(string origem, Exception exception) : base("Erro ao buscar dados. Erro: " + exception.Message + ". Origem: " + origem)
        {
        }
    }
}