using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppClassLibraryDomain.model
{
    [Table("sistemas")] //EF
    public class Sistema
    {
        private long? _id;
        private string _nome;
        private string _descricao;
        private bool? _ativo;
        private DateTimeOffset? _dataCadastro;
        private DateTimeOffset? _dataOperacao;
        [Key] //EF
        public virtual long? Id { get => _id; set => _id = value; }
        public virtual string Nome { get => _nome; set => _nome = value; }
        public virtual string Descricao { get => _descricao; set => _descricao = value; }
        public virtual bool? Ativo { get => _ativo; set => _ativo = value; }
        [Column("data_cadastro")] //EF
        public virtual DateTimeOffset? DataCadastro { get => _dataCadastro; set => _dataCadastro = value; }
        [Column("data_operacao")] //EF
        public virtual DateTimeOffset? DataOperacao { get => _dataOperacao; set => _dataOperacao = value; }
    }
}
