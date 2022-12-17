using System;

namespace AppClassLibraryDomain.model.DTO
{
    public class ObterFeriadoDTO
    {
        private DateTime? _dia;
        private string _feriado;

        public DateTime? Dia { get => _dia; set => _dia = value; }
        public string Feriado { get => _feriado; set => _feriado = value; }
    }
}
