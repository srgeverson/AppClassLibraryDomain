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
        private DateTimeOffset? _dataUltimoAcesso;

        public long? Id { get => id; set => id = value; }
        public Permissao Permissao { get => _permissao; set => _permissao = value; }
        public Usuario Usuario { get => _usuario; set => _usuario = value; }
        public bool? Ativo { get => ativo; set => ativo = value; }
        public DateTimeOffset? DataCadastro { get => _dataCadastro; set => _dataCadastro = value; }
        public DateTimeOffset? DataUltimoAcesso { get => _dataUltimoAcesso; set => _dataUltimoAcesso = value; }
    }
}
