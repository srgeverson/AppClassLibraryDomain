using System;

namespace AppClassLibraryDomain.model.DTO
{
    public class ConfiguracaoTokenDTO
    {
        private String _secret;
        private String _token;
        private Double _expired;
        private String _app;

        public String Secret { get { return _secret; } set { _secret = value; } }
        public String Token { get { return _token; } set { _token = value; } }
        public Double Expired { get { return _expired; } set { _expired = value; } }
        public String App { get { return _app; } set { _app = value; } }
    }
}
