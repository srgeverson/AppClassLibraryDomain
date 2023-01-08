using System.Data.Entity;
using AppClassLibraryDomain.model;
using AppClassLibraryDomain.model.DTO;

namespace AppClassLibraryDomain.DAO.EntityFramework
{
    public class ContextFactory : DbContext
    {
        public ContextFactory() : base("name=connection_string") { }

        #region Entities
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioFotoPerfil> UsuariosFotosPerfis { get; set; }
        public DbSet<UsuarioPermissao> UsuariosPermissoes { get; set; }
        #endregion

        #region Stored Function
        public DbSet<ObterFeriadoDTO> FnObterFeriados { get; set; }
        #endregion

        #region Stored Procedure
        public DbSet<CriaTabelaDTO> SpCriaTabelas { get; set; }
        #endregion
    }
}
