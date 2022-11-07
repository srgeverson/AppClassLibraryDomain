using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppClassLibraryDomain.model
{
    [Table("usuarios")]
    public class Usuario
    {
        private long? _id;
        private string _nome;
        private string _email;
        private string _senha;
        private bool? _ativo;
        private DateTimeOffset? _dataOperacao;
        private DateTimeOffset? _dataCadastro;
        private DateTimeOffset? _dataUltimoAcesso;
        private UsuarioFotoPerfil _usuarioFotoPerfil;
        [Key]
        public virtual long? Id { get => _id; set => _id = value; }
        public virtual string Nome { get => _nome; set => _nome = value; }
        public virtual string Email { get => _email; set => _email = value; }
        public virtual string Senha { get => _senha; set => _senha = value; }
        public virtual bool? Ativo { get => _ativo; set => _ativo = value; }
        [Column("data_operacao")]
        public virtual DateTimeOffset? DataOperacao { get => _dataOperacao; set => _dataOperacao = value; }
        [Column("data_cadastro")]
        public virtual DateTimeOffset? DataCadastro { get => _dataCadastro; set => _dataCadastro = value; }
        [Column("data_ultimo_acesso")]
        public virtual DateTimeOffset? DataUltimoAcesso { get => _dataUltimoAcesso; set => _dataUltimoAcesso = value; }
        [NotMapped]
        public virtual UsuarioFotoPerfil UsuarioFotoPerfil { get => _usuarioFotoPerfil; set => _usuarioFotoPerfil = value; }
    }
}
