using System;
using System.Collections.Generic;

namespace AppClassLibraryDomain.service
{
    public interface IGenericService<T, K>
    {
        /// <summary>
        /// Cadastra um objeto
        /// </summary>
        /// <param name="objeto">Objeto a ser cadstrado</param>
        void Adicionar(T objeto);
        /// <summary>
        /// Altera um objeto
        /// </summary>
        /// <param name="objeto">Objeto a ser alterado</param>
        void Alterar(T objeto);
        /// <summary>
        /// Método que busca a entidade pela chave primária
        /// </summary>
        /// <param name="id">Chave primária da entidade</param>
        /// <returns>Retorna uma entidade que utiliza o serviço</returns>
        [Obsolete("Este método deve ser substituído pelo BuscarPorId")]
        T Buscar(K id);
        /// <summary>
        /// Método que busca a entidade pela chave primária
        /// </summary>
        /// <param name="id">Chave primária da entidade</param>
        /// <returns>Retorna um objeto do tipo T, onde T é a entidade que utiliza o serviço</returns>
        T BuscarPorId(K id);
        /// <summary>
        /// Apaga um objeto pela chave primária
        /// </summary>
        /// <param name="id">Chave primária da entidade</param>
        void Excluir(K id);
        /// <summary>
        /// Lista os objetos que contém as informações contidas no parâmetro informado
        /// </summary>
        /// <param name="objeto">Objeto a ser consultado</param>
        /// <returns>Lista de objetos que possui as informações contidas no parâmetro informado</returns>
        IList<T> ListarPorObjeto(T objeto);
        /// <summary>
        /// Lista todos objetos
        /// </summary>
        /// <returns>Lista de todos objetos</returns>
        IList<T> ListarTodos();
    }
}
