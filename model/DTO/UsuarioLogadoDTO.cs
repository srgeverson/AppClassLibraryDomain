using System;

namespace AppClassLibraryDomain.model.DTO
{
    /// <summary>
    /// Classe que representa usuário que irá se autenticar
    /// </summary>
    public class UsuarioLogadoDTO
    {
        public String AccessToken { get; set; }
        public String TokenType { get; set; }
        public long ExpiresIn { get; set; }
        public String Scope { get; set; }
        public String Mensagem { get; set; }
    }
}
