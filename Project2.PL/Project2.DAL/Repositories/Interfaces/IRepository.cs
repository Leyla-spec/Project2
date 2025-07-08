using Project2.Core.Entities;

namespace Project2.DAL.Repositories.Interfaces
{
    public interface IRepository <T> where T : BaseEntity
    {
        T GetById(int id);
        List<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        IQueryable<T> Query();
        void SaveChanges();
    }
}
