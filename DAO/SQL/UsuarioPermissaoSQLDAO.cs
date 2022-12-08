using System;
using System.Text;
using System.Collections.Generic;
using AppClassLibraryDomain.utils;
using AppClassLibraryDomain.model;
using System.Data.SqlClient;
using AppClassLibraryDomain.model.DTO;

namespace AppClassLibraryDomain.DAO.SQL
{
    #region Class
    /// <summary>
    /// Classe de implementação de persistência de objetos usuários co SQL
    /// </summary>
    public class UsuarioPermissaoSQLDAO : IUsuarioPermissaoDAO
    {
        private String _urlConnection;

        public String UrlConnection { set => _urlConnection = value; }

        public bool DeleteById(long? id) => throw new NotImplementedException();

        public UsuarioPermissao Insert(UsuarioPermissao usuarioPermissao) => throw new NotImplementedException();

        public IList<UsuarioPermissao> SelectAll() => throw new NotImplementedException();

        public IList<UsuarioPermissao> SelectByContainsProperties(UsuarioPermissao usuarioPermissao) => throw new NotImplementedException();

        public IList<UsuarioPermissao> SelectByEmail(String email)
        {
            try
            {
                IList<UsuarioPermissao> usuarioPermissaos = null;
                using (var sqlConnection = new SqlConnection(_urlConnection))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand("SELECT up.* FROM usuarios_permissoes AS up INNER JOIN usuarios AS u ON u.id = up.usuario_id WHERE u.email = @email;", sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@email", email);

                        var sqlDataReader = sqlCommand.ExecuteReader();
                        usuarioPermissaos = ResultSetToModelUtils<UsuarioPermissao>.ToListModel(sqlDataReader);
                    }
                }
                return usuarioPermissaos;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public IList<UsuarioPermissaoDTO> SelectByEmailAndSistema(string email, string sistema)
        {
            IList<UsuarioPermissaoDTO> usuarioPermissaos = null;
            using (var sqlConnection = new SqlConnection(_urlConnection))
            {
                sqlConnection.Open();
                var sql = new StringBuilder();
                sql.Append("SELECT up.Id, up.usuario_id AS Usuario, up.permissao_id AS Permissao FROM usuarios_permissoes AS up ");
                sql.Append("INNER JOIN usuarios AS u ON u.id = up.usuario_id ");
                sql.Append("INNER JOIN permissoes AS p ON p.id = up.permissao_id ");
                sql.Append("INNER JOIN Sistemas AS s ON s.id = p.sistema_id ");
                sql.Append("WHERE  1 = 1 ");
                sql.Append("AND  u.email = @email ");
                sql.Append("AND  s.nome = @sistema ");
                using (var sqlCommand = new SqlCommand(sql.ToString(), sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@email", email);
                    sqlCommand.Parameters.AddWithValue("@sistema", sistema);

                    var sqlDataReader = sqlCommand.ExecuteReader();
                    usuarioPermissaos = ResultSetToModelUtils<UsuarioPermissaoDTO>.ToListModel(sqlDataReader);
                }
            }
            return usuarioPermissaos;
        }

        public UsuarioPermissao SelectById(long? id) => throw new NotImplementedException();

        public IList<UsuarioPermissao> SelectByObject(UsuarioPermissao instanceObject)
        {
            throw new NotImplementedException();
        }

        public bool UpdateById(UsuarioPermissao usuarioPermissao) => throw new NotImplementedException();


        #endregion
    }
}