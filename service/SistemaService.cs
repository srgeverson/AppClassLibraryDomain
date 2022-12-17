using System;
using System.Collections.Generic;
using System.Linq;
using AppClassLibraryDomain.DAO;
using AppClassLibraryDomain.model;
using AppClassLibraryDomain.model.DTO;

namespace AppClassLibraryDomain.service
{
    #region Interface
    /// <summary>
    /// Interface responsável por tratar dos dados cadastrais relacionados aos sistemas.
    /// </summary>
    public interface ISistemaService : IGenericService<Sistema, long?>
    {
        IList<ObterFeriadoDTO> BuscarFeriadoPorAno(Int32 ano);
        string Sistema(string nomeSistema);
    }
    #endregion

    #region Class
    /// <summary>
    /// Classe  que implementa os serviços relacionados ao cadastro de usuários.
    /// </summary>
    public class SistemaService : ISistemaService
    {
        private ISistemaDAO _sistemaDAO;
        public ISistemaDAO SistemaDAO { set => _sistemaDAO = value; }
        public void Adicionar(Sistema sistema)
        {
            sistema.Ativo = true;
            sistema.DataCadastro = DateTime.UtcNow;
            _sistemaDAO.Insert(sistema);
        }
        public void Alterar(Sistema sistema)
        {
            //sistema.Id = id;
            _sistemaDAO.UpdateById(sistema);
        }
        public Sistema Buscar(long? id) => throw new NotImplementedException();
        public Sistema BuscarPorId(long? id) => _sistemaDAO.SelectById(id);
        public void Excluir(long? id) => _sistemaDAO.DeleteById(Convert.ToInt64(id.ToString()));
        public IList<Sistema> ListarPorObjeto(Sistema sistema) => _sistemaDAO.SelectByObject(sistema);
        public IList<Sistema> ListarTodos() => _sistemaDAO.SelectAll();

        public IList<ObterFeriadoDTO> BuscarFeriadoPorAno(Int32 ano) => _sistemaDAO.SelectFeriadoByAno(ano);

        public string Sistema(string nomeSistema)
        {
            var sistema = ListarPorObjeto(new Sistema() { Nome = nomeSistema, Ativo = true }).FirstOrDefault();
            if (sistema == null)
                throw new Exception("Aplicação não encontrada ou desativada");
            return String.Format("{0} - {1}", sistema.Id, sistema.Descricao);
        }
    }
    #endregion
}
