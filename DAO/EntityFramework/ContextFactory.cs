using AppClassLibraryDomain.model.EntityFramework;
using System.Data.Entity;

namespace AppClassLibraryDomain.DAO.EntityFramework
{
    public class ContextFactory : DbContext
    {
        public ContextFactory() : base("name=connection_string")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
