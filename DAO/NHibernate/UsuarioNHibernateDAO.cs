using AppClassLibraryDomain.model;
using NHibernate;
using System;
using System.Collections.Generic;

namespace AppClassLibraryDomain.DAO.NHibernate
{
    public class UsuarioNHibernateDAO
    {
        public IList<Usuario> GetUsuarios()
        {
            using (var session = SessionFactory.OpenSession)
            {
                IQuery query = session.CreateQuery("FROM Usuario");
                return query.List<Usuario>();
            }
        }

        public Usuario CadastrarUsuario(Usuario usuario)
        {
            using (var session = SessionFactory.OpenSession)
                usuario.Id = Int64.Parse(session.Save(usuario).ToString());
            return usuario;
        }

        public Usuario BuscarUsuarioPorId(Int64 id)
        {
            using (var session = SessionFactory.OpenSession)
            {
                var usuario = (Usuario)session.Get(typeof(Usuario), id);
                return usuario;
            }
        }
        public Usuario ApagarUsuario(Usuario usuario)
        {
            using (var session = SessionFactory.OpenSession)
            {
                session.Delete(usuario);
                session.Flush();
                return usuario;
            }
        }

        public Usuario BuscarUsuarioPorEmail(string email)
        {
            using (var session = SessionFactory.OpenSession)
            {
                IQuery query = session.CreateQuery("FROM Usuario u WHERE u.Email = :email");
                query.SetParameter("email", email);
                var usuario = query.UniqueResult<Usuario>();
                return usuario;
            }
        }

        public Usuario AtualizarUsuario(Usuario usuario)
        {
            using (var session = SessionFactory.OpenSession)
            {
                usuario.DataOperacao = DateTimeOffset.UtcNow;
                session.Update(usuario);
                session.Flush();
                return usuario;
            }
        }
    }
}
