using System.Collections.Generic;

namespace AppClassLibraryDomain.DAO
{
    public interface IGenericDAO<T, K>
    {
        bool DeleteById(K id);

        bool DeleteByObject(T instanceObject);

        T Insert(T instanceObject);
        
        IList<T> SelectAll();
        
        IList<T> SelectByContainsProperties(T instanceObject);
        
        T SelectById(K id);
       
        IList<T> SelectByObject(T instanceObject);

        bool UpdateById(K id);
        
        bool UpdateByObject(T instanceObject);
    }
}
