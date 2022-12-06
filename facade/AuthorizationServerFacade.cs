using AppClassLibraryDomain.exception;
using AppClassLibraryDomain.model;
using AppClassLibraryDomain.model.DTO;
using AppClassLibraryDomain.service;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
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
        ConfiguracaoTokenDTO ValidarConfigucaoDoToken(String secret, String expired, String token, String app);
        bool? AtualizaDataUltimoAcesso(long? id);
        long[] PermissoesPorEmail(String email);
        Usuario CadastrarUsuario(Usuario usuario);
        IList<Usuario> ListarTodosUsuarios();
        UsuarioLogadoDTO GerarToken(Usuario usuario, ConfiguracaoTokenDTO configToken, long[] permissoesId);
        PayloadTokenDTO ValidarAcesso(ConfiguracaoTokenDTO configuracaoTokenDTO, String token, long[] roles);
        void ValidarSenha(String senha, Usuario usuario);
        bool ValidarToken(ConfiguracaoTokenDTO configuracaoTokenDTO, String token);
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

        public IUsuarioService UsuarioService { set => _usuarioService = value; }
        public IPermissaoService PermissaoService { set => _permissaoService = value; }
        public IUsuarioPermissaoService UsuarioPermissaoService { set => _usuarioPermissaoService = value; }

        public bool? AtualizaDataUltimoAcesso(long? id) => _usuarioService.AtualizaDataUltimoAcesso(id);

        public Usuario BuscarPorEmail(String email) => _usuarioService.BuscarPorEmail(email);

        public Usuario CadastrarUsuario(Usuario usuario)
        {
            _usuarioService.Adicionar(usuario);
            return usuario;
        }

        public UsuarioLogadoDTO GerarToken(Usuario usuario, ConfiguracaoTokenDTO configToken, long[] permissoesId)
        {
            var utcNow = DateTimeOffset.UtcNow;

            var payload = new PayloadTokenDTO()
            {
                sub = usuario.Id,
                iss = configToken.App,
                roles = permissoesId,
                name = usuario.Nome,
                iat = utcNow.ToUnixTimeSeconds(),
                exp = utcNow.AddSeconds(configToken.Expired).ToUnixTimeSeconds(),
                aud = "AppGenérico"
            };
            var extraHeaders = new Dictionary<string, object> { };
            var key = Convert.FromBase64String(configToken.Secret);

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(extraHeaders, payload, key);

            return new UsuarioLogadoDTO()
            {
                TokenType = configToken.Token,
                AccessToken = token,
                ExpiresIn = utcNow.AddSeconds(configToken.Expired).ToUnixTimeSeconds(),
                Mensagem = "Usuário autorizado"
            };
        }

        public IList<Usuario> ListarTodosUsuarios() => _usuarioService.ListarTodos();

        public long[] PermissoesPorEmail(String email)
        {
            long[] permissoesId = _usuarioPermissaoService
                .PermissoesPorEmail(email)
                .Select(permissao => permissao.Id)
                .ToList()
                .ConvertAll(x => x.Value)
                .ToArray();
            return permissoesId;
        }

        public PayloadTokenDTO ValidarAcesso(ConfiguracaoTokenDTO configuracaoTokenDTO, string token, long[] roles)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                var key = Convert.FromBase64String(configuracaoTokenDTO.Secret);
                var payloadToken = new JsonNetSerializer().Deserialize<PayloadTokenDTO>(decoder.Decode(token, key, verify: true));

                return payloadToken;
            }
            catch (Exception ex)
            {
                throw new AuthorizationServerException(401, ex.Message);
            }
        }

        public ConfiguracaoTokenDTO ValidarConfigucaoDoToken(String secret, String expired, String token, String app)
        {
            var configuracaoTokenDTO = new ConfiguracaoTokenDTO();
            if (String.IsNullOrEmpty(secret))
                throw new AuthorizationServerException("Não foi encontrado a chave secreta de validação do token.");
            else
                configuracaoTokenDTO.Secret = secret;

            if (String.IsNullOrEmpty(expired))
                throw new AuthorizationServerException("Não foi definido tempo de validação do token.");
            else if (!int.TryParse(expired, out _))
                throw new AuthorizationServerException("O tempo de validação do token não é inválido.");
            else
                configuracaoTokenDTO.Expired = Convert.ToDouble(expired);

            if (String.IsNullOrEmpty(token))
                throw new AuthorizationServerException("Não foi definido tipo do token.");
            else
                configuracaoTokenDTO.Token = token;

            configuracaoTokenDTO.App = app;

            return configuracaoTokenDTO;
        }

        public void ValidarSenha(String senha, Usuario usuario)
        {
            if (!_usuarioService.ValidarSenha(senha, usuario.Senha))
                throw new AuthorizationServerException("Não foi definido tempo de validação do token.");
        }

        public bool ValidarToken(ConfiguracaoTokenDTO configuracaoTokenDTO, String token)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                var key = Convert.FromBase64String(configuracaoTokenDTO.Secret);
                return String.IsNullOrEmpty(decoder.Decode(token, key, verify: true));
            }
            catch (TokenNotYetValidException tnyvex)
            {
                throw new AuthorizationServerException(401, tnyvex.Message);
            }
            catch (TokenExpiredException teex)
            {
                throw new AuthorizationServerException(401, teex.Message);
            }
            catch (SignatureVerificationException svex)
            {
                throw new AuthorizationServerException(401, svex.Message);
            }
            catch (Exception ex)
            {
                throw new AuthorizationServerException(401, ex.Message);
            }
        }
    }
    #endregion
}
