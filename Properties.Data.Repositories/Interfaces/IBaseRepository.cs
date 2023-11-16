using Properties.Data.Entities;

namespace Properties.Data.Repositories.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IEnumerable<T> Find(Func<T, bool> function);
        List<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task SaveChanges();
    }
}
