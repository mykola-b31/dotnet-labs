using System;

namespace Babko_lab5.Dao;

public interface IGenericDAO<T>
{
    T Merge(T item);
    T GetById(long id);
    List<T> GetAll();
    void Delete(T item);
}
