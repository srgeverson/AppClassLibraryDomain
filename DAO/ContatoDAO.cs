using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using AppClassLibraryDomain.DAO;
using AppClassLibraryDomain.utils;
using AppClassLibraryDomain.model;

namespace AppClassLibraryDomain.domain.DAO
{
    #region Inteface
    /// <summary>
    /// Inteface de persistência de objetos contatos
    /// </summary>
    public interface IContatoDAO : IGenericDAO<Contato, int> { }
    #endregion

    #region Class
    /// <summary>
    /// Classe de implementação de persistência de objetos contatos
    /// </summary>
    public class ContatoDAO : IContatoDAO
    {
        public bool DeleteById(int id)
        {
            try
            {
                var contatoRemovido = false;
                using (var sqlConnection = new SqlConnection(ConexaoDAO.URLCONEXAO))
                {
                    sqlConnection.Open();

                    var sqlCommand = new SqlCommand("DELETE FROM contatos WHERE Id = @id", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    var sqlDataReader = sqlCommand.ExecuteReader();
                    contatoRemovido = sqlDataReader.RecordsAffected > 0;

                    sqlConnection.Close();
                }
                return contatoRemovido;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public bool DeleteByObject(Contato contato)
        {
            throw new NotImplementedException();
        }

        public Contato Insert(Contato contato)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(ConexaoDAO.URLCONEXAO))
                {
                    sqlConnection.Open();

                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("INSERT INTO contatos (nome, sobre_nome, email, telefone)  ");
                    stringBuilder.Append("VALUES (@nome, @sobre_nome, @email, @telefone); ");
                    stringBuilder.Append("SELECT SCOPE_IDENTITY() AS Id;");

                    var sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection);
                    //sqlCommand.Parameters.AddWithValue("@nome", string.IsNullOrEmpty(contato.Nome) ? DBNull.Value : contato.Nome);
                    //sqlCommand.Parameters.AddWithValue("@sobre_nome", string.IsNullOrEmpty(contato.SobreNome) ? DBNull.Value : contato.SobreNome);
                    //sqlCommand.Parameters.AddWithValue("@email", string.IsNullOrEmpty(contato.Email) ? DBNull.Value : contato.Email);
                    //sqlCommand.Parameters.AddWithValue("@telefone", string.IsNullOrEmpty(contato.Telefone) ? DBNull.Value : contato.Telefone);

                    var sqlDataReader = sqlCommand.ExecuteReader();

                    var resultSetToModel = new ResultSetToModel<Contato>();
                    contato.Id = resultSetToModel.ToModel(sqlDataReader, true).Id;

                    sqlConnection.Close();
                }
                return contato;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }      
        }

        public IList<Contato> SelectAll()
        {
            try
            {
                IList<Contato> contatos = null;
                using (var sqlConnection = new SqlConnection(ConexaoDAO.URLCONEXAO))
                {
                    sqlConnection.Open();

                    var sqlCommand = new SqlCommand("SELECT c.* FROM contatos AS c WHERE c.Id = @id", sqlConnection);

                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        var resultSetToModel = new ResultSetToModel<Contato>();
                        contatos = resultSetToModel.ToListModel(sqlDataReader);
                    }
                }
                return contatos;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public Contato SelectById(int id)
        {
            try
            {
                Contato contato = null;
                using (var sqlConnection = new SqlConnection(ConexaoDAO.URLCONEXAO))
                {
                    sqlConnection.Open();

                    var sqlCommand = new SqlCommand("SELECT c.* FROM contatos AS c WHERE c.Id = @id", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        var resultSetToModel = new ResultSetToModel<Contato>();
                        contato = resultSetToModel.ToModel(sqlDataReader, true);
                    }
                }
                return contato;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }
        
        public IList<Contato> SelectByContainsProperties(Contato contato)
        {
            try
            {
                IList<Contato> contatos = null;
                using (var sqlConnection = new SqlConnection(ConexaoDAO.URLCONEXAO))
                {
                    sqlConnection.Open();

                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("SELECT c.* FROM contatos AS c WHERE 1 = 1 ");
                    if (contato.Id != null)
                        stringBuilder.Append(string.Format("AND c.id LIKE '%{0}%' ", contato.Id));
                    if (contato.Nome != null)
                        stringBuilder.Append(string.Format("AND c.nome = '%{0}%' ", contato.Nome));
                    if (contato.SobreNome != null)
                        stringBuilder.Append(string.Format("AND c.sobre_nome = '%{0}%' ", contato.SobreNome));
                    if (contato.Telefone != null)
                        stringBuilder.Append(string.Format("AND c.telefone = '%{0}%' ", contato.Telefone));
                    var sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection);

                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        var resultSetToModel = new ResultSetToModel<Contato>();
                        contatos = resultSetToModel.ToListModel(sqlDataReader);
                    }
                }
                return contatos;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }
        
        public IList<Contato> SelectByObject(Contato contato)
        {
            throw new NotImplementedException();
        }

        public bool UpdateById(int id)
        {
            try
            {
                var contatoAtualizado = false;

                using (var sqlConnection = new SqlConnection(ConexaoDAO.URLCONEXAO))
                {
                    sqlConnection.Open();

                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("UPDATE contatos ");
                    stringBuilder.Append("SET ");
                    stringBuilder.Append("nome = @nome, ");
                    stringBuilder.Append("sobre_nome = @sobre_nome, ");
                    stringBuilder.Append("email = @email, ");
                    stringBuilder.Append("telefone = @ativo ");
                    stringBuilder.Append("WHERE id = @id ");
                    using (var sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection))
                    {
                        //if (contato.Nome == null)
                        //    sqlCommand.Parameters.AddWithValue("@nome", DBNull.Value);
                        //else
                        //    sqlCommand.Parameters.AddWithValue("@nome", contato.Nome);

                        //if (contato.SobreNome == null)
                        //    sqlCommand.Parameters.AddWithValue("@sobre_nome", DBNull.Value);
                        //else
                        //    sqlCommand.Parameters.AddWithValue("@sobre_nome", contato.SobreNome);

                        //if (contato.Email == null)
                        //    sqlCommand.Parameters.AddWithValue("@email", DBNull.Value);
                        //else
                        //    sqlCommand.Parameters.AddWithValue("@sobre_nome", contato.Email);

                        //if (contato.Telefone == null)
                        //    sqlCommand.Parameters.AddWithValue("@telefone", DBNull.Value);
                        //else
                        //    sqlCommand.Parameters.AddWithValue("@sobre_nome", contato.Telefone);

                        //if (contato.Id == null)
                        //    sqlCommand.Parameters.AddWithValue("@id", contato.Id);
                        //else
                        //    sqlCommand.Parameters.AddWithValue("@sobre_nome", contato.Id);

                        var sqlDataReader = sqlCommand.ExecuteReader();
                        contatoAtualizado = sqlDataReader.RecordsAffected > 0;
                    }
                }
                return contatoAtualizado;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }

        public bool UpdateByObject(Contato contato)
        {
            try
            {
                var contatoAtualizado = false;

                using (var sqlConnection = new SqlConnection(ConexaoDAO.URLCONEXAO))
                {
                    sqlConnection.Open();

                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("UPDATE contatos ");
                    stringBuilder.Append("SET ");
                    stringBuilder.Append("nome = @nome, ");
                    stringBuilder.Append("sobre_nome = @sobre_nome, ");
                    stringBuilder.Append("email = @email, ");
                    stringBuilder.Append("telefone = @ativo ");
                    stringBuilder.Append("WHERE id = @id ");
                    using (var sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection))
                    {
                        if (contato.Nome == null)
                            sqlCommand.Parameters.AddWithValue("@nome", DBNull.Value);
                        else
                            sqlCommand.Parameters.AddWithValue("@nome", contato.Nome);

                        if (contato.SobreNome == null)
                            sqlCommand.Parameters.AddWithValue("@sobre_nome", DBNull.Value);
                        else
                            sqlCommand.Parameters.AddWithValue("@sobre_nome", contato.SobreNome);

                        if (contato.Email == null)
                            sqlCommand.Parameters.AddWithValue("@email", DBNull.Value );
                        else
                            sqlCommand.Parameters.AddWithValue("@sobre_nome", contato.Email);

                        if (contato.Telefone == null)
                            sqlCommand.Parameters.AddWithValue("@telefone", DBNull.Value);
                        else
                            sqlCommand.Parameters.AddWithValue("@sobre_nome", contato.Telefone);

                        if (contato.Id == null)
                            sqlCommand.Parameters.AddWithValue("@id", contato.Id);
                        else
                            sqlCommand.Parameters.AddWithValue("@sobre_nome", contato.Id);

                        var sqlDataReader = sqlCommand.ExecuteReader();
                        contatoAtualizado = sqlDataReader.RecordsAffected > 0;
                    }
                }
                return contatoAtualizado;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um erro em {0}. Detalhes: {1}", this.GetType().Name, ex.Message));
            }
        }
    }
    #endregion
}