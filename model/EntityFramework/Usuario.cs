using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppClassLibraryDomain.model.EntityFramework
{
    [Table("usuarios")]
    [Obsolete("Esta classe deve ser substituída pela classe AppClassLibraryDomain.model.Usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool? Ativo { get; set; }
        [Column("data_operacao")]
        public DateTimeOffset? DataOperacao { get; set; }
        [Column("data_ultimo_acesso")]
        public DateTimeOffset? DataUltimoAcesso { get; set; }
    }
}
