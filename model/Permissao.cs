namespace AppClassLibraryDomain.model
{
    public class Permissao
    {
        private long? id;
        private string nome;
        private string descricao;
        private bool? ativo;

        public virtual long? Id { get => id; set => id = value; }
        public virtual string Nome { get => nome; set => nome = value; }
        public virtual string Descricao { get => descricao; set => descricao = value; }
        public virtual bool? Ativo { get => ativo; set => ativo = value; }
    }
}
