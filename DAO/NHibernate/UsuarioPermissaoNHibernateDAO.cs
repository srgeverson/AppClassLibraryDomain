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
    public class UsuarioPermissaoNHibernateDAO : IUsuarioPermissaoDAO
    {
        private SessionFactoryImpl _sessionFactoryImpl;

        public SessionFactoryImpl SessionFactoryImpl { set => _sessionFactoryImpl = value; }

        public bool DeleteById(long? id)
        {
            var usuarioPermissao = this.SelectById(id);
            _sessionFactoryImpl.OpenSession.Delete(usuarioPermissao);
            return true;
        }

        public UsuarioPermissao Insert(UsuarioPermissao usuarioPermissao) => (UsuarioPermissao)_sessionFactoryImpl.OpenSession.Save(usuarioPermissao);

        public IList<UsuarioPermissao> SelectAll() => _sessionFactoryImpl.OpenSession.Query<UsuarioPermissao>().ToList();

        public IList<UsuarioPermissao> SelectByContainsProperties(UsuarioPermissao usuarioPermissao) =>
            _sessionFactoryImpl
            .OpenSession
            .CreateCriteria<UsuarioPermissao>()
            .Add(
                Expression.Or(
                    Expression.Eq("Id", usuarioPermissao.Id),
                    Expression.Eq("Ativo", usuarioPermissao.Ativo)
                    )
                )
            .List<UsuarioPermissao>();

        public IList<UsuarioPermissao> SelectByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public UsuarioPermissao SelectById(long? id) =>
            _sessionFactoryImpl
            .OpenSession
            .CreateCriteria<UsuarioPermissao>()
            .Add(Expression.Eq("id", id))
            .List<UsuarioPermissao>()
            .FirstOrDefault();

        public IList<UsuarioPermissao> SelectByObject(UsuarioPermissao instanceObject)
        {
            throw new NotImplementedException();
        }

        public bool UpdateById(UsuarioPermissao usuarioPermissao)
        {
            if (usuarioPermissao.Id == null)
                throw new ArgumentNullException(String.Format("O Id não pode ser nulo"));

            usuarioPermissao = SelectById(usuarioPermissao.Id);
            if (usuarioPermissao == null)
                throw new NullReferenceException(String.Format("Não foi encontrado permissao do usuário com Id informado"));

            _sessionFactoryImpl.OpenSession.Update(usuarioPermissao);

            return true;
        }
    }
    #endregion
}
