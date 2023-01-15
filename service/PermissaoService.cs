using AppClassLibraryDomain.DAO;
using AppClassLibraryDomain.DAO.SQL;
using AppClassLibraryDomain.model;
using System;
using System.Collections.Generic;

namespace AppClassLibraryDomain.service
{
    #region Interface
    /// <summary>
    /// Interface responsável por tratar dos dados cadastrais relacionados as permissões.
    /// </summary>
    public interface IPermissaoService : IGenericService<Permissao, long?> { }
    #endregion

    #region Class
    /// <summary>
    /// Classe responsável por tratar dos dados cadastrais relacionados as permissões.
    /// </summary>
    public class PermissaoService : IPermissaoService
    {

        private IPermissaoDAO _permissaoDAO;

        public PermissaoService(IPermissaoDAO permissaoDAO)
        {
            _permissaoDAO = permissaoDAO;
        }

        public IPermissaoDAO PermissaoDAO { set => _permissaoDAO = value; }

        public PermissaoService() : base() { }

        public List<Permissao> PermissoesPorNomeUsuario(string usuario) => throw new NotImplementedException();

        public IList<Permissao> PermissoesPorEmail(string email) => throw new NotImplementedException();

        public void Adicionar(Permissao permissao) => throw new NotImplementedException();

        public void Alterar(Permissao objeto) => throw new NotImplementedException();

        public Permissao Buscar(long? id) => throw new NotImplementedException();

        public Permissao BuscarPorId(long? id) => throw new NotImplementedException();

        public void Excluir(long? id) => throw new NotImplementedException();

        public IList<Permissao> ListarPorObjeto(Permissao objeto) => throw new NotImplementedException();

        public IList<Permissao> ListarTodos() => _permissaoDAO.SelectAll();
    }
    #endregion
}
