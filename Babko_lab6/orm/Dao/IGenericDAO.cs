using System;

namespace orm.Dao;

public interface IGenericDAO<T>
{
    T Merge(T item);
    T GetById(long id);
    List<T> GetAll();
    void Delete(T item);
}
