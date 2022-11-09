using System;
using System.Collections.Generic;
using AppClassLibraryDomain.model;
using NHibernate;

namespace AppClassLibraryDomain.DAO.NHibernate
{
    #region Class
    /// <summary>
    /// Classe de implementação de persistência de objetos usuários com NHibernate
    /// </summary>
    public class UsuarioNHibernateDAO : IUsuarioDAO
    {
        private SessionFactoryImpl _sessionFactoryImpl;

        public SessionFactoryImpl SessionFactoryImpl { set => _sessionFactoryImpl = value; }

        public bool DeleteById(long? id)
        {
            var usuario = this.SelectById(id);
            _sessionFactoryImpl.OpenSession.Delete(usuario);
            return true;
        }

        public UsuarioFotoPerfil DeleteFoto(UsuarioFotoPerfil usuarioFotoPerfil)
        {
            _sessionFactoryImpl.OpenSession.Delete(usuarioFotoPerfil);
            return usuarioFotoPerfil;
        }

        public Usuario Insert(Usuario usuario)
        {
            _sessionFactoryImpl.OpenSession.Save(usuario);
            return usuario;
        }
        public UsuarioFotoPerfil InsertFoto(UsuarioFotoPerfil usuarioFotoPerfil)
        {
            _sessionFactoryImpl.OpenSession.Save(usuarioFotoPerfil);
            return usuarioFotoPerfil;
        }

        public IList<Usuario> SelectAll()
        {
            IQuery query = _sessionFactoryImpl.OpenSession.CreateQuery("FROM Usuario");
            return query.List<Usuario>();
            //return null;
        }

        public IList<Usuario> SelectByContainsProperties(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Usuario SelectByEmail(string email)
        {
            //return null;
            //using (var session = SessionFactory.OpenSession)
            //{
            //}
            IQuery query = _sessionFactoryImpl.OpenSession.CreateQuery("FROM Usuario u WHERE u.Email = :email");
            query.SetParameter("email", email);
            var usuario = query.UniqueResult<Usuario>();
            return usuario;
        }

        public Usuario SelectById(long? id)
        {
            return null;
            //using (var session = SessionFactory.OpenSession)
            //{
            //    var usuario = (Usuario)session.Get(typeof(Usuario), id);
            //    return usuario;
            //}
        }

        public Usuario SelectByNome(string nome)
        {
            return null;
            //using (var session = SessionFactory.OpenSession)
            //{
            //    IQuery query = session.CreateQuery("FROM Usuario u WHERE u.Nome = :nome");
            //    query.SetParameter("nome", nome);
            //    var usuario = query.UniqueResult<Usuario>();
            //    return usuario;
            //}
        }

        public bool UpdateById(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Usuario UpdateByUsuario(Usuario usuario)
        {
            return null;
            //using (var session = SessionFactory.OpenSession)
            //{
            //    usuario.DataOperacao = DateTimeOffset.UtcNow;
            //    session.Update(usuario);
            //    session.Flush();
            //    return usuario;
            //}
        }

        public bool UpdateDataUltimoAcessoById(int? id)
        {
            //var usuario = this.SelectById(id);
            //using (var session = SessionFactory.OpenSession)
            //{
            //    usuario.DataOperacao = DateTimeOffset.UtcNow;
            //    session.Update(usuario);
            //    session.Flush();
            //    return usuario != null;
            //}
            return false;
        }

        public UsuarioFotoPerfil UpdateFoto(UsuarioFotoPerfil usuarioFotoPerfil)
        {
            //using (var session = SessionFactory.OpenSession)
            //{
            //    //usuarioFotoPerfil.DataOperacao = DateTimeOffset.UtcNow;
            //    session.Update(usuarioFotoPerfil);
            //    session.Flush();
            //    return usuarioFotoPerfil;
            //}
            return null;
        }
    }
    #endregion
}
