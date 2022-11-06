using System.Collections.Generic;

namespace AppClassLibraryDomain.service
{
    public interface IGenericService<T, K>
    {
        void Adicionar(T objeto);
        void Alterar(T objeto);
        T Buscar(K id);
        T BuscarPorId(K id);
        void Excluir(K id);
        IList<T> ListarPorObjeto(T objeto);
        IList<T> ListarTodos();
    }
}
