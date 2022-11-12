using AppClassLibraryDomain.DAO.SQL;
using AppClassLibraryDomain.model;
using System.Collections.Generic;

namespace AppClassLibraryDomain.service
{
    /// <summary>
    /// Classe responsável por tratar dos dados cadastrais relacionados as permissões do usuário.
    /// </summary>
    public class UsuarioPermissaoService
    {

        private PermissaoDAO usuarioPermissaoDAO;

        public UsuarioPermissaoService()
        {
            this.usuarioPermissaoDAO = new PermissaoDAO();
        }

        public List<Permissao> PermissoesPorNomeUsuario(string usuario)
        {
            return usuarioPermissaoDAO.SelectByNomeUsuario(usuario);
        }

        public List<Permissao> Todos()
        {
            return usuarioPermissaoDAO.Select();
        }

        public List<Permissao> PermissoesPorEmail(string email)
        {
            return usuarioPermissaoDAO.SelectByEmail(email);
        }
    }
}
