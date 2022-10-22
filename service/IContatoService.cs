using AppClassLibraryDomain.model;
using System.Collections.Generic;

namespace AppClassLibraryDomain.service
{
    public interface IContatoService
    {
        void Adicionar(Contato contato);
        void Alterar(Contato contato);
        Contato Buscar(int id);
        void Excluir(int id);
        IList<Contato> Listar(Contato contato);
    }
}
