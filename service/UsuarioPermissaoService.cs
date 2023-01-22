using AppClassLibraryDomain.DAO;
using AppClassLibraryDomain.DAO.SQL;
using AppClassLibraryDomain.model;
using AppClassLibraryDomain.model.DTO;
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
        IList<UsuarioPermissao> PermissoesPorEmail(String email);
        IList<UsuarioPermissaoDTO> PermissoesPorEmailESistema(String email, String sistema);
    }
    #endregion

    #region Class
    /// <summary>
    /// Classe responsável por tratar dos dados cadastrais relacionados as permissões do usuário.
    /// </summary>
    public class UsuarioPermissaoService : IUsuarioPermissaoService
    {

        private IUsuarioPermissaoDAO _usuarioPermissaoDAO;
        public UsuarioPermissaoService() : base() { }
        public UsuarioPermissaoService(IUsuarioPermissaoDAO usuarioPermissaoDAO)
        {
            _usuarioPermissaoDAO = usuarioPermissaoDAO;
        }

        public IUsuarioPermissaoDAO UsuarioPermissaoDAO { set => _usuarioPermissaoDAO = value; }

        public void Adicionar(UsuarioPermissao usuarioPermissao) => throw new NotImplementedException();

        public void Alterar(UsuarioPermissao usuarioPermissao) => throw new NotImplementedException();

        public UsuarioPermissao Buscar(long? id) => throw new NotImplementedException();

        public UsuarioPermissao BuscarPorId(long? id) => throw new NotImplementedException();

        public void Excluir(long? id) => throw new NotImplementedException();

        public IList<UsuarioPermissao> ListarPorObjeto(UsuarioPermissao usuarioPermissao) => throw new NotImplementedException();

        public IList<UsuarioPermissao> ListarTodos() => throw new NotImplementedException();

        public IList<UsuarioPermissao> PermissoesPorEmail(String email)
        {
            return _usuarioPermissaoDAO.SelectByEmail(email);
        }

        public IList<UsuarioPermissaoDTO> PermissoesPorEmailESistema(String email, String sistema)
        {
            return _usuarioPermissaoDAO.SelectByEmailAndSistema(email, sistema);
        }
    }
    #endregion
}
