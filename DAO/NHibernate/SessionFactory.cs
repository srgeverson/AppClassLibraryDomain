using NHibernate;
using NHibernate.Cfg;
using System;
using System.Reflection;

namespace AppClassLibraryDomain.DAO.NHibernate
{
    public sealed class SessionFactory
    {
        private static volatile ISessionFactory sessionFactory;
        private static object syncRoot = new Object();

        public static ISession OpenSession
        {
            get
            {
                if (sessionFactory == null)
                {
                    lock (syncRoot)
                    {
                        if (sessionFactory == null)
                        {
                            var configuration = new Configuration();
                            configuration.Configure();
                            //Alternativa para adicionar os assemblies
                            //configuration.AddAssembly(typeof(Usuario).Assembly);
                            configuration.AddAssembly(Assembly.GetCallingAssembly());
                            sessionFactory = configuration.BuildSessionFactory();
                        }
                    }
                }
                return sessionFactory.OpenSession();
            }
        }
    }
}
