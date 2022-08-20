using System;
using System.Collections.Generic;
using AppClassLibraryDomain.DAO;
using AppClassLibraryDomain.DAO.EntityFramework;
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

        public IList<Usuario> GetUsuarios()
        {
           
            return usuarioNHibernateDAO.GetUsuarios();
        }

        public IList<ModelContex.Usuario> GetUsuariosEntity()
        {
            return usuarioEntityFrameworkDAO.GetUsuarios();
        }

        public bool AlterarPorId(Usuario usuario, long id)
        {
            usuario.Id = id;
            return usuarioDAO.Update(usuario);
        }

        public Usuario ApagarUsuarioNHibernate(Usuario usuario)
        {
            return usuarioNHibernateDAO.ApagarUsuario(usuario);
        }

        public bool ApagarPorId(Int32 id)
        {
            return usuarioDAO.DeletePorId(id);
        }
        public Usuario BuscarPorId(Int32 id)
        {
            return usuarioDAO.SelectPorId(id);
        }

        public Usuario BuscarPorNome(string nome)
        {
            return usuarioDAO.SelectPorNome(nome);
        }
        public Usuario BuscarPorEmail(string nome)
        {
            return usuarioDAO.SelectPorEmail(nome);
        }
        public Usuario BuscarPorIdNHibernate(Int64 id)
        {
            return usuarioNHibernateDAO.BuscarUsuarioPorId(id);
        }

        public Usuario BuscarPorEmailNHibernate(string email)
        {
            return usuarioNHibernateDAO.BuscarUsuarioPorEmail(email);
        }

        public Usuario Cadastrar(Usuario usuario)
        {
            usuario.Ativo = true;
            usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioDAO.Insert(usuario);
        }

        public Usuario CadastrarNHibernate(Usuario usuario)
        {
            usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioNHibernateDAO.CadastrarUsuario(usuario);
        }

        public bool ValidarSenha(string senhaTexto, string senhaEncriptada)
        {
            return BCryptNet.Verify(senhaTexto, senhaEncriptada);
        }

        public List<Usuario> Todos()
        {
            return usuarioDAO.Select();
        }

        public bool AlterarPorId(int? id)
        {
            return usuarioDAO.UpdateDataUltimoAcesso(id);
        }

        public object AtualizarNHibernate(Usuario usuario)
        {
            usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioNHibernateDAO.AtualizarUsuario(usuario);
        }
    }
}
