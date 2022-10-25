using System.Configuration;

namespace AppClassLibraryDomain.DAO
{
    public class ConnectionFactoryDAO
    {
        private string url;
        public string Url
        {
            get
            {
                if (url == null)
                    return ConfigurationManager.AppSettings["sqlConnection"];
                else
                    return url;
            }
        }
    }
}
