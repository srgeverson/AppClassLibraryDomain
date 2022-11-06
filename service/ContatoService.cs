using System;
using System.Collections.Generic;
using AppClassLibraryDomain.DAO.SQL;
using AppClassLibraryDomain.model;

namespace AppClassLibraryDomain.service
{
    #region Interface
    /// <summary>
    /// Interface responsável por tratar dos dados cadastrais relacionados aos contatos.
    /// </summary>
    public interface IContatoService : IGenericService<Contato, int?> { }
    #endregion

    #region Class
    /// <summary>
    /// Classe que implementa os serviços relacionados ao cadastro de contatos.
    /// </summary>
    public class ContatoService : IContatoService
    {
        private IContatoDAO _contatoDAO;

        public IContatoDAO ContatoDAO { set => _contatoDAO = value; }

        public void Adicionar(Contato contato)
        {
            try
            {
                var contatoNovo = _contatoDAO.Insert(contato);
                //if (contatoNovo.Id != null && contato.Id != 0)
                //    throw new Exception("Nenhum dado foi afetado.");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível realizar a operação {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public void Alterar(Contato contato)
        {
            try
            {
                if (!_contatoDAO.UpdateById(contato))
                    throw new Exception("Nenhum dado foi afetado.");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível realizar a operação {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public Contato Buscar(int? id)
        {
            try
            {
                return _contatoDAO.SelectById((int)id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível realizar a operação {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public Contato BuscarPorId(int? id)
        {
            try
            {
                return _contatoDAO.SelectById((int)id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível realizar a operação {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public void Excluir(int? id)
        {
            try
            {
                if (!_contatoDAO.DeleteById((int)id))
                    throw new Exception("Nenhum dado foi afetado.");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível realizar a operação {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public IList<Contato> ListarPorObjeto(Contato contato)
        {
            try
            {
                return _contatoDAO.SelectByContainsProperties(contato);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível realizar a operação {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }
        public IList<Contato> ListarTodos() => _contatoDAO.SelectAll();
    }
    #endregion
}
