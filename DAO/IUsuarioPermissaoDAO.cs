using AppClassLibraryDomain.model;
using System.Collections.Generic;

namespace AppClassLibraryDomain.DAO
{
    #region Inteface
    /// <summary>
    /// Inteface de persistência de objetos usuários permissões
    /// </summary>
    public interface IUsuarioPermissaoDAO : IGenericDAO<UsuarioPermissao, long?>
    {
        IList<UsuarioPermissao> SelectByEmail(string email);
    }
    #endregion
}
