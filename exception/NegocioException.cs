using System;

namespace AppClassLibraryDomain.exception
{
    public class NegocioException : Exception
    {
        private int? _status;
        public int? Status { get => _status; set => _status = value; }
        public NegocioException(string message) : base(message) { }
        public NegocioException(int? status, String message) : base(message)
        {
            _status = status;
        }
    }
}