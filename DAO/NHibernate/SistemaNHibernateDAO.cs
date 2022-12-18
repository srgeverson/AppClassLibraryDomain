using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppClassLibraryDomain.model;
using AppClassLibraryDomain.model.DTO;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Mapping;
using NHibernate.Transform;

namespace AppClassLibraryDomain.DAO.NHibernate
{
    #region Class
    /// <summary>
    /// Classe de implementação de persistência de objetos usuários com NHibernate
    /// </summary>
    public class SistemaNHibernateDAO : ISistemaDAO
    {
        private SessionFactoryImpl _sessionFactoryImpl;

        public SessionFactoryImpl SessionFactoryImpl { set => _sessionFactoryImpl = value; }

        public void CreateTableByName(String nome)
        {
            if (false)
            {
                IQuery query = _sessionFactoryImpl.OpenSession.CreateSQLQuery("EXEC Sp_Cria_Tabela @nome = :nome");
                query.SetString("nome", nome);
                var result = query.ExecuteUpdate();
            }
            else
            {
               var criaTabelaDTO = _sessionFactoryImpl.OpenSession
              .GetNamedQuery("spCriaTabela")
              .SetString("nome", nome)
              .SetResultTransformer(Transformers.AliasToBean<CriaTabelaDTO>())
              .List<CriaTabelaDTO>()
              .FirstOrDefault();
            }
        }

        public bool DeleteById(long? id)
        {
            var sistema = this.SelectById(id);
            _sessionFactoryImpl.OpenSession.Delete(sistema);
            return true;
        }

        public Sistema Insert(Sistema sistema) => (Sistema)_sessionFactoryImpl.OpenSession.Save(sistema);

        public IList<Sistema> SelectAll() => _sessionFactoryImpl.OpenSession.Query<Sistema>().ToList();

        public IList<Sistema> SelectByContainsProperties(Sistema sistema) =>
            _sessionFactoryImpl
            .OpenSession
            .CreateCriteria<Sistema>()
            .Add(
                Expression.Or(
                    Expression.Eq("Id", sistema.Id),
                    Expression.Eq("Ativo", sistema.Ativo)
                    )
                )
            .List<Sistema>();

        public Sistema SelectById(long? id) =>
            _sessionFactoryImpl
            .OpenSession
            .CreateCriteria<Sistema>()
            .Add(Expression.Eq("id", id))
            .List<Sistema>()
            .FirstOrDefault();

        public IList<Sistema> SelectByObject(Sistema sistema)
        {
            var hql = new StringBuilder();
            hql.Append("FROM Sistema s WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(sistema.Nome))
                hql.Append("AND s.Nome = :nome ");
            if (sistema.Ativo != null)
                hql.Append("AND s.Ativo = :ativo ");

            var result = _sessionFactoryImpl.OpenSession.CreateQuery(hql.ToString());

            if (!string.IsNullOrEmpty(sistema.Nome))
                result.SetParameter("nome", sistema.Nome);
            if (sistema.Ativo != null)
                result.SetParameter("ativo", sistema.Ativo);

            return result.List<Sistema>();
        }
        public IList<ObterFeriadoDTO> SelectFeriadoByAno(Int32 ano)
        {
            var listObterFeriadoDTO = _sessionFactoryImpl.OpenSession
               .GetNamedQuery("obterFeriados")
               .SetInt32("ano", ano)
               .SetResultTransformer(Transformers.AliasToBean<ObterFeriadoDTO>())
               .List<ObterFeriadoDTO>();

            return listObterFeriadoDTO;
        }
        public bool UpdateById(Sistema sistema)
        {
            if (sistema.Id == null)
                throw new ArgumentNullException(String.Format("O Id não pode ser nulo"));

            sistema = SelectById(sistema.Id);
            if (sistema == null)
                throw new NullReferenceException(String.Format("Não foi encontrado permissao do usuário com Id informado"));

            _sessionFactoryImpl.OpenSession.Update(sistema);

            return true;
        }
    }
    #endregion
}
