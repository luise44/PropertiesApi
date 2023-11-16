using Microsoft.EntityFrameworkCore;
using Properties.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Data.Repositories.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly PropertiesDbContext _propertiesDbContext;

        public BaseRepository(PropertiesDbContext propertiesDbContext)
        {
            _propertiesDbContext = propertiesDbContext;
        }

        public virtual T GetById(int id)
        {
            return _propertiesDbContext
                .Set<T>()
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public virtual IEnumerable<T> Find(Func<T, bool> function)
        {
            return _propertiesDbContext
                .Set<T>()
                .Where(function);
        }

        public virtual List<T> GetAll()
        {
            return _propertiesDbContext
                .Set<T>()
                .ToList();
        }

        public virtual void Add(T entity)
        {
            _propertiesDbContext
                .Set<T>()
                .Add(entity);
        }

        public virtual void Update(T entity)
        {
            _propertiesDbContext
                .Set<T>()
                .Update(entity);
        }

        public virtual void Remove(T entity)
        {
            _propertiesDbContext
                .Set<T>()
                .Remove(entity);
        }

        public virtual async Task SaveChanges()
        {
            await _propertiesDbContext
                .SaveChangesAsync();
        }
    }
}
