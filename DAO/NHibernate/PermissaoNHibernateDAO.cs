using System;
using System.Collections.Generic;
using System.Linq;
using AppClassLibraryDomain.model;
using NHibernate.Criterion;

namespace AppClassLibraryDomain.DAO.NHibernate
{
    #region Class
    /// <summary>
    /// Classe de implementação de persistência de objetos usuários com NHibernate
    /// </summary>
    public class PermissaoNHibernateDAO : IPermissaoDAO
    {
        private SessionFactoryImpl _sessionFactoryImpl;

        public SessionFactoryImpl SessionFactoryImpl { set => _sessionFactoryImpl = value; }

        public bool DeleteById(long? id)
        {
            var permissao = this.SelectById(id);
            _sessionFactoryImpl.OpenSession.Delete(permissao);
            return true;
        }

        public Permissao Insert(Permissao permissao) => (Permissao)_sessionFactoryImpl.OpenSession.Save(permissao);

        public IList<Permissao> SelectAll() => _sessionFactoryImpl.OpenSession.Query<Permissao>().ToList();

        public IList<Permissao> SelectByContainsProperties(Permissao permissao) =>
            _sessionFactoryImpl
            .OpenSession
            .CreateCriteria<Permissao>()
            .Add(
                Expression.Or(
                    Expression.Eq("Id", permissao.Id),
                    Expression.Eq("Ativo", permissao.Ativo)
                    )
                )
            .List<Permissao>();

        public Permissao SelectById(long? id) =>
            _sessionFactoryImpl
            .OpenSession
            .CreateCriteria<Permissao>()
            .Add(Expression.Eq("id", id))
            .List<Permissao>()
            .FirstOrDefault();

        public IList<Permissao> SelectByObject(Permissao instanceObject) => throw new NotImplementedException();

        public bool UpdateById(Permissao permissao)
        {
            if (permissao.Id == null)
                throw new ArgumentNullException(String.Format("O Id não pode ser nulo"));

            permissao = SelectById(permissao.Id);
            if (permissao == null)
                throw new NullReferenceException(String.Format("Não foi encontrado permissao com Id informado"));

            _sessionFactoryImpl.OpenSession.Update(permissao);

            return true;
        }
    }
    #endregion
}
