namespace AppClassLibraryDomain.model
{
    public class UsuarioFotoPerfil
    {
        private long? _id;
        private string _nome;
        private Usuario _usuario;
        private string _mimeType;
        private string _caminho;
        private byte[] _arquivo;

        public virtual long? Id { get => _id; set => _id = value; }
        public virtual string Nome { get => _nome; set => _nome = value; }
        public virtual Usuario Usuario { get => _usuario; set => _usuario = value; }
        public virtual string MimeType { get => _mimeType; set => _mimeType = value; }
        public virtual string Caminho { get => _caminho; set => _caminho = value; }
        public virtual byte[] Arquivo { get => _arquivo; set => _arquivo = value; }
        public override string ToString() => _nome;
    }
}
