using System;
using System.Collections.Generic;
using System.Linq;
using EduConnect.Interfaces;

namespace EduConnect.Services
{
    // Generic repository implementing IRepository using an in-memory list.
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _data = new List<T>();

        public IEnumerable<T> GetAll() => _data;

        public T GetById(Guid id)
        {
            // Using reflection for the in-memory mock to find the Id property dynamically
            return _data.FirstOrDefault(e => (Guid)e.GetType().GetProperty("Id")?.GetValue(e) == id);
        }

        public void Add(T entity)
        {
            _data.Add(entity);
        }

        public void Update(T entity)
        {
            var id = (Guid)entity.GetType().GetProperty("Id")?.GetValue(entity);
            var existing = GetById(id);
            if (existing != null)
            {
                _data.Remove(existing);
                _data.Add(entity);
            }
        }

        public void Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _data.Remove(entity);
            }
        }

        public IQueryable<T> Query() => _data.AsQueryable();
    }
}
