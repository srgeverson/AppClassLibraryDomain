using System.Configuration;

namespace AppClassLibraryDomain.DAO.SQL
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
            //set { url = value; }
        }
    }
}
