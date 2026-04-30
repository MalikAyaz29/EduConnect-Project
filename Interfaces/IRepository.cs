using System;
using System.Collections.Generic;
using System.Linq;

namespace EduConnect.Interfaces
{
    // SOLID Principle: Interface Segregation Principle (ISP) & Dependency Inversion Principle (DIP)
    // Defines a generic repository contract to abstract the data source (in-memory collection in our case).
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
        IQueryable<T> Query();
    }
}
