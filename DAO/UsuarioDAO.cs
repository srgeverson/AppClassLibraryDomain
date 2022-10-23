using System;
using System.Text;
using System.Collections.Generic;
using AppClassLibraryDomain.utils;
using AppClassLibraryDomain.model;
using System.Data.SqlClient;

namespace AppClassLibraryDomain.DAO
{
    #region Inteface
    /// <summary>
    /// Inteface de persistência de objetos usuarios
    /// </summary>
    public interface IUsuarioDAO : IGenericDAO<Usuario, long?>
    {
        bool UpdateDataUltimoAcessoById(int? id);
        Usuario SelectByNome(string email);
        Usuario SelectByEmail(string email);
    }
    #endregion

    #region Class
    /// <summary>
    /// Classe responsável por consultar os registros relacionados à tabela usuarios.
    /// </summary>
    public class UsuarioDAO : IUsuarioDAO
    {
        private ConnectionFactoryDAO connectionFactoryDAO;

        public ConnectionFactoryDAO ConnectionFactoryDAO { set => connectionFactoryDAO = value; }

        public bool DeleteById(long? id)
        {
            try
            {
                var usuarioRemovido = false;
                using (var sqlConnection = new SqlConnection(connectionFactoryDAO.Url))
                {
                    sqlConnection.Open();

                    var sqlCommand = new SqlCommand("DELETE FROM usuarios WHERE Id = @id", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    var sqlDataReader = sqlCommand.ExecuteReader();
                    usuarioRemovido = sqlDataReader.RecordsAffected > 0;

                    sqlConnection.Close();
                }
                return usuarioRemovido;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public Usuario Insert(Usuario usuario)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(connectionFactoryDAO.Url))
                {
                    sqlConnection.Open();

                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("INSERT INTO usuarios (nome, email, senha, ativo, data_cadastro, data_operacao) ");
                    stringBuilder.Append("VALUES (@nome, @email, @senha, @ativo, GETUTCDATE(), GETUTCDATE()); ");
                    stringBuilder.Append("SELECT @@IDENTITY AS Id;");

                    var sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@nome", usuario.Nome);
                    sqlCommand.Parameters.AddWithValue("@email", usuario.Email);
                    sqlCommand.Parameters.AddWithValue("@senha", usuario.Senha);
                    sqlCommand.Parameters.AddWithValue("@ativo", usuario.Ativo);

                    var sqlDataReader = sqlCommand.ExecuteReader();

                    var resultSetToModel = new ResultSetToModel<Usuario>();
                    usuario.Id = resultSetToModel.ToModel(sqlDataReader, true).Id;

                    sqlConnection.Close();
                }
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public IList<Usuario> SelectAll()
        {
            try
            {
                var usuarios = new List<Usuario>();
                using (var sqlConnection = new SqlConnection(connectionFactoryDAO.Url))
                {
                    sqlConnection.Open();
                    var sqlCommand = new SqlCommand("SELECT * FROM usuarios;", sqlConnection);
                    var sqlDataReader = sqlCommand.ExecuteReader();

                    var resultSetToModel = new ResultSetToModel<Usuario>();
                    usuarios = resultSetToModel.ToListModel(sqlDataReader);

                    sqlConnection.Close();
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public IList<Usuario> SelectByContainsProperties(Usuario instanceObject)
        {
            throw new NotImplementedException();
        }

        public Usuario SelectByEmail(string email)
        {
            try
            {
                Usuario usuario = null;
                using (var sqlConnection = new SqlConnection(connectionFactoryDAO.Url))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand("SELECT u.* FROM usuarios AS u WHERE u.email = @email;", sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@email", email);

                        var sqlDataReader = sqlCommand.ExecuteReader();
                        var resultSetToModel = new ResultSetToModel<Usuario>();
                        usuario = resultSetToModel.ToModel(sqlDataReader, true);
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public Usuario SelectById(long? id)
        {
            try
            {
                Usuario usuario = null;
                using (var sqlConnection = new SqlConnection(connectionFactoryDAO.Url))
                {
                    sqlConnection.Open();

                    var sqlCommand = new SqlCommand("SELECT * FROM usuarios AS u WHERE u.Id = @id", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        var resultSetToModel = new ResultSetToModel<Usuario>();
                        usuario = resultSetToModel.ToModel(sqlDataReader, true);
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public Usuario SelectByNome(string nome)
        {
            try
            {
                Usuario usuario = null;
                using (var sqlConnection = new SqlConnection(connectionFactoryDAO.Url))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand("SELECT * FROM usuarios AS u WHERE u.Nome = @nome;", sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@nome", nome);

                        var sqlDataReader = sqlCommand.ExecuteReader();
                        var resultSetToModel = new ResultSetToModel<Usuario>();
                        usuario = resultSetToModel.ToModel(sqlDataReader, true);
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public IList<Usuario> SelectByObject(Usuario instanceObject)
        {
            throw new NotImplementedException();
        }

        public bool UpdateById(Usuario usuario)
        {
            try
            {
                var usuarioAtualizado = false;

                using (var sqlConnection = new SqlConnection(connectionFactoryDAO.Url))
                {
                    sqlConnection.Open();

                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("UPDATE usuarios ");
                    stringBuilder.Append("SET ");
                    stringBuilder.Append("Nome = ISNULL(@nome, Nome), ");
                    stringBuilder.Append("Email = ISNULL(@email, Email), ");
                    stringBuilder.Append("Senha = ISNULL(@senha, Senha), ");
                    stringBuilder.Append("Ativo = ISNULL(@ativo, Ativo) ");
                    stringBuilder.Append("WHERE Id = @id ");
                    using (var sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@nome", usuario.Nome.Equals(null) ? null : usuario.Nome);
                        sqlCommand.Parameters.AddWithValue("@email", usuario.Email.Equals(null) ? null : usuario.Email);
                        sqlCommand.Parameters.AddWithValue("@senha", usuario.Senha.Equals(null) ? null : usuario.Senha);
                        sqlCommand.Parameters.AddWithValue("@ativo", usuario.Ativo.Equals(null) ? null : usuario.Ativo);
                        sqlCommand.Parameters.AddWithValue("@id", usuario.Id);

                        var sqlDataReader = sqlCommand.ExecuteReader();
                        usuarioAtualizado = sqlDataReader.RecordsAffected > 0;
                    }
                }
                return usuarioAtualizado;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public bool UpdateDataUltimoAcessoById(int? id)
        {
            try
            {
                var usuarioAtualizado = false;

                using (var sqlConnection = new SqlConnection(connectionFactoryDAO.Url))
                {
                    sqlConnection.Open();

                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("UPDATE usuarios ");
                    stringBuilder.Append("SET ");
                    stringBuilder.Append("data_ultimo_acesso = GETDATE() ");
                    stringBuilder.Append("WHERE Id = @id ");
                    using (var sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@id", id);

                        var sqlDataReader = sqlCommand.ExecuteReader();
                        usuarioAtualizado = sqlDataReader.RecordsAffected > 0;
                    }
                }
                return usuarioAtualizado;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }
    }
    #endregion
}
