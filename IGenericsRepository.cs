namespace CoreAssignmentForRollBased.Repository
{
    public interface IGenericsRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        //void Delete(string id);
        void Save();
    }
}
