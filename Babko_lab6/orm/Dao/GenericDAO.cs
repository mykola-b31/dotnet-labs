using NHibernate;
using ISession = NHibernate.ISession;

namespace orm.Dao;

public class GenericDAO<T> : IGenericDAO<T>
{
    protected ISession session;

    public GenericDAO() { }

    public GenericDAO(ISession session)
    {
        this.session = session;
    }

    public void Delete(T item)
    {
        ITransaction transaction = session.BeginTransaction();
        session.Delete(item);
        transaction.Commit();
    }

    public List<T> GetAll()
    {
        return new List<T>(session.CreateCriteria(typeof(T)).List<T>());
    }

    public T GetById(long id)
    {
        return session.Get<T>(id);
    }

    public T Merge(T item)
    {
        ITransaction transaction = session.BeginTransaction();
        T resultItem = (T) session.Merge(item);
        transaction.Commit();
        return resultItem;
    }
}
