using System;

namespace AppClassLibraryDomain.model
{
    public class UsuarioPermissao
    {
        private long? id;
        private Permissao _permissao;    
        private Usuario _usuario;    
        private bool? ativo;
        private DateTimeOffset? _dataCadastro;
        private DateTimeOffset? _dataOperacao;

        public virtual long? Id { get => id; set => id = value; }
        public virtual Permissao Permissao { get => _permissao; set => _permissao = value; }
        public virtual Usuario Usuario { get => _usuario; set => _usuario = value; }
        public virtual bool? Ativo { get => ativo; set => ativo = value; }
        public virtual DateTimeOffset? DataCadastro { get => _dataCadastro; set => _dataCadastro = value; }
        public virtual DateTimeOffset? DataOperacao { get => _dataOperacao; set => _dataOperacao = value; }
    }
}
