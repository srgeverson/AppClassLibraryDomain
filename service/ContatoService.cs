﻿using System;
using System.Collections.Generic;
using AppClassLibraryDomain.DAO;
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
        private IContatoDAO contatoDAO;

        public IContatoDAO ContatoDAO { set => contatoDAO = value; }

        public void Adicionar(Contato contato)
        {
            try
            {
                var contatoNovo = contatoDAO.Insert(contato);
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
                if (!contatoDAO.UpdateById(contato))
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
                return contatoDAO.SelectById((int)id);
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
                if (!contatoDAO.DeleteById((int)id))
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
                return contatoDAO.SelectByContainsProperties(contato);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível realizar a operação {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }
    }
    #endregion
}