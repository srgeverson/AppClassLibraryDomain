using AppClassLibraryDomain.DAO.EntityFramework;
using AppClassLibraryDomain.model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using ModelContex = AppClassLibraryDomain.model.EntityFramework;


namespace AppClassLibraryDomain.DAO.NHibernate.EntityFramework
{
    public class UsuarioEntityFrameworkDAO
    {
        public IList<ModelContex.Usuario> GetUsuarios()
        {
            return new ContextFactory().Usuarios.Select(usuario => usuario).ToList();   
        }

        public ModelContex.Usuario CadastrarUsuario(ModelContex.Usuario usuario)
        {
            using (var context = new ContextFactory())
            {
                context.Usuarios.Add(usuario);
                var id = context.SaveChanges();
                return BuscarUsuarioPorId(id);
            }
        }

        public ModelContex.Usuario BuscarUsuarioPorId(int id)
        {
            using (var context = new ContextFactory())
                return context.Usuarios.Find(id);
        }
        public ModelContex.Usuario ApagarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public ModelContex.Usuario BuscarUsuarioPorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Usuario AtualizarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
