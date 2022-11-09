using System;

namespace AppClassLibraryDomain.exception
{
    public class ExcluirException : Exception
    {
        public ExcluirException(string origem, Exception exception) : base("Erro ao excluir os dados. Erro: " + exception.Message + ". Origem: " + origem)
        {
        }
    }
}