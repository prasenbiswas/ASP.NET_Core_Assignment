using CoreAssignmentForRollBased.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreAssignmentForRollBased.Repository
{
    public class GenericsRepository<T> : IGenericsRepository<T> where T : class
    {
        private EmployeeDbContext _context = null;
        private DbSet<T> table = null;
        //public GenericsRepository()
        //{
        //    this._context = new EmployeeDbContext();
        //    table = _context.Set<T>();
        //}
        public GenericsRepository(EmployeeDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        //public void Delete(string id)
        //{
        //    T existing = table.Find(id);
        //    table.Remove(existing);
        //}

    }
}
