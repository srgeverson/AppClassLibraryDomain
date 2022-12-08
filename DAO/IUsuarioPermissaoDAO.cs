using System;
using System.Collections.Generic;
using AppClassLibraryDomain.model;
using AppClassLibraryDomain.model.DTO;

namespace AppClassLibraryDomain.DAO
{
    #region Inteface
    /// <summary>
    /// Inteface de persistência de objetos usuários permissões
    /// </summary>
    public interface IUsuarioPermissaoDAO : IGenericDAO<UsuarioPermissao, long?>
    {
        IList<UsuarioPermissao> SelectByEmail(String email);
        IList<UsuarioPermissaoDTO> SelectByEmailAndSistema(String email, String sistema);
    }
    #endregion
}
