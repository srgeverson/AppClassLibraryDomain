using System.Collections.Generic;

namespace AppClassLibraryDomain.service
{
    public interface IGenericService<T, K>
    {
        void Adicionar(T objeto);
        void Alterar(T objeto);
        T Buscar(K id);
        void Excluir(K id);
        IList<T> Listar(T objeto);
    }
}
