using System;
using System.Text;
using System.Collections.Generic;
using AppClassLibraryDomain.utils;
using AppClassLibraryDomain.model;
using System.Data.SqlClient;
using AppClassLibraryDomain.model.DTO;

namespace AppClassLibraryDomain.DAO.SQL
{
    /// <summary>
    /// Classe responsável por consultar os registros relacionados à tabela Sistemas.
    /// </summary>
    public class SistemaSQLDAO : ISistemaDAO
    {
        private String _urlConnection;

        public String UrlConnection { set => _urlConnection = value; }

        public void CreateTableByName(String nome)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(long? id) => throw new NotImplementedException();

        public Sistema Insert(Sistema sistema) => throw new NotImplementedException();

        public IList<Sistema> SelectAll() => throw new NotImplementedException();

        public IList<Sistema> SelectByContainsProperties(Sistema instanceObject) => throw new NotImplementedException();

        public Sistema SelectById(long? id) => throw new NotImplementedException();

        public IList<Sistema> SelectByObject(Sistema sistema)
        {

            try
            {
                var permissaos = new List<Sistema>();
                using (var sqlConnection = new SqlConnection(_urlConnection))
                {
                    sqlConnection.Open();
                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("SELECT s.* ");
                    stringBuilder.Append("FROM sistemas AS s ");
                    stringBuilder.Append("WHERE 1 = 1 ");
                    if (!String.IsNullOrEmpty(sistema.Nome))
                        stringBuilder.Append(String.Format("AND s.nome = '{0}' ", sistema.Nome));
                    if (sistema.Ativo != null)
                        stringBuilder.Append(String.Format("AND s.ativo = {0} ", (bool)sistema.Ativo ? 1 : 0));
                    var sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection);
                    var sqlDataReader = sqlCommand.ExecuteReader();

                    permissaos = ResultSetToModelUtils<Sistema>.ToListModel(sqlDataReader);
                }
                return permissaos;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public IList<ObterFeriadoDTO> SelectFeriadoByAno(int ano)
        {
            throw new NotImplementedException();
        }

        public bool UpdateById(Sistema instanceObject) => throw new NotImplementedException();
    }
}
