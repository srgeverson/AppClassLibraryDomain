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
    #region Interface
    /// <summary>
    /// Interface responsável por tratar dos dados cadastrais relacionados aos contatos.
    /// </summary>
    public interface IUsuarioService : IGenericService<Usuario, long?> { }
    #endregion

    #region Class
    /// <summary>
    /// Classe  que implementa os serviços relacionados ao cadastro de usuários.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioDAO usuarioDAO;
        private UsuarioNHibernateDAO usuarioNHibernateDAO;
        private UsuarioEntityFrameworkDAO usuarioEntityFrameworkDAO;

        public IUsuarioDAO UsuarioDAO { set => usuarioDAO = value; }

        public UsuarioService()
        {
            //if (usuarioDAO == null)
            //    usuarioDAO = new UsuarioDAO();

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
            return usuarioEntityFrameworkDAO.DeleteById(usuario.Id);
        }

        public ModelContex.Usuario AtualizarEntity(ModelContex.Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioEntityFrameworkDAO.UpdateByUsuario(usuario);
        }

        public ModelContex.Usuario BuscarPorEmailEntity(string email)
        {
            return usuarioEntityFrameworkDAO.SelectByEmail(email);
        }

        public ModelContex.Usuario CadastrarEntity(ModelContex.Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioEntityFrameworkDAO.Insert(usuario);
        }

        public IList<ModelContex.Usuario> GetUsuariosEntity()
        {
            return usuarioEntityFrameworkDAO.SelectAll();
        }

        public ModelContex.Usuario GetUsuarioPorIdEntity(int id)
        {
            return usuarioEntityFrameworkDAO.SelectById(id);
        }

        #endregion -> Fim Entity Framework

        #region -> Início NHibernate

        public Usuario AtualizarNHibernate(Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioNHibernateDAO.UpdateByUsuario(usuario);
        }

        public Usuario ApagarUsuarioNHibernate(Usuario usuario)
        {
            return usuarioNHibernateDAO.DeleteById((long)usuario.Id) ? usuario : null;
        }

        public Usuario BuscarPorIdNHibernate(Int64 id)
        {
            return usuarioNHibernateDAO.SelectById(id);
        }

        public Usuario BuscarPorEmailNHibernate(string email)
        {
            return usuarioNHibernateDAO.SelectByEmail(email);
        }

        public Usuario CadastrarNHibernate(Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioNHibernateDAO.Insert(usuario);
        }

        public IList<Usuario> GetUsuariosNHibernate()
        {
            return usuarioNHibernateDAO.SelectAll();
        }

        public UsuarioFotoPerfil CadastrarFotoNHibernate(UsuarioFotoPerfil usuarioFotoPerfil)
        {
            return usuarioNHibernateDAO.InsertFoto(usuarioFotoPerfil);
        }
        public UsuarioFotoPerfil ApagarFotoUsuarioNHibernate(UsuarioFotoPerfil usuarioFotoPerfil)
        {
            return usuarioNHibernateDAO.DeleteFoto(usuarioFotoPerfil);
        }
        public UsuarioFotoPerfil AtualizarFotoNHibernate(UsuarioFotoPerfil usuarioFotoPerfil)
        {
            return usuarioNHibernateDAO.UpdateFoto(usuarioFotoPerfil);
        }

        #endregion -> Início NHibernate

        #region -> Início SQL

        public bool ApagarPorIdSQL(long id)
        {
            return usuarioDAO.DeleteById(Convert.ToInt32(id.ToString()));
        }

        public bool AlterarPorIdSQL(Usuario usuario, long id)
        {
            usuario.Id = id;
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            return usuarioDAO.UpdateById(usuario);
        }

        public bool AlterarPorIdSQL(int? id)
        {
            return usuarioDAO.UpdateDataUltimoAcessoById(id);
        }

        public Usuario BuscarPorIdSQL(Int32 id)
        {
            return usuarioDAO.SelectById(id);
        }

        public Usuario BuscarPorNomeSQL(string nome)
        {
            return usuarioDAO.SelectByNome(nome);
        }
        public Usuario BuscarPorEmailSQL(string nome)
        {
            return usuarioDAO.SelectByEmail(nome);
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
            return usuarioDAO.SelectAll();
        }

        public void Adicionar(Usuario objeto)
        {
            this.CadastrarSQL(objeto);
        }

        public void Alterar(Usuario objeto)
        {
            this.AlterarPorIdSQL(objeto, objeto.Id);
        }

        public Usuario Buscar(long? id)
        {
            return this.BuscarPorIdSQL((int)id);
        }

        public void Excluir(long? id)
        {
            this.ApagarPorIdSQL((long)id);
        }

        public IList<Usuario> Listar(Usuario objeto)
        {
           return this.GetUsuariosSQL();
        }

        #endregion -> Fim SQL

        #endregion
    }
}
