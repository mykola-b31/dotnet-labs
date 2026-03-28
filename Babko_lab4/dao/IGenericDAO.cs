using System;

namespace Babko_lab4.dao;

public interface IGenericDAO<T>
{
    void SaveOrUpdate(T item);
    T GetById(long id);
    List<T> GetAll();
    void Delete(T item);
}
