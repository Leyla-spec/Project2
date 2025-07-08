using Project2.Core.Entities;
using Project2.DAL.Repositories.Interfaces;

namespace Project2.DAL.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public T GetById(int id)
        {

        }
        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }
        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public IQueryable<T> Query()
        {
            throw new NotImplementedException();
        }
        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
