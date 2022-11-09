using System;
using System.Collections.Generic;
using AppClassLibraryDomain.model;
using NHibernate;

namespace AppClassLibraryDomain.DAO.NHibernate
{
    #region Class
    /// <summary>
    /// Classe de implementação de persistência de objetos usuários com NHibernate
    /// </summary>
    public abstract class NHibernateImplDAO<T, K>
    {
        private SessionFactoryImpl _sessionFactoryImpl;

        public SessionFactoryImpl SessionFactoryImpl { set => _sessionFactoryImpl = value; }

        public bool DeleteById(K id)
        {
            var objeto = SelectById(id);
            _sessionFactoryImpl.OpenSession.Delete(objeto);
            return true;
        }

        public T Insert(T instanceObject)
        {
           return (T)_sessionFactoryImpl.OpenSession.Save(instanceObject);
        }
        private IList<T> GetAll<T>() where T : class
        {
            ICriteria criteria = _sessionFactoryImpl.OpenSession.CreateCriteria<T>();
            return criteria.List<T>();
        }

        public IList<T> SelectAll()
        {
            ICriteria criteria = _sessionFactoryImpl.OpenSession.CreateCriteria(typeof(T));
            return criteria.List<T>();
            //return this.GetAll<T>();
        }

        public IList<T> SelectByContainsProperties(T instanceObject)
        {
            throw new NotImplementedException();
        }

        public T SelectById(K id)
        {
            throw new NotImplementedException();
            //return _sessionFactoryImpl.OpenSession.Get(id);
        }

        public bool UpdateById(T instanceObject)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
