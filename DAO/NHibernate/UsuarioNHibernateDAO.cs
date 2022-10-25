using AppClassLibraryDomain.model;
using NHibernate;
using System;
using System.Collections.Generic;

namespace AppClassLibraryDomain.DAO.NHibernate
{
    #region Class
    /// <summary>
    /// Classe de implementação de persistência de objetos usuários com NHibernate
    /// </summary>
    public class UsuarioNHibernateDAO : IUsuarioDAO
    {
        public bool DeleteById(long? id)
        {
            var usuario = this.SelectById(id);
            using (var session = SessionFactory.OpenSession)
            {
                session.Delete(usuario);
                session.Flush();
                return usuario != null;
            }
        }

        public UsuarioFotoPerfil DeleteFoto(UsuarioFotoPerfil usuarioFotoPerfil)
        {
            using (var session = SessionFactory.OpenSession)
            {
                session.Delete(usuarioFotoPerfil);
                session.Flush();
                return usuarioFotoPerfil;
            }
        }

        public Usuario Insert(Usuario usuario)
        {
            using (var session = SessionFactory.OpenSession)
                usuario.Id = Int64.Parse(session.Save(usuario).ToString());
            return usuario;
        }
        public UsuarioFotoPerfil InsertFoto(UsuarioFotoPerfil usuarioFotoPerfil)
        {
            using (var session = SessionFactory.OpenSession)
                usuarioFotoPerfil.Id = Int64.Parse(session.Save(usuarioFotoPerfil).ToString());
            return usuarioFotoPerfil;
        }

        public IList<Usuario> SelectAll()
        {
            using (var session = SessionFactory.OpenSession)
            {
                IQuery query = session.CreateQuery("FROM Usuario");
                return query.List<Usuario>();
            }
        }

        public IList<Usuario> SelectByContainsProperties(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Usuario SelectByEmail(string email)
        {
            using (var session = SessionFactory.OpenSession)
            {
                IQuery query = session.CreateQuery("FROM Usuario u WHERE u.Email = :email");
                query.SetParameter("email", email);
                var usuario = query.UniqueResult<Usuario>();
                return usuario;
            }
        }

        public Usuario SelectById(long? id)
        {
            using (var session = SessionFactory.OpenSession)
            {
                var usuario = (Usuario)session.Get(typeof(Usuario), id);
                return usuario;
            }
        }

        public Usuario SelectByNome(string nome)
        {
            using (var session = SessionFactory.OpenSession)
            {
                IQuery query = session.CreateQuery("FROM Usuario u WHERE u.Nome = :nome");
                query.SetParameter("nome", nome);
                var usuario = query.UniqueResult<Usuario>();
                return usuario;
            }
        }

        public bool UpdateById(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Usuario UpdateByUsuario(Usuario usuario)
        {
            using (var session = SessionFactory.OpenSession)
            {
                usuario.DataOperacao = DateTimeOffset.UtcNow;
                session.Update(usuario);
                session.Flush();
                return usuario;
            }
        }

        public bool UpdateDataUltimoAcessoById(int? id)
        {
            var usuario = this.SelectById(id);
            using (var session = SessionFactory.OpenSession)
            {
                usuario.DataOperacao = DateTimeOffset.UtcNow;
                session.Update(usuario);
                session.Flush();
                return usuario != null;
            }
        }

        public UsuarioFotoPerfil UpdateFoto(UsuarioFotoPerfil usuarioFotoPerfil)
        {
            using (var session = SessionFactory.OpenSession)
            {
                //usuarioFotoPerfil.DataOperacao = DateTimeOffset.UtcNow;
                session.Update(usuarioFotoPerfil);
                session.Flush();
                return usuarioFotoPerfil;
            }
        }
    }
    #endregion
}
