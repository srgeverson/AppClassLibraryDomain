using AppClassLibraryDomain.DAO;
using AppClassLibraryDomain.DAO.SQL;
using AppClassLibraryDomain.model;
using System;
using System.Collections.Generic;

namespace AppClassLibraryDomain.service
{
    #region Interface
    /// <summary>
    /// Interface responsável por tratar dos dados cadastrais relacionados as permissões dos usuários.
    /// </summary>
    public interface IUsuarioPermissaoService : IGenericService<UsuarioPermissao, long?>
    {
        IList<UsuarioPermissao> PermissoesPorEmail(string email);
    }
    #endregion

    #region Class
    /// <summary>
    /// Classe responsável por tratar dos dados cadastrais relacionados as permissões do usuário.
    /// </summary>
    public class UsuarioPermissaoService : IUsuarioPermissaoService
    {

        private IUsuarioPermissaoDAO _usuarioPermissaoDAO;

        public UsuarioPermissaoService()
        {
            if (_usuarioPermissaoDAO == null)
                _usuarioPermissaoDAO = new UsuarioPermissaoSQLDAO();
        }

        public void Adicionar(UsuarioPermissao usuarioPermissao) => throw new NotImplementedException();

        public void Alterar(UsuarioPermissao usuarioPermissao) => throw new NotImplementedException();

        public UsuarioPermissao Buscar(long? id) => throw new NotImplementedException();

        public UsuarioPermissao BuscarPorId(long? id) => throw new NotImplementedException();

        public void Excluir(long? id) => throw new NotImplementedException();

        public IList<UsuarioPermissao> ListarPorObjeto(UsuarioPermissao usuarioPermissao) => throw new NotImplementedException();

        public IList<UsuarioPermissao> ListarTodos() => throw new NotImplementedException();

        public IList<UsuarioPermissao> PermissoesPorEmail(string email)
        {
            return _usuarioPermissaoDAO.SelectByEmail(email);
        }
    }
    #endregion
}
