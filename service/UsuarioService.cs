using System;
using System.Collections.Generic;
using AppClassLibraryDomain.DAO;
using AppClassLibraryDomain.DAO.NHibernate;
using AppClassLibraryDomain.DAO.NHibernate.EntityFramework;
using AppClassLibraryDomain.model;
using BCryptNet = BCrypt.Net.BCrypt;
using ModelContex = AppClassLibraryDomain.model.EntityFramework;


namespace AppClassLibraryDomain.service
{
    /// <summary>
    /// Classe responsável por tratar dos dados cadastrais relacionados usuários.
    /// </summary>
    public class UsuarioService
    {
        private UsuarioDAO usuarioDAO;
        private UsuarioNHibernateDAO usuarioNHibernateDAO;
        private UsuarioEntityFrameworkDAO usuarioEntityFrameworkDAO;

        public UsuarioService()
        {
            if (usuarioDAO == null)
                usuarioDAO = new UsuarioDAO();

            if (usuarioNHibernateDAO == null)
                usuarioNHibernateDAO = new UsuarioNHibernateDAO();

            if (usuarioEntityFrameworkDAO == null)
                usuarioEntityFrameworkDAO = new UsuarioEntityFrameworkDAO();
        }

        public bool ValidarSenha(string senhaTexto, string senhaEncriptada)
        {
            return BCryptNet.Verify(senhaTexto, senhaEncriptada);
        }

        #region -> Início Entity Framework

        public bool ApagarUsuarioEntity(ModelContex.Usuario usuario)
        {
            return usuarioEntityFrameworkDAO.ApagarUsuario(usuario);
        }

        public ModelContex.Usuario AtualizarEntity(ModelContex.Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioEntityFrameworkDAO.AtualizarUsuario(usuario);
        }

        public ModelContex.Usuario BuscarPorEmailEntity(string email)
        {
            return usuarioEntityFrameworkDAO.BuscarUsuarioPorEmail(email);
        }

        public ModelContex.Usuario CadastrarEntity(ModelContex.Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioEntityFrameworkDAO.CadastrarUsuario(usuario);
        }

        public IList<ModelContex.Usuario> GetUsuariosEntity()
        {
            return usuarioEntityFrameworkDAO.GetUsuarios();
        }

        public ModelContex.Usuario GetUsuarioPorIdEntity(int id)
        {
            return usuarioEntityFrameworkDAO.BuscarUsuarioPorId(id);
        }

        #endregion -> Fim Entity Framework

        #region -> Início NHibernate

        public Usuario AtualizarNHibernate(Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioNHibernateDAO.AtualizarUsuario(usuario);
        }

        public Usuario ApagarUsuarioNHibernate(Usuario usuario)
        {
            return usuarioNHibernateDAO.ApagarUsuario(usuario);
        }

        public Usuario BuscarPorIdNHibernate(Int64 id)
        {
            return usuarioNHibernateDAO.BuscarUsuarioPorId(id);
        }

        public Usuario BuscarPorEmailNHibernate(string email)
        {
            return usuarioNHibernateDAO.BuscarUsuarioPorEmail(email);
        }

        public Usuario CadastrarNHibernate(Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioNHibernateDAO.CadastrarUsuario(usuario);
        }

        public IList<Usuario> GetUsuariosNHibernate()
        {
            return usuarioNHibernateDAO.GetUsuarios();
        }

        public UsuarioFotoPerfil CadastrarFotoNHibernate(UsuarioFotoPerfil usuarioFotoPerfil)
        {
            return usuarioNHibernateDAO.CadastrarFotoUsuario(usuarioFotoPerfil);
        }
        public UsuarioFotoPerfil ApagarFotoUsuarioNHibernate(UsuarioFotoPerfil usuarioFotoPerfil)
        {
            return usuarioNHibernateDAO.ApagarFotoUsuario(usuarioFotoPerfil);
        }
        public UsuarioFotoPerfil AtualizarFotoNHibernate(UsuarioFotoPerfil usuarioFotoPerfil)
        {
            return usuarioNHibernateDAO.AtualizarFotoUsuario(usuarioFotoPerfil);
        }

        #endregion -> Início NHibernate

        #region -> Início SQL

        public bool ApagarPorIdSQL(long id)
        {
            return usuarioDAO.DeletePorId(Convert.ToInt32(id.ToString()));
        }

        public bool AlterarPorIdSQL(Usuario usuario, long id)
        {
            usuario.Id = id;
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioDAO.Update(usuario);
        }

        public bool AlterarPorIdSQL(int? id)
        {
            return usuarioDAO.UpdateDataUltimoAcesso(id);
        }

        public Usuario BuscarPorIdSQL(Int32 id)
        {
            return usuarioDAO.SelectPorId(id);
        }

        public Usuario BuscarPorNomeSQL(string nome)
        {
            return usuarioDAO.SelectPorNome(nome);
        }
        public Usuario BuscarPorEmailSQL(string nome)
        {
            return usuarioDAO.SelectPorEmail(nome);
        }

        public Usuario CadastrarSQL(Usuario usuario)
        {
            usuario.Ativo = true;
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioDAO.Insert(usuario);
        }

        public IList<Usuario> GetUsuariosSQL()
        {
            return usuarioDAO.Select();
        }

        #endregion -> Fim SQL
    }
}
