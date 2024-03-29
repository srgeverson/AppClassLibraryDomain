﻿using System;

namespace AppClassLibraryDomain.exception
{
    public class AuthorizationServerException : Exception
    {
        private int? _status;
        public int? Status { get => _status; set => _status = value; }
        public AuthorizationServerException(string message) : base(message) { }
        public AuthorizationServerException(int? status, String message) : base(message)
        {
            _status = status;
        }
    }
}
