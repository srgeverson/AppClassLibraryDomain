using System;

namespace AppClassLibraryDomain.model
{
    public class Usuario
    {
        public virtual long Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Senha { get; set; }
        public virtual bool? Ativo { get; set; }
        public virtual DateTimeOffset? DataOperacao { get; set; }
        public virtual DateTimeOffset? DataUltimoAcesso { get; set; }
    }
}
