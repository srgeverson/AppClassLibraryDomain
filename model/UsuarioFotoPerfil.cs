using System;

namespace AppClassLibraryDomain.model
{
    public class UsuarioFotoPerfil
    {
        public virtual long Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual Usuario Usuario { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}
