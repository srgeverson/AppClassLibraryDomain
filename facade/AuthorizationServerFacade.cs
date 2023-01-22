using AppClassLibraryDomain.exception;
using AppClassLibraryDomain.model;
using AppClassLibraryDomain.model.DTO;
using AppClassLibraryDomain.service;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

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
        long[] PermissoesPorEmailESistema(String email, String sistema);
        Usuario CadastrarUsuario(Usuario usuario);
        IList<Usuario> ListarTodosUsuarios();
        UsuarioLogadoDTO GerarToken(Usuario usuario, ConfiguracaoTokenDTO configToken, long[] permissoesId);
        string GerarTokenNetCore(Usuario usuario, ConfiguracaoTokenDTO configToken, long[] permissoesId);
        PayloadTokenDTO ValidarAcesso(ConfiguracaoTokenDTO configuracaoTokenDTO, String token, long[] roles);
        void ValidarSenha(String senha, Usuario usuario);
        bool ValidarToken(ConfiguracaoTokenDTO configuracaoTokenDTO, String token);
        void ValidarRoles(long[] rolesEndPoint, long[] rolesUsuario);
        object BuscarUsuarioPorId(long id);
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

        public AuthorizationServerFacade() : base() { }
        public AuthorizationServerFacade(IUsuarioService usuarioService, IPermissaoService permissaoService, IUsuarioPermissaoService usuarioPermissaoService) : base()
        {
            _usuarioService = usuarioService;
            _permissaoService = permissaoService;
            _usuarioPermissaoService = usuarioPermissaoService;
        }

        public IUsuarioService UsuarioService { set => _usuarioService = value; }
        public IPermissaoService PermissaoService { set => _permissaoService = value; }
        public IUsuarioPermissaoService UsuarioPermissaoService { set => _usuarioPermissaoService = value; }

        public bool? AtualizaDataUltimoAcesso(long? id) => _usuarioService.AtualizaDataUltimoAcesso(id);

        public Usuario BuscarPorEmail(String email) => _usuarioService.BuscarPorEmail(email);

        public object BuscarUsuarioPorId(long id)
        {
            throw new NotImplementedException();
        }

        public Usuario CadastrarUsuario(Usuario usuario)
        {
            if (BuscarPorEmail(usuario.Email) != null)
                throw new NegocioException(409, "Já existe usuário cadastrado com o e-mail informado.");
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
        public string GerarTokenNetCore(Usuario usuario, ConfiguracaoTokenDTO configToken, long[] permissoesId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configToken.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome.ToString()),
                    //new Claim(ClaimTypes.Role, string.Join(",",permissoesId.ToArray()))
                    new Claim(ClaimTypes.Role, "1")
                }),
                Expires = DateTime.UtcNow.AddSeconds(configToken.Expired),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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

        public long[] PermissoesPorEmailESistema(string email, string sistema)
        {
            var permissoesUsuario = _usuarioPermissaoService
                .PermissoesPorEmailESistema(email, sistema);
            long[] permissoesId = permissoesUsuario
                .Select(permissao => permissao.Permissao)
                .ToList()
                .ConvertAll(x => x.Value)
                .ToArray();
            return permissoesId;
        }

        public PayloadTokenDTO ValidarAcesso(ConfiguracaoTokenDTO configuracaoTokenDTO, string token, long[] roles)
        {
            if (token == null)
                throw new AuthorizationServerException(401, "Token não informado.");
            var tokenSplit = token.Split(' ');
            if (tokenSplit.Length != 2)
                throw new AuthorizationServerException(401, "Token inválido.");
            if (!tokenSplit[0].ToLower().Equals(configuracaoTokenDTO.Token.ToLower()))
                throw new AuthorizationServerException(401, "Autenticação inválida.");
            if (tokenSplit[1] == null || tokenSplit[1].Equals(""))
                throw new AuthorizationServerException(401, "Token não inválido.");
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                var key = Convert.FromBase64String(configuracaoTokenDTO.Secret);
                var payloadToken = new JsonNetSerializer().Deserialize<PayloadTokenDTO>(decoder.Decode(tokenSplit[1], key, verify: true));

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
                throw new AuthorizationServerException(401, "Não foi encontrado a chave secreta de validação do token.");
            else
                configuracaoTokenDTO.Secret = secret;

            if (String.IsNullOrEmpty(expired))
                throw new AuthorizationServerException(401, "Não foi definido tempo de validação do token.");
            else if (!int.TryParse(expired, out _))
                throw new AuthorizationServerException(401, "O tempo de validação do token não é inválido.");
            else
                configuracaoTokenDTO.Expired = Convert.ToDouble(expired);

            if (String.IsNullOrEmpty(token))
                throw new AuthorizationServerException(401, "Não foi definido tipo do token.");
            else
                configuracaoTokenDTO.Token = token;

            configuracaoTokenDTO.App = app;

            return configuracaoTokenDTO;
        }

        public void ValidarRoles(long[] rolesEndPoint, long[] rolesUsuario)
        {
            var rolesAcesso = from ru in rolesUsuario join re in rolesEndPoint on ru equals re select re;
            if (!rolesAcesso.Any())
                throw new AuthorizationServerException(String.Format("Acesso negado. Este usuário não tem a(s) permissão(ões): {0}.", String.Join(",", rolesEndPoint)));
        }

        public void ValidarSenha(String senha, Usuario usuario)
        {
            if (!_usuarioService.ValidarSenha(senha, usuario.Senha))
                throw new AuthorizationServerException("Dados de usuário inválido.");
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
