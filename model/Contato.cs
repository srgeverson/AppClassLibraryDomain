namespace AppClassLibraryDomain.model
{
    public class Contato
    {
        private int? id;
        private string nome;
        public int? Id { get => id; set => id = value; }
        public string Nome { get { return nome; } set { nome = value; } }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
