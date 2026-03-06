using System;

namespace Babko_lab3.dao;

public interface IGenericDAO<T>
{
    void SaveOrUpdate(T item);
    T GetById(long id);
    List<T> GetAll();
    void Delete(T item);
}
