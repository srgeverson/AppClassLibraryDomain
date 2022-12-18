using System;

namespace AppClassLibraryDomain.model.DTO
{
    public class CriaTabelaDTO
    {
        private Int32 _numeroErro;
        private String _mensagemDeErro;

        public int NumeroErro { get => _numeroErro; set => _numeroErro = value; }
        public string MensagemDeErro { get => _mensagemDeErro; set => _mensagemDeErro = value; }
    }
}
