using NHibernate;
using NHibernate.Cfg;
using System;
using System.Reflection;

namespace AppClassLibraryDomain.DAO.NHibernate
{
    public class SessionFactoryImpl
    {
        private static volatile ISessionFactory _sessionFactory;
        private static object _syncRoot = new Object();

        public ISession OpenSession
        {
            get
            {
                if (_sessionFactory == null)
                {
                    lock (_syncRoot)
                    {
                        if (_sessionFactory == null)
                        {
                            var configuration = new Configuration();
                            configuration.Configure();
                            configuration.AddAssembly(Assembly.GetCallingAssembly());
                            _sessionFactory = configuration.BuildSessionFactory();
                        }
                    }
                }
                return _sessionFactory.OpenSession();
            }
        }
    }
}
