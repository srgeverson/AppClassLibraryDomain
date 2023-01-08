using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppClassLibraryDomain.model
{
    [Table("permissoes")] //EF
    public class Permissao
    {
        private long? id;
        private string nome;
        private string descricao;
        private bool? ativo;

        [Key] //EF
        public virtual long? Id { get => id; set => id = value; }
        public virtual string Nome { get => nome; set => nome = value; }
        public virtual string Descricao { get => descricao; set => descricao = value; }
        public virtual bool? Ativo { get => ativo; set => ativo = value; }
    }
}
