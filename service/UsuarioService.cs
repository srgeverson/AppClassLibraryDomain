using System;
using System.Collections.Generic;
using AppClassLibraryDomain.DAO;
using AppClassLibraryDomain.model;
using BCryptNet = BCrypt.Net.BCrypt;

namespace AppClassLibraryDomain.service
{
    #region Interface
    /// <summary>
    /// Interface responsável por tratar dos dados cadastrais relacionados aos contatos.
    /// </summary>
    public interface IUsuarioService : IGenericService<Usuario, long?>
    {
        void ApagarFotoUsuario(UsuarioFotoPerfil usuarioFotoPerfil);
        UsuarioFotoPerfil AtualizarFoto(UsuarioFotoPerfil usuarioFotoPerfil);
        Usuario BuscarPorEmail(string email);
        Usuario BuscarPorNome(string nome);
        UsuarioFotoPerfil CadastrarFoto(UsuarioFotoPerfil usuarioFotoPerfil);
        bool ValidarSenha(string senhaTexto, string senhaEncriptada);
        bool? AtualizaDataUltimoAcesso(long? id);
    }
    #endregion

    #region Class
    /// <summary>
    /// Classe  que implementa os serviços relacionados ao cadastro de usuários.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioDAO _usuarioDAO;


        public IUsuarioDAO UsuarioDAO { set => _usuarioDAO = value; }
        public UsuarioService(IUsuarioDAO usuarioDAO)
        {
            _usuarioDAO = usuarioDAO;
        }
        public void Adicionar(Usuario usuario)
        {
            usuario.Ativo = true;
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            usuario.DataCadastro = DateTime.UtcNow;
            _usuarioDAO.Insert(usuario);
        }
        public void Alterar(Usuario usuario)
        {
            //usuario.Id = id;
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = BCryptNet.HashPassword(usuario.Senha);
            _usuarioDAO.UpdateById(usuario);
        }
        public void ApagarFotoUsuario(UsuarioFotoPerfil usuarioFotoPerfil) => _usuarioDAO.DeleteFoto(usuarioFotoPerfil);
        public UsuarioFotoPerfil AtualizarFoto(UsuarioFotoPerfil usuarioFotoPerfil) => _usuarioDAO.UpdateFoto(usuarioFotoPerfil);
        public Usuario Buscar(long? id) => throw new NotImplementedException();
        public Usuario BuscarPorId(long? id) => _usuarioDAO.SelectById(id);
        public Usuario BuscarPorEmail(string email) => _usuarioDAO.SelectByEmail(email);
        public Usuario BuscarPorNome(string nome) => _usuarioDAO.SelectByNome(nome);
        public UsuarioFotoPerfil CadastrarFoto(UsuarioFotoPerfil usuarioFotoPerfil) => _usuarioDAO.InsertFoto(usuarioFotoPerfil);
        public void Excluir(long? id) => _usuarioDAO.DeleteById(Convert.ToInt64(id.ToString()));
        public IList<Usuario> ListarPorObjeto(Usuario usuario) => throw new NotImplementedException();
        public IList<Usuario> ListarTodos() => _usuarioDAO.SelectAll();
        public bool ValidarSenha(string senhaTexto, string senhaEncriptada)
        {
            return BCryptNet.Verify(senhaTexto, senhaEncriptada);
        }
        public bool? AtualizaDataUltimoAcesso(long? id) => _usuarioDAO.UpdateDataUltimoAcessoById(id);
    }
    #endregion
}
