namespace Web_API.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll(int id = 0);
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        T Update(T entity);
        T Delete(T entity);
        Task<bool> IsvalidGenre(int id);
        
        public void seed(); 
    }
}
