using AppClassLibraryDomain.DAO.EntityFramework;
using AppClassLibraryDomain.model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace AppClassLibraryDomain.DAO.NHibernate.EntityFramework
{
    #region Inteface
    /// <summary>
    /// Inteface de persistência de objetos usuários
    /// </summary>
    public interface IUsuarioEntityFrameworkDAO : IGenericDAO<Usuario, long?>
    {
        Usuario SelectByEmail(string email);
        Usuario SelectByNome(string nome);
        Usuario UpdateByUsuario(Usuario usuario);
        bool UpdateDataUltimoAcessoById(long? id);
    }
    #endregion

    #region Class
    /// <summary>
    /// Classe de implementação de persistência de objetos usuários com NHibernate
    /// </summary>
    public class UsuarioEntityFrameworkDAO : IUsuarioEntityFrameworkDAO
    {
        public bool UpdateDataUltimoAcessoById(long? id)
        {
            var atualizar = false;
            var usuario = SelectById(id);
            using (var context = new ContextFactory())
            {
                usuario.DataUltimoAcesso = DateTime.UtcNow;
                context.Usuarios.Attach(usuario);
                context.Entry(usuario).State = EntityState.Modified;
                context.SaveChanges();
                atualizar = true;
            }
            return atualizar;
        }

        public bool DeleteById(long? id)
        {
            var usuario = SelectById(id);
            using (var context = new ContextFactory())
            {
                context.Usuarios.Attach(usuario);
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
                return true;
            }
        }

        public Usuario Insert(Usuario usuario)
        {
            using (var context = new ContextFactory())
            {
                context.Usuarios.Add(usuario);
                var id = context.SaveChanges();
                return SelectById(id);
            }
        }

        public IList<Usuario> SelectAll()
        {
            return new ContextFactory().Usuarios.ToList();
        }

        public IList<Usuario> SelectByContainsProperties(Usuario usuario)
        {
            throw new NotImplementedException();
        }
       
        public Usuario SelectByEmail(string email)
        {
            using (var context = new ContextFactory())
                return context.Usuarios.Where(usuario => usuario.Email.Equals(email)).FirstOrDefault();
        }

        public Usuario SelectById(long? id)
        {
            using (var context = new ContextFactory())
                return context.Usuarios.Find(id);
        }

        public Usuario SelectByNome(string nome)
        {
            using (var context = new ContextFactory())
                return context.Usuarios.Where(usuario => usuario.Email.Equals(nome)).FirstOrDefault();
        }

        public bool UpdateById(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Usuario UpdateByUsuario(Usuario usuario)
        {
            using (var context = new ContextFactory())
            {
                context.Usuarios.Attach(usuario);
                context.Entry(usuario).State = EntityState.Modified;
                var id = context.SaveChanges();
                return SelectById(id);
            }
        }

    }
    #endregion
}
