using AppClassLibraryDomain.exception;
using AppClassLibraryDomain.model;
using AppClassLibraryDomain.service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppClassLibraryDomain.facade
{
    #region Interface
    /// <summary>
    /// Interface de definição de Autorização de Aplicação
    /// </summary>
    public interface IAuthorizationServerFacade
    {
        Usuario BuscarPorEmail(String login);
        void ValidarConfigucaoDoToken(String secret, String expired, String token);
        bool? AtualizaDataUltimoAcesso(long? id);
        long[] PermissoesPorEmail(string email);
        Usuario CadastrarUsuario(Usuario usuario);
        IList<Usuario> ListarTodosUsuarios();
    }
    #endregion

    #region Interface
    /// <summary>
    /// Classe que implementa a Autorização de Aplicação
    /// </summary>
    public class AuthorizationServerFacade : IAuthorizationServerFacade
    {
        private IUsuarioService _usuarioService;
        private IPermissaoService _permissaoService;
        private IUsuarioPermissaoService _usuarioPermissaoService;

        public AuthorizationServerFacade()
        {
            if (_usuarioService == null)
                _usuarioService = new UsuarioService();

            if (_permissaoService == null)
                _permissaoService = new PermissaoService();

            if (_usuarioPermissaoService == null)
                _usuarioPermissaoService = new UsuarioPermissaoService();
        }

        public bool? AtualizaDataUltimoAcesso(long? id) => _usuarioService.AtualizaDataUltimoAcesso(id);

        public Usuario BuscarPorEmail(String email) => BuscarPorEmail(email);

        public Usuario CadastrarUsuario(Usuario usuario)
        {
            _usuarioService.Adicionar(usuario);
            return usuario;
        }

        public IList<Usuario> ListarTodosUsuarios() => _usuarioService.ListarTodos();

        public long[] PermissoesPorEmail(string email)
        {
            long[] permissoesId = _usuarioPermissaoService.PermissoesPorEmail(email)
                .Select(permissao => permissao.Id)
                .ToList()
                .ConvertAll(x => x.Value)
                .ToArray();
            return permissoesId;
        }

        public void ValidarConfigucaoDoToken(String secret, String expired, String token)
        {
            if (String.IsNullOrEmpty(secret)) throw new AuthorizationServerException("Não foi encontrado a chave secreta de validação do token.");
            if (String.IsNullOrEmpty(expired)) throw new AuthorizationServerException("Não foi definido tempo de validação do token.");
            if (int.TryParse(expired, out _)) throw new AuthorizationServerException("O tempo de validação do token não é um número válido.");
            if (String.IsNullOrEmpty(token)) throw new AuthorizationServerException("Não foi definido tipo do token.");
        }
    }
    #endregion
}
