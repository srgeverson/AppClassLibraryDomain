using System;

namespace SisContatos.domain.exception
{
    public class InserirException : Exception
    {
        public InserirException(string origem, Exception exception) : base("Erro ao salvar os dados. Erro: " + exception.Message + ". Origem: " + origem)
        {
        }
    }
}