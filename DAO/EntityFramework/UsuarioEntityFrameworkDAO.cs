using AppClassLibraryDomain.DAO.EntityFramework;
using AppClassLibraryDomain.model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace AppClassLibraryDomain.DAO.NHibernate.EntityFramework
{
    public class UsuarioEntityFrameworkDAO
    {
        public IList<Usuario> GetUsuarios()
        {
            return new ContextFactory().Usuarios.ToList();
        }

        public Usuario CadastrarUsuario(Usuario usuario)
        {
            using (var context = new ContextFactory())
            {
                context.Usuarios.Add(usuario);
                var id = context.SaveChanges();
                return BuscarUsuarioPorId(id);
            }
        }

        public Usuario BuscarUsuarioPorId(int id)
        {
            using (var context = new ContextFactory())
                return context.Usuarios.Find(id);
        }
        public bool ApagarUsuario(Usuario usuario)
        {
            using (var context = new ContextFactory())
            {
                context.Usuarios.Attach(usuario);
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
                return true;
            }
        }

        public Usuario BuscarUsuarioPorEmail(string email)
        {
            using (var context = new ContextFactory())
                return context.Usuarios.Where(usuario => usuario.Email.Equals(email)).FirstOrDefault();
        }

        public Usuario AtualizarUsuario(Usuario usuario)
        {
            using (var context = new ContextFactory())
            {
                context.Usuarios.Attach(usuario);
                context.Entry(usuario).State = EntityState.Modified;
                var id = context.SaveChanges();
                return BuscarUsuarioPorId(id);
            }
        }
    }
}
