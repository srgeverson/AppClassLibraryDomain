using AppClassLibraryDomain.model;

namespace AppClassLibraryDomain.DAO
{
    #region Inteface
    /// <summary>
    /// Inteface de persistência de objetos usuários
    /// </summary>
    public interface IUsuarioDAO : IGenericDAO<Usuario, long?>
    {
        UsuarioFotoPerfil DeleteFoto(UsuarioFotoPerfil usuarioFotoPerfil);
        Usuario SelectByEmail(string email);
        Usuario SelectByNome(string nome);
        Usuario UpdateByUsuario(Usuario usuario);
        bool UpdateDataUltimoAcessoById(int? id);
        UsuarioFotoPerfil UpdateFoto(UsuarioFotoPerfil usuarioFotoPerfil);
        UsuarioFotoPerfil InsertFoto(UsuarioFotoPerfil usuarioFotoPerfil);
    }
    #endregion
}
