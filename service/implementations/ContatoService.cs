using System;
using System.Collections.Generic;
using AppClassLibraryDomain.domain.DAO;
using AppClassLibraryDomain.model;

namespace AppClassLibraryDomain.service.implementations
{
    public class ContatoService : IContatoService
    {
        private IContatoDAO contatoDAO = new ContatoDAO();

        public IContatoDAO ContatoDAO { set => contatoDAO = value; }

        public void Adicionar(Contato contato)
        {
            try
            {
                var contatoNovo = contatoDAO.Insert(contato);
                if (contatoNovo.Id != null && contato.Id != 0)
                    throw new Exception("Nenhum dado foi afetado.");
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
                if (!contatoDAO.UpdateByObject(contato))
                    throw new Exception("Nenhum dado foi afetado.");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível realizar a operação {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public Contato Buscar(int id)
        {
            try
            {
                return contatoDAO.SelectById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível realizar a operação {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public void Excluir(int id)
        {
            try
            {
                if (!contatoDAO.DeleteById(id))
                    throw new Exception("Nenhum dado foi afetado.");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível realizar a operação {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public IList<Contato> Listar(Contato contato)
        {
            try
            {
                return contatoDAO.SelectByObject(contato);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível realizar a operação {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }
    }
}
