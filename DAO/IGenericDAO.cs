using System.Collections.Generic;

namespace AppClassLibraryDomain.DAO
{
    public interface IGenericDAO<T, K>
    {
        bool DeleteById(K id);

        T Insert(T instanceObject);
        
        IList<T> SelectAll();
        
        IList<T> SelectByContainsProperties(T instanceObject);
        
        T SelectById(K id);
       
        bool UpdateById(T instanceObject);
    }
}
