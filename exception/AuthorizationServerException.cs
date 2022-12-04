using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppClassLibraryDomain.exception
{
    public class AuthorizationServerException : Exception
    {
        public int? Status { get; set; }
        public AuthorizationServerException(string message) : base(message) { }
    }
}
