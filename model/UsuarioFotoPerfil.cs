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

        public long? Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public Usuario Usuario { get => _usuario; set => _usuario = value; }
        public string MimeType { get => _mimeType; set => _mimeType = value; }
        public string Caminho { get => _caminho; set => _caminho = value; }
        public byte[] Arquivo { get => _arquivo; set => _arquivo = value; }

        public override string ToString() => _nome;
    }
}
