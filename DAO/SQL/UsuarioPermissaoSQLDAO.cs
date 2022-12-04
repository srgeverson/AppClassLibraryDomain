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

        public IList<UsuarioPermissao> SelectByEmail(string email)
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
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
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