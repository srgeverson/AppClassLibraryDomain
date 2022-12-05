using System;

namespace AppClassLibraryDomain.exception
{
    public class SistemaException : Exception
    {
        private int? _status;
        public int? Status { get => _status; set => _status = value; }
        public SistemaException(String message) : base(message) { }
        public SistemaException(int? status, String message) : base(message) 
        {
            _status = status;
        }
    }
}
