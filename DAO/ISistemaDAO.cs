using AppClassLibraryDomain.model;
using System.Collections.Generic;

namespace AppClassLibraryDomain.DAO
{
    #region Inteface
    /// <summary>
    /// Inteface de persistência de objetos permissões
    /// </summary>
    public interface ISistemaDAO : IGenericDAO<Sistema, long?>
    {
        IList<Sistema> SelectByObject(Sistema sistema);
    }
    #endregion
}
