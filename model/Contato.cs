using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppClassLibraryDomain.model
{
    [Table("contatos")] //EF
    public class Contato
    {
        private long? _id;
        private string _nome;
        private string _sobreNome;
        private string _email;
        private string _telefone;

        [Key] //EF
        public virtual long? Id { get => _id; set => _id = value; }
        public virtual string Nome { get => _nome; set => _nome = value; }
        public virtual string SobreNome { get => _sobreNome; set => _sobreNome = value; }
        public virtual string Email { get => _email; set => _email = value; }
        public virtual string Telefone { get => _telefone; set => _telefone = value; }
    }
}
