using System;
using System.Linq;
using System.Data.Entity;
using AppClassLibraryDomain.model;
using System.Collections.Generic;

namespace AppClassLibraryDomain.DAO.EntityFramework
{
    #region Class
    /// <summary>
    /// Classe de implementação de persistência de objetos usuários com NHibernate
    /// </summary>
    public class UsuarioEntityFrameworkDAO : IUsuarioDAO
    {
        private ContextFactory _contextFactory;

        public ContextFactory ContextFactory { set => _contextFactory = value; }

        public bool DeleteById(long? id)
        {
            var usuario = SelectById(id);
            _contextFactory.Usuarios.Attach(usuario);
            _contextFactory.Usuarios.Remove(usuario);
            _contextFactory.SaveChanges();
            return true;
        }

        public UsuarioFotoPerfil DeleteFoto(UsuarioFotoPerfil usuarioFotoPerfil) => throw new NotImplementedException();

        public Usuario Insert(Usuario usuario)
        {
            _contextFactory.Usuarios.Add(usuario);
            var id = _contextFactory.SaveChanges();
            return SelectById(id);
        }

        public UsuarioFotoPerfil InsertFoto(UsuarioFotoPerfil usuarioFotoPerfil) => throw new NotImplementedException();

        public IList<Usuario> SelectAll() => _contextFactory.Usuarios.ToList();

        public IList<Usuario> SelectByContainsProperties(Usuario instanceObject) => throw new NotImplementedException();

        public Usuario SelectByEmail(string email) => _contextFactory.Usuarios.Where(usuario => usuario.Email.Equals(email)).FirstOrDefault();

        public Usuario SelectById(long? id) => _contextFactory.Usuarios.Find(id);

        public Usuario SelectByNome(string nome) => _contextFactory.Usuarios.Where(usuario => usuario.Email.Equals(nome)).FirstOrDefault();

        public bool UpdateById(Usuario instanceObject) => throw new NotImplementedException();

        public Usuario UpdateByUsuario(Usuario usuario)
        {
            _contextFactory.Usuarios.Attach(usuario);
            _contextFactory.Entry(usuario).State = EntityState.Modified;
            var id = _contextFactory.SaveChanges();
            return SelectById(id);
        }

        public bool UpdateDataUltimoAcessoById(long? id)
        {
            var usuario = SelectById(id);
            usuario.DataUltimoAcesso = DateTime.UtcNow;
            _contextFactory.Usuarios.Attach(usuario);
            _contextFactory.Entry(usuario).State = EntityState.Modified;
            _contextFactory.SaveChanges();
            return true;
        }

        public UsuarioFotoPerfil UpdateFoto(UsuarioFotoPerfil usuarioFotoPerfil) => throw new NotImplementedException();
    }
    #endregion
}
