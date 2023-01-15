using System;
using System.Text;
using System.Collections.Generic;
using AppClassLibraryDomain.utils;
using AppClassLibraryDomain.model;
using System.Data.SqlClient;

namespace AppClassLibraryDomain.DAO.SQL
{
    #region Class
    /// <summary>
    /// Classe responsável por consultar os registros relacionados à tabela Permissaos.
    /// </summary>
    public class PermissaoSQLDAO : IPermissaoDAO
    {
        private String urlConnection;

        public String UrlConnection { set => urlConnection = value; }

        public IList<Permissao> SelectByNomeUsuario(string nomeUsuario)
        {
            try
            {
                var permissaos = new List<Permissao>();
                using (var sqlConnection = new SqlConnection(urlConnection))
                {
                    sqlConnection.Open();
                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("SELECT p.* ");
                    stringBuilder.Append("FROM permissoes AS p ");
                    stringBuilder.Append("INNER JOIN usuarios_permissoes AS up ON (up.permissao_id = p.id) ");
                    stringBuilder.Append("INNER JOIN usuarios AS u ON (u.id = up.usuario_id) ");
                    stringBuilder.Append(string.Format("WHERE u.nome = '{0}';", nomeUsuario));
                    var sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection);
                    var sqlDataReader = sqlCommand.ExecuteReader();

                    permissaos = ResultSetToModelUtils<Permissao>.ToListModel(sqlDataReader);
                }
                return permissaos;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public IList<Permissao> SelectByEmail(string email)
        {
            try
            {
                var permissaos = new List<Permissao>();
                using (var sqlConnection = new SqlConnection(urlConnection))
                {
                    sqlConnection.Open();
                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("SELECT p.* ");
                    stringBuilder.Append("FROM permissoes AS p ");
                    stringBuilder.Append("INNER JOIN usuarios_permissoes AS up ON (up.permissao_id = p.id) ");
                    stringBuilder.Append("INNER JOIN usuarios AS u ON (u.id = up.usuario_id) ");
                    stringBuilder.Append(string.Format("WHERE u.email = '{0}';", email));
                    var sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection);
                    var sqlDataReader = sqlCommand.ExecuteReader();

                    permissaos = ResultSetToModelUtils<Permissao>.ToListModel(sqlDataReader);
                }
                return permissaos;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public IList<Permissao> Select()
        {
            try
            {
                var permissaos = new List<Permissao>();
                using (var sqlConnection = new SqlConnection(urlConnection))
                {
                    sqlConnection.Open();
                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("SELECT p.* ");
                    stringBuilder.Append("FROM permissoes AS p; ");
                    var sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection);
                    var sqlDataReader = sqlCommand.ExecuteReader();

                    permissaos = ResultSetToModelUtils<Permissao>.ToListModel(sqlDataReader);
                }
                return permissaos;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public bool DeleteById(long? id) => throw new NotImplementedException();

        public Permissao Insert(Permissao instanceObject) => throw new NotImplementedException();

        public IList<Permissao> SelectAll() => Select();

        public IList<Permissao> SelectByContainsProperties(Permissao instanceObject) => throw new NotImplementedException();

        public Permissao SelectById(long? id) => throw new NotImplementedException();

        public bool UpdateById(Permissao instanceObject) => throw new NotImplementedException();

        public IList<Permissao> SelectByObject(Permissao instanceObject) => throw new NotImplementedException();
    }
    #endregion
}
