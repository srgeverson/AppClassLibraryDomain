using AppClassLibraryDomain.model;
using AppClassLibraryDomain.model.DTO;
using System;
using System.Collections.Generic;

namespace AppClassLibraryDomain.DAO
{
    #region Inteface
    /// <summary>
    /// Inteface de persistência de objetos permissões
    /// </summary>
    public interface ISistemaDAO : IGenericDAO<Sistema, long?>
    {
        void CreateTableByName(String nome);
        IList<Sistema> SelectByObject(Sistema sistema);
        IList<ObterFeriadoDTO> SelectFeriadoByAno(Int32 ano);
    }
    #endregion
}
