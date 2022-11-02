using System;
using System.Text;
using System.Collections.Generic;
using AppClassLibraryDomain.utils;
using AppClassLibraryDomain.model;
using System.Data.SqlClient;

namespace AppClassLibraryDomain.DAO.SQL
{
    /// <summary>
    /// Classe responsável por consultar os registros relacionados à tabela Permissaos.
    /// </summary>
    public class PermissaoDAO
    {
        private String urlConnection;

        public String UrlConnection { set => urlConnection = value; }

        public List<Permissao> SelectByNomeUsuario(string nomeUsuario)
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

                    var resultSetToModel = new ResultSetToModel<Permissao>();
                    permissaos = resultSetToModel.ToListModel(sqlDataReader);
                }
                return permissaos;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        internal List<Permissao> SelectByEmail(string email)
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

                    var resultSetToModel = new ResultSetToModel<Permissao>();
                    permissaos = resultSetToModel.ToListModel(sqlDataReader);
                }
                return permissaos;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        internal List<Permissao> Select()
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

                    var resultSetToModel = new ResultSetToModel<Permissao>();
                    permissaos = resultSetToModel.ToListModel(sqlDataReader);
                }
                return permissaos;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }
    }
}
